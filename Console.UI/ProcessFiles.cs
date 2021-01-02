namespace Console.UI
{
    using Microsoft.WindowsAPICodePack.Shell;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class ProcessFiles
    {
        public void MoveMovies(string srcPath, string destFilePath, int fileBatch, IList<string> videoExtentions, SearchOption searchOption)
        {
            try
            {
                MoveFiles(srcPath, destFilePath, fileBatch, searchOption, videoExtentions);
                foreach (string dir in Directory.GetDirectories(srcPath))
                {
                    MoveFiles(dir, destFilePath, fileBatch, searchOption, videoExtentions);
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public void MoveMusic(string srcPath, string destFilePath, int fileBatch, SearchOption searchOption, IList<string> fileExt = null, bool removeDatePart = false)
        {
            var files = GetFiles(srcPath, searchOption, fileExt);
            var batchFiles = files.Take(fileBatch).ToList();
            foreach (FileInfo fileInfo in batchFiles)
            {
                var srcFilePath = fileInfo.FullName;
                var filename = srcFilePath.Split("\\".ToCharArray()).Last();
                var localFileExt = filename.Split(".".ToCharArray()).Last();


                if (ShellFile.FromFilePath(fileInfo.FullName).Properties.System.ItemAuthors.Value == null) continue;
                if (ShellFile.FromFilePath(fileInfo.FullName).Properties.System.Title.Value == null) continue;
                var artist = ShellFile.FromFilePath(fileInfo.FullName).Properties.System.ItemAuthors.Value[0];
                var title = ShellFile.FromFilePath(fileInfo.FullName).Properties.System.Title.Value;

                artist = artist.Split(';').First();

                artist = artist.Replace("?", ".").Replace("/", "-").Replace("\"", "").Replace("*", "").Replace(",", "-").Replace(":", "-");
                title = title.Replace("?", ".").Replace("/", "-").Replace("\"", "").Replace("*", "").Replace(",", "-").Replace(":", "-");

                var file = $"{artist} - {title}";

                var d = $"{destFilePath}{artist}";

                var src = $"{srcFilePath}";
                var dest = $"{destFilePath}{artist}\\{file}.{localFileExt}";

                try
                {
                    if (!Directory.Exists(d)) Directory.CreateDirectory(d);
                    File.Move(src, dest);
                    Console.WriteLine($"File moved from: {src} to {dest}");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("already exists"))
                    {
                        var destDupDir = $"{destFilePath}Duplicate";
                        if (!Directory.Exists(destDupDir)) Directory.CreateDirectory(destDupDir);
                        var destDup = $"{destDupDir}\\{file}{new Random().Next(1000)}.{localFileExt}";
                        File.Move(src, destDup);
                        Console.WriteLine($"File moved from: {src} to {destDup}");
                    }
                }
            }
        }

        public string RemoveSpecialCharacters(string str)
        {
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string MoveFiles(string srcPath, string destFilePath, int fileBatch, SearchOption searchOption, IList<string> fileExt = null, bool removeDatePart = false)
        {
            var sb = new StringBuilder();

            var files = GetFiles(srcPath, searchOption, fileExt);
            var batchFiles = files.Take(fileBatch).ToList();
            foreach (FileInfo fileInfo in batchFiles)
            {
                var srcFilePath = fileInfo.FullName;
                var file = srcFilePath.Split("\\".ToCharArray()).Last();
                var fileName = file.Split(".".ToCharArray()).First();
                var localFileExt = file.Split(".".ToCharArray()).Last();
                var shellFile = ShellFile.FromFilePath(fileInfo.FullName);
                var date = shellFile.Properties.System.ItemDate.Value.Value;


                var datePart = (date.Month.ToString().Length == 1) ? "0" + date.Month.ToString() : date.Month.ToString();
                var datePath = removeDatePart ? $"" : $"{date.Year}\\" + $"{date.Year.ToString() + datePart}\\";

                var s = $"{destFilePath}\\{datePath}";
                var d = $"{destFilePath}\\{datePath}";

                var src = $"{srcFilePath}";
                var dest = $"{destFilePath}\\{datePath}{file}";

                try
                {
                    if (!Directory.Exists(s)) Directory.CreateDirectory(d);
                    File.Move(src, dest);
                    var line = $"* File moved from: {src} to {dest}";
                    Console.WriteLine(line); sb.AppendLine(line);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("already exists"))
                    {
                        if (ShellFile.FromFilePath(src).Properties.System.Size.Value.Value ==
                            ShellFile.FromFilePath(dest).Properties.System.Size.Value.Value)
                        {
                            if (!File.Exists($"{destFilePath}\\{datePath}{fileName}_Duplicate.{localFileExt}"))
                            {
                                File.Move(src, $"{destFilePath}\\{datePath}{fileName}_Duplicate.{localFileExt}");
                                var line = $"* File moved from: {src} to {destFilePath}\\{datePath}{fileName}_Duplicate.{localFileExt}";
                                Console.WriteLine(line); sb.AppendLine(line);
                            }
                            else
                            {
                                var line = $"* File Already Exists NOT moved from: {src} to {destFilePath}\\{datePath}{fileName}_Duplicate.{localFileExt}";
                                Console.WriteLine(line); sb.AppendLine(line);
                            }
                        }
                        else
                        {
                            var counter = 1;
                            while (File.Exists($"{destFilePath}\\{datePath}{fileName}_{counter}.{localFileExt}"))
                            {
                                counter++;
                            }

                            File.Move(src, $"{destFilePath}\\{datePath}{fileName}_{counter}.{localFileExt}");
                            var line = $"* File moved from: {src} to {destFilePath}\\{datePath}{fileName}_{counter}.{localFileExt}";
                            Console.WriteLine(line); sb.AppendLine(line);
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return sb.ToString();
        }

        public static List<FileInfo> GetFiles(string srcPath, SearchOption searchOption, IList<string> fileExt)
        {
            DirectoryInfo srcDirectory = new DirectoryInfo(srcPath);
            List<FileInfo> files;
            if (fileExt == null)
            {
                files = srcDirectory.GetFiles($"*.*", searchOption).ToList();
            }
            else
            {
                files = srcDirectory.GetFiles($"*.*", searchOption).Where(fl => fileExt.Any(ext => fl.Name.ToLower().EndsWith(ext.ToLower()))).ToList();
            }

            return files;
        }

        public void DirectorySearch(string dir, string destFilePath, int fileBatch, IList<string> videoExtentions, SearchOption searchOption)
        {
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    //Console.WriteLine(Path.GetFileName(f));
                }
                foreach (string d in Directory.GetDirectories(dir))
                {
                    Console.WriteLine(Path.GetFileName(d));
                    MoveMovies(d, destFilePath, fileBatch, videoExtentions, SearchOption.TopDirectoryOnly);
                    DirectorySearch(d, destFilePath, fileBatch, videoExtentions, SearchOption.TopDirectoryOnly);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RemoveEmptyDirectories(string startDir, bool removeHiddenFiles = true)
        {
            foreach (var directory in Directory.GetDirectories(startDir))
            {
                RemoveEmptyDirectories(directory);

                var files = GetFiles(directory, SearchOption.AllDirectories, null);
                var hasOnlyHiddenFile = files.All(f => f.Attributes.HasFlag(FileAttributes.Hidden));

                if (removeHiddenFiles && hasOnlyHiddenFile)
                {
                    foreach (var file in files)
                    {
                        File.Delete(file.FullName);
                    }
                }

                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                    Console.WriteLine($"Folder deleted: {directory}");
                }
            }
        }

        public static void WriteLinesToFiles(string srcPath)
        {
            if (File.Exists($"{srcPath}\\FileNames.csv")) File.Delete($"{srcPath}\\FileNames.csv");

            var lines = new List<string>();
            var files = GetFiles(srcPath, SearchOption.AllDirectories, null);
            lines.Add($"Artist,Title,Filename,Ext.,FullName");

            //lines.Add($"Artist,Title,Filename, Duplicate");
            //var group =
            //    (from fl in files
            //     select new DummyClass
            //     {
            //         Filename = Path.GetFileName(fl.FullName),
            //         Title = Path.GetFileName(fl.FullName).Split('-')[1].Trim(),
            //         Artist = Path.GetFileName(fl.FullName).Split('-')[0].Trim()
            //     }).GroupBy(x => x.Artist).ToList();

            //foreach (var gs in group)
            //{
            //    var count = 0;
            //    foreach (var g in gs)
            //    {
            //        count = gs.Where(f => !f.Filename.Equals(g.Filename)).Any(local => local.Title.StartsWith(g.Title)) ? count + 1 : count;
            //        var dup = $"Duplicate_{count}";
            //        lines.Add($"{g.Artist},{g.Title},{g.Filename},{dup}");
            //    }
            //}


            foreach (FileInfo file in files)
            {
                var index = file.Name.IndexOf(" - ");
                var artist = file.Name.Substring(0, index);

                var ext = file.Name.Split('.').Last();
                var title = file.Name.Substring(index + 3, file.Name.Length - (index + 3)).Replace($".{ext}", "");
                var filename = file.Name.Replace(ext, "");

                lines.Add($"{artist},{title},{filename},{ext},{file.FullName}");

            }

            File.WriteAllLines($"{srcPath}\\FileNames.csv", lines);
        }

        private static string MarkAsDuplicate(FileInfo file, List<FileInfo> files)
        {
            var index = file.Name.IndexOf(" - ");
            var artist = file.Name.Substring(0, index);
            var ext = file.Name.Split('.').Last();
            var title = file.Name.Substring(index + 3, file.Name.Length - (index + 3)).Replace($".{ext}", "");
            var filename = file.Name.Replace(ext, "");

            //TODO:

            var count = 0;
            var localFiles = files.Where(f => !f.Equals(file)).Where(f => f.Name.StartsWith(artist)).ToList();
            foreach (var localFile in localFiles)
            {
                var localTitle = ShellFile.FromFilePath(localFile.FullName).Properties.System.Title.Value;
                if (title.StartsWith(localTitle))
                    count++;
            }

            var dup = $"Duplicate_{count}";
            return $"{artist},{title},{filename},{ext},{file.FullName},{dup}";
        }

        public static void WriteTextToFiles(string filePath, string text)
        {
            //string text = "A class is the most powerful data type in C#. Like a structure, " +
            //             "a class defines the data and behavior of the data type. ";
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", text);
            File.WriteAllText(filePath, text);
        }


        public static void RenameFiles(string srcPath, string replaceFrom, string replaceTo)
        {
            var files = ProcessFiles.GetFiles(srcPath, SearchOption.AllDirectories, new string[] { "MKV" });
            foreach (var file in files)
            {
                var oldfile = file.Name;
                var newFile = (oldfile.Contains(replaceFrom)) ? oldfile.Replace(replaceFrom, replaceTo) : oldfile;
                var newFileFull = $"{file.Directory}\\{newFile}";
                File.Move(file.FullName, newFileFull);
            }
        }

        public static string ExecuteProcessFiles(string srcPath, string destFilePath, FileType fileType, bool? allDirectories)
        {
            var searchOption = (allDirectories ?? false) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var sb = new StringBuilder();
            var pf = new ProcessFiles();
            var fileBatch = 45000;// 8500;

            if (fileType == FileType.Photos)
            {
                var extentions = Constants.PhotoExt;
                sb.Append(pf.MoveFiles(srcPath, destFilePath, fileBatch, searchOption, extentions));
            }

            else if (fileType == FileType.Videos)
            {
                var extentions = Constants.VideoExt;
                pf.DirectorySearch(srcPath, destFilePath, fileBatch, extentions, searchOption);
                pf.MoveMovies(srcPath, destFilePath, fileBatch, extentions, searchOption);
            }
            else if (fileType == FileType.Music)
            {
                var extentions = Constants.MusicExt;
                pf.MoveMusic(srcPath, destFilePath, fileBatch, searchOption, extentions);
            }
            else if (fileType == FileType.Any)
            {
                pf.MoveFiles(srcPath, destFilePath, fileBatch, searchOption, null, true);
            }
            else
            {
                throw new NotImplementedException($"FileType: {fileType} not defined. Defined FileType: {FileType.Photos} and {FileType.Videos}");
            }

            var line = "* Process Completed";
            Console.WriteLine(line); sb.AppendLine(line);
            Console.Read();

            return sb.ToString();
        }
    }

    public enum FileType
    {
        Photos,
        Videos,
        Music,
        Any
    }

    public static class Constants
    {
        public static IList<string> PhotoExt = new string[] { "JPG", "PNG", "GIF", "WEBP", "TIFF", "TIF", "PSD", "RAW", "BMP", "HEIF", "INDD", "JPEG 2000", "SVG", "AI", "EPS", "PDF", "AAE", "HEIC", "JPEG", "NEF"};
        public static IList<string> VideoExt = new string[] { "MP4", "AVI", "MOV", "FLV", "WMV", "MPG", "3G2", "3GP" };
        public static IList<string> MusicExt = new string[] { "MP3", "M4P", "M4A", "WMA", "FLAC" };
    }

    public class DummyClass
    {
        public string Filename { get; set; }
        public string Title { get; set; }

        public string Artist { get; set; }
    }

}