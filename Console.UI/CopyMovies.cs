namespace Console.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public static class CopyMovies
    {

        public static string Copy(string targetPathLetter, string destDirLetter, string fileMoviesList)
        {            
            var srcFilenameCollection = GetFilenameCollectionToCopy(fileMoviesList);
            var sb = new StringBuilder();

            try
            {
                // Copy text files.
                foreach (var srcFilename in srcFilenameCollection)
                {
                    var dstFilename = srcFilename.Replace(targetPathLetter, destDirLetter);
                    try
                    {
                        var dstFileExists = File.Exists(dstFilename);
                        if (dstFileExists)
                        {
                            var line3 = $"* File Already Exists, NOT Copy From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}";
                            Console.WriteLine(line3); sb.AppendLine(line3);
                        }
                        else
                        {
                            CreateDirectoryWhenNonExists(dstFilename);

                            // Will not overwrite if the destination file already exists.

                            var line1 = $"* File Copying From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}...";
                            Console.WriteLine(line1); sb.AppendLine(line1);
                            File.Copy(srcFilename, dstFilename, false);
                            var line2 = $"* File Copied From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}";
                            Console.WriteLine(line2); sb.AppendLine(line2);
                        }
                    }

                    // Catch exception if the file was already copied.
                    catch (IOException copyError)
                    {
                        sb.AppendLine(copyError.Message);
                    }
                }

                var line = $"* File Copy Process Completed";
                Console.WriteLine(line);
                sb.AppendLine(line);

                // Delete source files that were copied.
                //foreach (string f in fileToCopyList)
                //{
                //    File.Delete(f);
                //}
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                sb.AppendLine(dirNotFound.Message);
            }

            return sb.ToString();
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

        public static string[] GetFilenameCollectionToCopy(string fileMoviesList)
        {
            string[] lines = File.ReadAllLines(fileMoviesList);
            List<string> l = new List<string>();
            foreach (var line in lines)
            {
                var s = line;
                if (line.StartsWith("\""))
                {
                    s = line.Substring(1);
                }
                if (line.StartsWith("\""))
                {
                    s = s.Substring(0, s.Length - 1);
                }

                l.Add(s);
            }
            return l.ToArray();
        }
    }
}