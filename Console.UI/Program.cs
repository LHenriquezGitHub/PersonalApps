using System;
using System.Collections.Generic;

namespace Console.UI
{
    using System;
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Think of an animal and I'll guess it!");
            //Console.WriteLine("Press enter to start.");
            //Console.ReadLine();

            //string guessedAnimal = GuessAnimal();

            //Console.WriteLine($"I guessed it! It's a {guessedAnimal}.");
            //Console.ReadLine();

            /**********************************************************/

            //CopyMovies.Copy(@"F:\Movies");
            //System.Console.Read();

            /**********************************************************/


            /**********************************************************/

             string srcPath = @"C:\Users\Leo\Desktop\FastPhoto\";
             string destPath = @"C:\Users\Leo\Desktop\FastPhoto\";
             FileType fileType = FileType.Photos;

            ProcessFiles.ExecuteProcessFiles(srcPath, destPath, fileType, true);
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

            //ProcessFiles.WriteLinesToFiles(srcPath);

            /****/
            //ProcessFiles.RenameFiles(destPath);

            ///////////////////////ProcessFiles.WriteDetailsToFiles(destPath);

        }
    }
}