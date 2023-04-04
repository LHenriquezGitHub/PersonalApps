using ExifLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wpf.UI
{
    public static class HelperClass
    {
        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        /// <summary>
        /// 
        /// Method for getting the date the photo was taken from the image  
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DateTime GetDateTakenFromImage(string path)
        { 
            //retrieves the datetime WITHOUT loading the whole image
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }
    }
}