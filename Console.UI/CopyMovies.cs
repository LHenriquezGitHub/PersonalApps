namespace Console.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public static class CopyMovies
    {

        public static string Copy(string targetPathLetter, string destDirLetter, string fileMoviesList)
        {
            string[] srcFilenameCollection = GetFilenameCollectionToCopy(fileMoviesList);
            StringBuilder sb = new StringBuilder();

            try
            {
                // Copy text files.
                Stopwatch stopwatch = new Stopwatch();
                long toSec = 1000;
                long toMin = toSec * 60;
                foreach (string srcFilename in srcFilenameCollection)
                {

                    string dstFilename = srcFilename.Replace(targetPathLetter, destDirLetter);
                    try
                    {
                        bool dstFileExists = File.Exists(dstFilename);
                        if (dstFileExists)
                        {
                            string line3 = $"* File Already Exists, NOT Copy From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}";
                            Console.WriteLine(line3); 
                            sb.AppendLine(line3);
                        }
                        else
                        {
                            stopwatch.Start();
                            CreateDirectoryWhenNonExists(dstFilename);

                            // Will not overwrite if the destination file already exists.
                            string line1 = $"* File Copying From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}...";
                            Console.WriteLine(line1); 
                            sb.AppendLine(line1);
                            File.Copy(srcFilename, dstFilename, false);
                            long elapsedSeconds = (stopwatch.ElapsedMilliseconds > 60000) ? stopwatch.ElapsedMilliseconds / toMin : stopwatch.ElapsedMilliseconds / toSec;
                            string freq = (stopwatch.ElapsedMilliseconds > 60000) ? "(Mins)" : "(Secs)";
                            string line2 = $"elapsed{freq}: {elapsedSeconds}{freq} - * File Copied From: {GetShortFilename(srcFilename)} To: {GetShortFilename(dstFilename)}";
                            Console.WriteLine(line2); 
                            sb.AppendLine(line2); 
                            stopwatch.Stop();
                        }
                    }

                    // Catch exception if the file was already copied.
                    catch (IOException copyError)
                    {
                        sb.AppendLine(copyError.Message);
                    }
                }

                string line = $"* File Copy Process Completed";
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
            int prct = (int)(srcFilename.Length * .20M);
            string s = srcFilename.Substring(0, prct);
            string s2 = Path.GetFileName(srcFilename);
            s = $"{s}...{s2}";
            return s;
        }

        private static void CreateDirectoryWhenNonExists(string dstFilename)
        {
            string dstDirectoryName = Path.GetDirectoryName(dstFilename);
            if (!Directory.Exists(dstDirectoryName)) Directory.CreateDirectory(dstDirectoryName);
        }

        public static string[] GetFilenameCollectionToCopy(string fileMoviesList)
        {
            string[] lines = File.ReadAllLines(fileMoviesList);
            List<string> l = new List<string>();
            foreach (string line in lines)
            {
                string s = line;
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