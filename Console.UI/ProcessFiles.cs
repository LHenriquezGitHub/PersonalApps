

namespace Console.UI
{
    using Microsoft.WindowsAPICodePack.Shell;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    public class ProcessFiles
    {
        public void MoveMovies(string srcPath, string destFilePath, int fileBatch, string[] videoExtentions, SearchOption searchOption)
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

        public void MoveFiles(string srcPath, string destFilePath, int fileBatch, SearchOption searchOption, string[] fileExt = null, bool removeDatePart = false)
        {
            var files = GetFiles(srcPath, searchOption, fileExt);
            var batchFiles = files.Take(fileBatch).ToList();
            foreach (FileInfo fileInfo in batchFiles)
            {
                var srcFilePath = fileInfo.FullName;
                var file = srcFilePath.Split("\\".ToCharArray()).Last();
                var fileName = file.Split(".".ToCharArray()).First();
                var localFileExt = file.Split(".".ToCharArray()).Last();

                var date = ShellFile.FromFilePath(fileInfo.FullName).Properties.System.ItemDate.Value.Value;
                var datePart = (date.Month.ToString().Length == 1) ? "0" + date.Month.ToString() : date.Month.ToString();
                var datePath = removeDatePart ? $"" :
                    $"{date.Year.ToString()}\\" +
                    $"{date.Year.ToString() + datePart}\\";

                var s = $"{destFilePath}{datePath}";
                var d = $"{destFilePath}{datePath}";

                var src = $"{srcFilePath}";
                var dest = $"{destFilePath}{datePath}{file}";

                try
                {
                    if (!Directory.Exists(s)) Directory.CreateDirectory(d);
                    File.Move(src, dest);
                    Console.WriteLine($"File moved from: {src} to {dest}");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("already exists"))
                    {
                        if (ShellFile.FromFilePath(src).Properties.System.Size.Value.Value ==
                            ShellFile.FromFilePath(dest).Properties.System.Size.Value.Value)
                        {
                            if (!File.Exists($"{destFilePath}{datePath}{fileName}_Duplicate.{localFileExt}"))
                            {
                                File.Move(src, $"{destFilePath}{datePath}{fileName}_Duplicate.{localFileExt}");
                                Console.WriteLine($"File moved from: {src} to {destFilePath}{datePath}{fileName}_Duplicate.{localFileExt}");
                            }
                            else
                            {
                                Console.WriteLine($"File NOT moved from: {src} to {destFilePath}{datePath}{fileName}_Duplicate.{localFileExt}");
                            }
                        }
                        else
                        {
                            var counter = 1;
                            while (File.Exists($"{destFilePath}{datePath}{fileName}_{counter}.{localFileExt}"))
                            {
                                counter++;
                            }

                            File.Move(src, $"{destFilePath}{datePath}{fileName}_{counter}.{localFileExt}");
                            Console.WriteLine($"File moved from: {src} to {destFilePath}{datePath}{fileName}_{counter}.{localFileExt}");
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public static List<FileInfo> GetFiles(string srcPath, SearchOption searchOption, string[] fileExt)
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

        public void DirectorySearch(string dir, string destFilePath, int fileBatch, string[] videoExtentions, SearchOption searchOption)
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

        public static void RemoveEmptyDirectories(string startDir)
        {
            foreach (var directory in Directory.GetDirectories(startDir))
            {
                RemoveEmptyDirectories(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                    Console.WriteLine($"Folder deleted: {directory}");
                }
            }
        }

        public static void WriteLinesToFiles(string srcPath)
        {
            var lines = new List<string>();
            var files = GetFiles(srcPath, SearchOption.TopDirectoryOnly, null);
            foreach (FileInfo fileInfo in files)
            {
                //string[] lines = { "First line", "Second line", "Third line" };
                // WriteAllLines creates a file, writes a collection of strings to the file,
                // and then closes the file.  You do NOT need to call Flush() or Close().
                //File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);

                lines.Add(fileInfo.Name);

            }
            File.WriteAllLines($"{srcPath}FileNames.txt" , lines);
        }

        public void WriteTextToFiles(string filePath, string text)
        {
            //string text = "A class is the most powerful data type in C#. Like a structure, " +
            //             "a class defines the data and behavior of the data type. ";
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", text);
            File.WriteAllText(filePath, text);
        }
    }

    public enum FileType
    {
        Photos,
        Videos,
        Any
    }
}