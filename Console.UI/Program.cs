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

            var srcPath = @"C:\Users\Leo\Desktop\Christina";
            var destPath = @"C:\Users\Leo\Desktop\Christina2";
            var fileType = FileType.Videos;

            //ProcessFiles.ExecuteProcessFiles(srcPath, destPath, fileType, true);
            //ProcessFiles.WriteLinesToFiles(srcPath);

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

            //ProcessFiles.WriteLinesToFiles(srcPath);

            ProcessFiles.RenameFiles(destPath);

            ProcessFiles.WriteDetailsToFiles(destPath);


        }

      
    }
}