


using Console.UI.Exercises;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Console.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExecuteProcessFiles();
            //ExecuteBasicForBegginersPrt1();
            //ExecuteBasicForBegginersPrt2();
            //ExecuteBasicForBegginersPrt3();
            ExecuteBasicForBegginersPrt4();

            System.Console.WriteLine("{0} says, hello world!", "Leo");
            System.Console.Read();

        }

        private static void ExecuteBasicForBegginersPrt4()
        {
            /*
             * 1- Write a program and ask the user to enter a few numbers separated by a hyphen. 
             * Work out if the numbers are consecutive. For example, if the input is "5-6-7-8-9" or 
             * "20-19-18-17-16", display a message: "Consecutive"; otherwise, display "Not Consecutive".
             */
            //{
            //    System.Console.WriteLine($"Enter Number separated by hyphen");
            //    var readline = System.Console.ReadLine().Split('-').Select(n => Convert.ToInt32(n)).ToArray();

            //    var copyReadline = new int[readline.Length];
            //    Array.Copy(readline, copyReadline, readline.Length);
            //    Array.Sort(copyReadline);

            //    if (readline.SequenceEqual(copyReadline) || readline.Reverse().SequenceEqual(copyReadline))
            //    {
            //        System.Console.WriteLine("Consecutive");
            //    }
            //    else
            //    {
            //        System.Console.WriteLine("Not Consecutive");
            //    }
            //}
            /*
             * 2- Write a program and ask the user to enter a few numbers separated by a hyphen. 
             * If the user simply presses Enter, without supplying an input, exit immediately; 
             * otherwise, check to see if there are duplicates. If so, display "Duplicate" on the console.
             */
            //{
            //    while (true)
            //    {
            //        System.Console.WriteLine($"Enter Number separated by hyphen");
            //        var readline = System.Console.ReadLine();
            //        var array = readline.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
            //        if (string.IsNullOrWhiteSpace(readline)) break;

            //        var groupNumberByCount = array.GroupBy(n => n).Select(number => new
            //        {
            //            Number = number.Key,
            //            Count = number.Count()
            //        });

            //        StringBuilder sb = new StringBuilder();
            //        sb.AppendLine("Duplicate - Number : Count");
            //        foreach (var number in groupNumberByCount)
            //        {
            //            if (number.Count > 1)
            //            {
            //                sb.AppendLine($"{number.Number} : {number.Count}");
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(sb.ToString()))
            //        {
            //            System.Console.WriteLine(sb.ToString());
            //        }
            //    }
            //}
            /*
             * 3 - Write a program and ask the user to enter a time value in the 24 - hour time format(e.g. 19:00). 
             * A valid time should be between 00:00 and 23:59.If the time is valid, display "Ok"; otherwise, display 
             * "Invalid Time".If the user doesn't provide any values, consider it as invalid time. 
             */

            //{
            //    System.Console.WriteLine($"Enter a time value in the 24-hour time format (e.g. 19:00)");
            //    var readline = System.Console.ReadLine();
            //    TimeSpan ts;
            //    if (TimeSpan.TryParse(readline, out ts) && readline.Contains(":"))
            //    {
            //        System.Console.WriteLine("Ok");
            //    }
            //    else
            //    {
            //        System.Console.WriteLine("Invalid Time");
            //    }

            //}
            /*
             * 4 - Write a program and ask the user to enter a few words separated by a space.
             * Use the words to create a variable name with PascalCase. For example, if the user types: 
             * "number of students", display "NumberOfStudents".Make sure that the program is not 
             * dependent on the input.So, if the user types "NUMBER OF STUDENTS", the program should still display "NumberOfStudents".
             */
            //{
            //    System.Console.WriteLine($"Enter a few words separated by a space");
            //    var readline = System.Console.ReadLine();
            //    var titleCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(readline.ToLower());

            //    System.Console.WriteLine(titleCase.Replace(" ", string.Empty));
            //}

            /*
             * 5 - Write a program and ask the user to enter an English word.Count the number of vowels(a, e, o, u, i) 
             * in the word. So, if the user enters "inadequate", the program should display 6 on the console.
             */
            {
                System.Console.WriteLine($"Enter an English word");
                var readline = System.Console.ReadLine().ToLower().ToCharArray();
                int count = 0;

                var charArray = new[] { 'a', 'e', 'o', 'u', 'i' };
                foreach (var c in charArray)
                {
                    count+= readline.Count(w => w.Equals(c));
                }

                System.Console.WriteLine($"The number of vowels in the word: {count}");
            }

        }

        public static void ExecuteProcessFiles()
        {
            ProcessFiles pf = new ProcessFiles();

            //var fileBatch = 8500;
            //var srcPath = @"C:\Users\Leo\Desktop\Source";
            //var destFilePath = @"C:\Users\Leo\Desktop\Destination\";

            //pf.MoveFiles(srcPath, destFilePath, fileBatch, SearchOption.TopDirectoryOnly);

            /*****************************************************************************************/

            //var fileBatch = 500;
            //var srcPath = @"E:\My Pictures\Leo";
            //var destFilePath = @"E:\My Videos\Leo\";

            //string[] videoExtentions = new string[] { "MP4", "AVI", "MOV", "FLV", "WMV", "MPG", "3G2", "3GP" };

            //pf.DirectorySearch(srcPath, destFilePath, fileBatch, videoExtentions, SearchOption.TopDirectoryOnly);

            //pf.MoveMovies(srcPath, destFilePath, fileBatch, videoExtentions, SearchOption.TopDirectoryOnly);

            //System.Console.WriteLine("Process Completed");
            //System.Console.Read();

            /*****************************************************************************************/


        }
        public static void ExecuteBasicForBegginersPrt1()
        {
            BasicForBeginners basicForBeginners = new BasicForBeginners();

            //1.
            for (int n = 5; n < 11; n++)
            {
                var msg = basicForBeginners.IsValidNumber(n);
                System.Console.WriteLine($"Number {n} is {msg}.");
            }

            //2.
            int number1 = 1, number2 = 2;
            var maxNumber = basicForBeginners.GetMaxNumber(number1, number2);
            System.Console.WriteLine($"From Number {number1} and {number2}, {maxNumber} is the max number.");
            number1 = 1; number2 = 1;
            maxNumber = basicForBeginners.GetMaxNumber(number1, number2);
            System.Console.WriteLine($"From Number {number1} and {number2}, {maxNumber} is the max number.");
            number1 = 3; number2 = 2;
            maxNumber = basicForBeginners.GetMaxNumber(number1, number2);
            System.Console.WriteLine($"From Number {number1} and {number2}, {maxNumber} is the max number.");

            //3.
            System.Console.WriteLine("Enter Image width and height (width,height)");
            var widthHeight = System.Console.ReadLine().Split(',');
            int width = Convert.ToInt32(widthHeight[0]);
            int height = Convert.ToInt32(widthHeight[1]);

            var imageOrientation = basicForBeginners.GetImageOrientation(width, height);
            System.Console.WriteLine($"Image Orientation {imageOrientation}.");

            //4.
            int i = 65;
            var message = basicForBeginners.GetSpeedCameraMsg(i);
            System.Console.WriteLine($"Number {i} is {message}.");
            i = 75;
            message = basicForBeginners.GetSpeedCameraMsg(i);
            System.Console.WriteLine($"Number {i} is {message}.");
            i = 130;
            message = basicForBeginners.GetSpeedCameraMsg(i);
            System.Console.WriteLine($"Number {i} is {message}.");
        }
        private static void ExecuteBasicForBegginersPrt2()
        {
            BasicForBeginners basicForBeginners = new BasicForBeginners();

            //1.
            var count = basicForBeginners.NumbersDivisibleByThree();
            System.Console.WriteLine($"numbers between 1 and 100 are divisible by 3: {count}.");

            //2.
            int num = 4;
            var factorial = basicForBeginners.Factorial(num);
            System.Console.WriteLine($"Factorial({num}): {factorial}.");

            //3.
            List<int> numbers = new List<int>();

            while (true)
            {
                System.Console.WriteLine($"Enter Number to [ADD] or [OK] to Exit");
                string readLine = System.Console.ReadLine();
                if (readLine.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;

                }
                numbers.Add(Convert.ToInt32(readLine));
            }
            System.Console.WriteLine($"The Sum: ({numbers.Sum()}).");


            //4.
            int numRandom = 0;
            int numGuest = 2;
            var msg = basicForBeginners.GuessRandomNumber(numGuest, ref numRandom);
            System.Console.WriteLine($"Winning number: {numRandom} - Your number: {numGuest} - {msg}");
            numGuest = 5;
            msg = basicForBeginners.GuessRandomNumber(numGuest, ref numRandom);
            System.Console.WriteLine($"Winning number: {numRandom} - Your number: {numGuest} - {msg}");
            numGuest = 8;
            msg = basicForBeginners.GuessRandomNumber(numGuest, ref numRandom);
            System.Console.WriteLine($"Winning number: {numRandom} - Your number: {numGuest} - {msg}");

            //5.
            List<int> nums = new List<int>();
            System.Console.WriteLine($"Enter Number separated by comma");
            var rl = System.Console.ReadLine().Split(',');
            var ls = rl.Select(n => Convert.ToInt32(n)).ToList();

            System.Console.WriteLine($"Max Number: ({ls.Max()}).");

        }
        private static void ExecuteBasicForBegginersPrt3()
        {
            BasicForBeginners basicForBeginners = new BasicForBeginners();

            //1.
            List<string> friends = new List<string>();

            //while (true)
            //{
            //    System.Console.WriteLine($"Enter facebook friend name or press [Enter] to Exit");
            //    string readLine = System.Console.ReadLine();
            //    if (readLine.Equals("", StringComparison.CurrentCultureIgnoreCase))
            //    {
            //        break;

            //    }
            //    friends.Add(readLine);
            //}
            //System.Console.WriteLine($"{basicForBeginners.GetFacebookInfo(friends)}.");

            /*
             * 2- Write a program and ask the user to enter their name. Use an array to reverse the name 
             * and then store the result in a new string. Display the reversed name on the console.             
             */
            //System.Console.WriteLine($"Enter name or press [Enter] to Exit");
            //var readLine = System.Console.ReadLine().ToArray();
            //Array.Reverse(readLine);
            //System.Console.WriteLine(readLine);

            /*
             * 3- Write a program and ask the user to enter 5 numbers. If a number has been previously entered, 
             * display an error message and ask the user to re-try. Once the user successfully enters 5 
             * unique numbers, sort them and display the result on the console.
             */
            //List<int> numbers = new List<int>();

            //while (true)
            //{
            //    if (numbers?.Count >= 5) { break; }
            //    System.Console.WriteLine($"Enter 5 Number or press [Enter] to Exit");
            //    var readLine = Convert.ToInt32(System.Console.ReadLine());
            //    if (numbers.Contains(readLine) )
            //    {
            //        System.Console.WriteLine($"number {readLine} has been previously entered, re-try");
            //        continue;
            //    }


            //    numbers.Add(Convert.ToInt32(readLine));
            //}
            //numbers.Sort();
            //foreach (var num in numbers)
            //{
            //    System.Console.WriteLine(num);
            //}

            /*
             * 4- Write a program and ask the user to continuously enter a number or type "Quit" to exit. 
             * The list of numbers may include duplicates. Display the unique numbers that the user has entered.
             */
            //List<int> numbers = new List<int>();

            //while (true)
            //{
            //    System.Console.WriteLine($"Enter a Number or type [Quit] to Exit");
            //    var readLine = (System.Console.ReadLine());
            //    if (readLine.Equals("Quit", StringComparison.CurrentCultureIgnoreCase)) { break; }
            //    numbers.Add(Convert.ToInt32(readLine));
            //}
            //foreach (var num in numbers.Distinct())
            //{
            //    System.Console.WriteLine(num);
            //}

            /*
             * 5- Write a program and ask the user to supply a list of comma separated numbers (e.g 5, 1, 9, 2, 10). 
             * If the list is empty or includes less than 5 numbers, display "Invalid List" and ask the user to re-try;
             * otherwise, display the 3 smallest numbers in the list.
             */

            while (true)
            {
                System.Console.WriteLine("Enter a list of comma separated numbers (e.g 5, 1, 9, 2, 10)");
                var readline = System.Console.ReadLine();
                if (readline.Equals("Quit", StringComparison.InvariantCultureIgnoreCase)) { break; }

                if (string.IsNullOrWhiteSpace(readline))
                {
                    System.Console.WriteLine("Invalid List, re-try");
                    continue;
                }
                var rlArray = readline.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                if (rlArray?.Count() < 5)
                {
                    System.Console.WriteLine("Invalid List, re-try");
                    continue;
                }

                System.Console.WriteLine("the 3 smallest numbers:");
                Array.Sort(rlArray);
                foreach (var num in rlArray.Take(3))
                {
                    System.Console.WriteLine(num);
                }
            }
        }
    }
}