using System;
using System.Collections.Generic;

namespace Console.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /**********************************************************/

            //CopyMovies.Copy(@"F:\Movies");
            //System.Console.Read();

            /**********************************************************/


            /**********************************************************/

            string srcPath = @"C:\Users\Leo\Desktop\Photos";
            string destPath = @"C:\Users\Leo\Desktop\20221022_LeoPhotosVideos2";
            FileType fileType = FileType.Photos;

            ProcessFiles.ExecuteProcessFiles(srcPath, destPath, fileType, true);
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

            /****/
            //ProcessFiles.RenameFiles(destPath);

            ProcessFiles.WriteDetailsToFiles(destPath);

        }
    }
}