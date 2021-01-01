

namespace Console.UI
{
    using Microsoft.Office.Interop.Excel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Documents;

    public static class CopyMovies
    {

        public static string Copy(string destDirLetter)
        {
            var returnMsg = string.Empty;
            var srcFilenameCollection = GetFilenameCollectionToCopy();

            try
            {
                // Copy text files.
                foreach (var srcFilename in srcFilenameCollection)
                {
                    var dstFilename = srcFilename.Replace(@"G:\Videos\Movies\Movies Kids", destDirLetter);
                    try
                    {
                        var dstFileExists = File.Exists(dstFilename);
                        if (dstFileExists)
                        {
                            Console.WriteLine($"File Already Exists, NOT Copy From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}");
                        }
                        else
                        {
                            CreateDirectoryWhenNonExists(dstFilename);

                            // Will not overwrite if the destination file already exists.

                            Console.WriteLine($"File Copying From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}...");
                            File.Copy(srcFilename, dstFilename, false);
                            Console.WriteLine($"File Copied From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}");
                        }
                    }

                    // Catch exception if the file was already copied.
                    catch (IOException copyError)
                    {
                        returnMsg = (copyError.Message);
                    }
                }

                Console.WriteLine($"File Copy Process Completed");

                // Delete source files that were copied.
                //foreach (string f in fileToCopyList)
                //{
                //    File.Delete(f);
                //}
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                returnMsg = (dirNotFound.Message);
            }

            return returnMsg;
        }

        private static string GetShortFilename(string srcFilename)
        {
            var prct = (int)(srcFilename.Length * .20M);
            var s = srcFilename.Substring(0, prct);
            var s2 = Path.GetFileName(srcFilename);
            s = $"{s}...{s2}";
            return s;
        }

        private static void CreateDirectoryWhenNonExists(string dstFilename)
        {
            var dstDirectoryName = Path.GetDirectoryName(dstFilename);
            if (!Directory.Exists(dstDirectoryName)) Directory.CreateDirectory(dstDirectoryName);
        }

        public static string[] GetFilenameCollectionToCopy()
        {
            var filename = @"G:\Videos\Movies\Movies Kids\MovieToCopyToUsbOrSdCard.txt";
            string[] lines = File.ReadAllLines(filename);
            List<string> l = new List<string>();
            foreach (var    line in lines)
            {
                var s = line;
                if (line.StartsWith("\""))
                {
                    s = line.Substring(1);
                }
                if (line.StartsWith("\""))
                {
                  s =  s.Substring(0,s.Length - 1);
                }

                l.Add(s);
            }
            return l.ToArray();
        }
    }
}