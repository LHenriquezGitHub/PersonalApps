

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

                    //foreach (string f in Directory.GetFiles(d))
                    //{
                    //    MoveFiles(f, destFilePath, fileBatch, searchOption, videoExtentions);
                    //}
                    MoveFiles(dir, destFilePath, fileBatch, searchOption, videoExtentions);
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

        }
        public void MoveFiles(string srcPath, string destFilePath, int fileBatch, SearchOption searchOption, string[] fileExt = null)
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

            var batchFiles = files.Take(fileBatch).ToList();
            foreach (FileInfo fileInfo in batchFiles)
            {
                var srcFilePath = fileInfo.FullName;
                var file = srcFilePath.Split("\\".ToCharArray()).Last();
                var fileName = file.Split(".".ToCharArray()).First();
                var localFileExt = file.Split(".".ToCharArray()).Last();

                var date = ShellFile.FromFilePath(fileInfo.FullName).Properties.System.ItemDate.Value.Value;
                var datePart = (date.Month.ToString().Length == 1) ? "0" + date.Month.ToString() : date.Month.ToString();
                var datePath =
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
                    }
                    else
                    {
                        throw;
                    }
                }
            }
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

    }

    public enum FileType
        {
        Photos,
        Videos
    }
}