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
        public void MoveMovies(string srcPath, string destFilePath, int fileBatch, IList<string> videoExtentions, SearchOption searchOption, string livePhotos = null)
        {
            try
            {
                MoveFiles(srcPath, destFilePath, fileBatch, searchOption, videoExtentions, livePhotos: livePhotos);
                foreach (string dir in Directory.GetDirectories(srcPath))
                {
                    MoveFiles(dir, destFilePath, fileBatch, searchOption, videoExtentions, livePhotos: livePhotos);
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public void MoveMusic(string srcPath, string destFilePath, int fileBatch, SearchOption searchOption, IList<string> fileExt = null)
        {
            List<FileInfo> files = GetFiles(srcPath, searchOption, fileExt);
            List<FileInfo> batchFiles = files.Take(fileBatch).ToList();
            foreach (FileInfo fileInfo in batchFiles)
            {
                string srcFilePath = fileInfo.FullName;
                string filename = srcFilePath.Split("\\".ToCharArray()).Last();
                string localFileExt = filename.Split(".".ToCharArray()).Last();


                if (ShellFile.FromFilePath(fileInfo.FullName).Properties.System.ItemAuthors.Value == null) continue;
                if (ShellFile.FromFilePath(fileInfo.FullName).Properties.System.Title.Value == null) continue;
                string artist = ShellFile.FromFilePath(fileInfo.FullName).Properties.System.ItemAuthors.Value[0];
                string title = ShellFile.FromFilePath(fileInfo.FullName).Properties.System.Title.Value;

                artist = artist.Split(';').First();

                artist = artist.Replace("?", ".").Replace("/", "-").Replace("\"", "").Replace("*", "").Replace(",", "-").Replace(":", "-");
                title = title.Replace("?", ".").Replace("/", "-").Replace("\"", "").Replace("*", "").Replace(",", "-").Replace(":", "-");

                string file = $"{artist} - {title}";

                string d = $"{destFilePath}{artist}";

                string src = $"{srcFilePath}";
                string dest = $"{destFilePath}{artist}\\{file}.{localFileExt}";

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
                        string destDupDir = $"{destFilePath}Duplicate";
                        if (!Directory.Exists(destDupDir)) Directory.CreateDirectory(destDupDir);
                        string destDup = $"{destDupDir}\\{file}{new Random().Next(1000)}.{localFileExt}";
                        File.Move(src, destDup);
                        Console.WriteLine($"File moved from: {src} to {destDup}");
                    }
                }
            }
        }

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static void RenameFiles(string destPath)
        {
            //Rename Files
            List<FileInfo> files = ProcessFiles.GetFiles(destPath, SearchOption.AllDirectories, null);
            foreach (FileInfo file in files)
            {
                if (file.FullName.Contains("_Duplicate"))
                {
                    string removeDupStr = file.FullName.Replace("_Duplicate", "");
                    File.Move(file.FullName, removeDupStr);

                    string line = $"* File rename from: {file.FullName} to {removeDupStr}";
                    Console.WriteLine(line);

                }
            }
        }

        public string MoveFiles(string srcPath, string destFilePath, int fileBatch, SearchOption searchOption, IList<string> fileExt = null, bool removeDatePart = false, string livePhotos = null)
        {
            StringBuilder sb = new StringBuilder();

            List<FileInfo> files = GetFiles(srcPath, searchOption, fileExt);
            List<FileInfo> batchFiles = files.Take(fileBatch).ToList();
            foreach (FileInfo fileInfo in batchFiles)
            {
                string srcFilePath = fileInfo.FullName;
                string fileNameWithExt = srcFilePath.Split("\\".ToCharArray()).Last();
                string fileName = fileNameWithExt.Split(".".ToCharArray()).First();
                string localFileExt = fileNameWithExt.Split(".".ToCharArray()).Last();
                ShellFile shellFile = ShellFile.FromFilePath(fileInfo.FullName);
                DateTime date = shellFile.Properties.System.ItemDate.Value.Value;

                bool isLivePhoto = false;
                if (!string.IsNullOrEmpty(livePhotos))
                {
                    try
                    {   //Convert nanoseconds to milliseconds
                        double? duration = shellFile.Properties.System.Media.Duration.Value.Value * 0.0001;
                        isLivePhoto = duration.HasValue && duration <= 4000;
                    }
                    catch (Exception) { if (!isLivePhoto) isLivePhoto = fileInfo.Length < 5000000; }
                }

                string datePart = (date.Month.ToString().Length == 1) ? "0" + date.Month.ToString() : date.Month.ToString();
                string datePath = removeDatePart ? $"" : $"{date.Year}\\" + $"{date.Year.ToString() + datePart}\\";

                string src = $"{srcFilePath}";
                string dest = isLivePhoto ? $"{destFilePath}\\{datePath}{livePhotos}{fileNameWithExt}" : $"{destFilePath}\\{datePath}{fileNameWithExt}";


                string destdir =  Path.GetDirectoryName(dest);

                try
                {
                    if (!Directory.Exists(destdir)) Directory.CreateDirectory(destdir);
                    File.Move(src, dest);
                    string line = $"* File moved from: {src} to {dest}";
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
                                string line = $"* File moved from: {src} to {destFilePath}\\{datePath}{fileName}_Duplicate.{localFileExt}";
                                Console.WriteLine(line); sb.AppendLine(line);
                            }
                            else
                            {
                                string line = $"* File Already Exists NOT moved from: {src} to {destFilePath}\\{datePath}{fileName}_Duplicate.{localFileExt}";
                                Console.WriteLine(line); sb.AppendLine(line);
                            }
                        }
                        else
                        {
                            int counter = 1;
                            while (File.Exists($"{destFilePath}\\{datePath}{fileName}_{counter}.{localFileExt}"))
                            {
                                counter++;
                            }

                            File.Move(src, $"{destFilePath}\\{datePath}{fileName}_{counter}.{localFileExt}");
                            string line = $"* File moved from: {src} to {destFilePath}\\{datePath}{fileName}_{counter}.{localFileExt}";
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

        public void DirectorySearch(string dir, string destFilePath, int fileBatch, IList<string> videoExtentions, SearchOption searchOption, string livePhotos = null)
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
                    MoveMovies(d, destFilePath, fileBatch, videoExtentions, SearchOption.TopDirectoryOnly, livePhotos: livePhotos);
                    DirectorySearch(d, destFilePath, fileBatch, videoExtentions, SearchOption.TopDirectoryOnly, livePhotos: livePhotos);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RemoveEmptyDirectories(string startDir, bool removeHiddenFiles = true)
        {
            foreach (string directory in Directory.GetDirectories(startDir))
            {
                RemoveEmptyDirectories(directory);

                List<FileInfo> files = GetFiles(directory, SearchOption.AllDirectories, null);
                bool hasOnlyHiddenFile = files.All(f => f.Attributes.HasFlag(FileAttributes.Hidden));

                if (removeHiddenFiles && hasOnlyHiddenFile)
                {
                    foreach (FileInfo file in files)
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


        public static void WriteDetailsToFiles(string srcPath)
        {
            if (File.Exists($"{srcPath}\\FileNames.csv")) File.Delete($"{srcPath}\\FileNames.csv");

            List<string> lines = new List<string>();
            List<FileInfo> files = GetFiles(srcPath, SearchOption.AllDirectories, null);
            string line = $"row,fileNameWithExt,date,datePath,duration,length";
            lines.Add(line);
            Console.WriteLine(line);

            int count = 1;
            foreach (FileInfo fileInfo in files)
            {
                ShellFile shellFile = ShellFile.FromFilePath(fileInfo.FullName);
                string fileName = fileInfo.FullName;
                DateTime date = shellFile.Properties.System.ItemDate.Value.Value;
                string datePart = (date.Month.ToString().Length == 1) ? "0" + date.Month.ToString() : date.Month.ToString();
                string datePath = $"{date.Year}\\" + $"{date.Year.ToString() + datePart}\\";
                double duration = (shellFile.Properties.System.Media.Duration.Value ?? -1 * 0.0001);
                long length = fileInfo.Length;
                int row = count++;

                line = $"{row},{fileName},{date},{datePath},{duration},{length}";
                lines.Add(line);
                Console.WriteLine(line);
            }

            File.WriteAllLines($"{srcPath}\\FileNames.csv", lines);
        }

        public static void WriteLinesToFiles(string srcPath)
        {
            if (File.Exists($"{srcPath}\\FileNames.csv")) File.Delete($"{srcPath}\\FileNames.csv");

            List<string> lines = new List<string>();
            List<FileInfo> files = GetFiles(srcPath, SearchOption.AllDirectories, null);
            string line = $"Artist,Title,Filename,Ext.,FullName";
            lines.Add(line);
            Console.WriteLine(line);


            foreach (FileInfo file in files)
            {
                int index = file.Name.IndexOf(" - ");
                string artist = file.Name.Substring(0, index);

                string ext = file.Name.Split('.').Last();
                string title = file.Name.Substring(index + 3, file.Name.Length - (index + 3)).Replace($".{ext}", "");
                string filename = file.Name.Replace(ext, "");

                line = $"{artist},{title},{filename},{ext},{file.FullName}";
                lines.Add(line);

                Console.WriteLine(line);

            }

            File.WriteAllLines($"{srcPath}\\FileNames.csv", lines);
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
            List<FileInfo> files = ProcessFiles.GetFiles(srcPath, SearchOption.AllDirectories, new string[] { "MKV" });
            foreach (FileInfo file in files)
            {
                string oldfile = file.Name;
                string newFile = (oldfile.Contains(replaceFrom)) ? oldfile.Replace(replaceFrom, replaceTo) : oldfile;
                string newFileFull = $"{file.Directory}\\{newFile}";
                File.Move(file.FullName, newFileFull);
            }
        }

        public static string ExecuteProcessFiles(string srcPath, string destFilePath, FileType fileType, bool? allDirectories)
        {
            SearchOption searchOption = (allDirectories ?? false) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            StringBuilder sb = new StringBuilder();
            ProcessFiles pf = new ProcessFiles();
            int fileBatch = 45000;// 8500;

            if (fileType == FileType.Photos)
            {
                IList<string> extentions = Constants.PhotoExt;
                sb.Append(pf.MoveFiles(srcPath, destFilePath, fileBatch, searchOption, extentions));
            }

            else if (fileType == FileType.Videos)
            {
                IList<string> extentions = Constants.VideoExt;
                string livePhotos = "LivePhotos\\";
                pf.DirectorySearch(srcPath, destFilePath, fileBatch, extentions, searchOption, livePhotos);
                pf.MoveMovies(srcPath, destFilePath, fileBatch, extentions, searchOption, livePhotos);
            }
            else if (fileType == FileType.Music)
            {
                IList<string> extentions = Constants.MusicExt;
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

            string line = "* Process Completed";
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