


using System;
using System.IO;

namespace Console.UI
{
    class Program
    {

        public static void Main(string[] args)
        {

            /**********************************************************/

            //CopyMovies.Copy(@"F:\Movies");
            //System.Console.Read();

            /**********************************************************/


            /**********************************************************/

                var srcPath = @"C:\Users\Leo\Desktop\100D5100";
                var destFilePath = @"C:\Users\Leo\Desktop\100D5100\";

            ExecuteProcessFiles(srcPath, destFilePath, FileType.Photos);
            ProcessFiles.WriteLinesToFiles(srcPath);

            /**********************************************************/

            //Delete Empty Folders
            //ProcessFiles.RemoveEmptyDirectories(srcPath);

            //Renaming Files
            //srcPath = @"F:\Videos\TV Shows\Blue's Clues (Complete)";
            //var collection = new List<KeyValuePair<string, string>>()
            //{
            //    new KeyValuePair<string,string>("Blue's Clues - ",  "BluesClues"),
            //    new KeyValuePair<string,string>( " - ",  ""),
            //    new KeyValuePair<string,string>("'",  ""),
            //    new KeyValuePair<string,string>(" ",  ""),
            //    new KeyValuePair<string,string>("_.",  ".")
            //};
            //foreach (var kv in collection)
            //{
            //    RenameFiles(srcPath, kv.Key, kv.Value);
            //}

            ProcessFiles.WriteLinesToFiles(srcPath);


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

        public static void ExecuteProcessFiles(string srcPath, string destFilePath, FileType fileType)
        {
            var pf = new ProcessFiles();
            var fileBatch = 45000;// 8500;

            if (fileType == FileType.Photos)
            {
                var extentions = Constants.PhotoExt;
                pf.MoveFiles(srcPath, destFilePath, fileBatch, SearchOption.AllDirectories, extentions);
            }

            else if (fileType == FileType.Videos)
            {
               var extentions = Constants.VideoExt;
                pf.DirectorySearch(srcPath, destFilePath, fileBatch, extentions, SearchOption.TopDirectoryOnly);
                pf.MoveMovies(srcPath, destFilePath, fileBatch, extentions, SearchOption.TopDirectoryOnly);
            }
            else if (fileType == FileType.Music)
            {
                var extentions = Constants.MusicExt;
                pf.MoveMusic(srcPath, destFilePath, fileBatch, SearchOption.AllDirectories, extentions);
            }
            else if (fileType == FileType.Any)
            {
                pf.MoveFiles(srcPath, destFilePath, fileBatch, SearchOption.AllDirectories, null, true);
            }
            else
            {
                throw new NotImplementedException($"FileType: {fileType} not defined. Defined FileType: {FileType.Photos} and {FileType.Videos}");
            }

            System.Console.WriteLine("Process Completed");
            System.Console.Read();
        }
    }
}