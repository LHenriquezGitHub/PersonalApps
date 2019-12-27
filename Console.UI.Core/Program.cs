

namespace Console.UI
{
    using Microsoft.WindowsAPICodePack.Shell;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    class Program
    {
        static void Main(string[] args)
        {
            var fileBatch = 50;
            var srcPath = @"E:\My Pictures\Christina";
            var destFilePath = @"C:\Users\Leo\Desktop\Destination\";

            for (int i = 0; i < 200; i++)
            {
                CopyFiles(srcPath, destFilePath, fileBatch);
                Console.WriteLine($"Files copied: {1 * fileBatch}");
            }
        }

        static void CopyFiles(string srcPath, string destFilePath, int fileBatch)
        {
            DirectoryInfo srcDirectory = new DirectoryInfo(srcPath);
            FileInfo[] files = srcDirectory.GetFiles("*.*");

            var batchFiles = files.Take(fileBatch).ToList();
            foreach (FileInfo fileInfo in batchFiles)
            {
                var srcFilePath = fileInfo.FullName;
                var file = srcFilePath.Split("\\".ToCharArray()).Last();
                var fileName = file.Split(".".ToCharArray()).First();
                var fileExt = file.Split(".".ToCharArray()).Last();

                var date = ShellFile.FromFilePath(fileInfo.FullName).Properties.System.ItemDate.Value.Value;
                var datePart = (date.Month.ToString().Length == 1) ? "0" + date.Month.ToString() : date.Month.ToString();
                var datePath =
                    $"{date.Year.ToString()}\\" +
                    $"{date.Year.ToString() + datePart}\\";

                try
                {
                    if (!Directory.Exists($"{ destFilePath}{datePath}")) Directory.CreateDirectory($"{ destFilePath}{ datePath}");
                    File.Copy($"{srcFilePath}", $"{destFilePath}{datePath}{file}");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("already exists"))
                    {

                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}

