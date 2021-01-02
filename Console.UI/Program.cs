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

            var srcPath = @"C:\Users\Leo\Downloads\iloveimg-converted";
            var destPath = @"C:\Users\Leo\Downloads\StagePhotos\";

            
            ProcessFiles.ExecuteProcessFiles(srcPath, destPath, FileType.Photos, true);
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
    }
}