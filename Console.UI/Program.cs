


using Console.UI.Exercises;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console.UI
{
    using Console.UI.Exercises.Intermediate;
    using System;
    using System.Threading;

    class Program
    {

        public static void Main(string[] args)
        {
            var srcPath = @"E:\My Pictures\Leo\";
            var destFilePath = @"E:\My Videos\Leo\";

            ExecuteProcessFiles(srcPath, destFilePath, FileType.Videos);


            //var s = new Solution2();
            //var cell = s.topNCompetitors(5, 2, new List<string> { "anacell", "betacellular", "cetracular", "deltacellular", "eurocell" }, 5,
            //    new List<string> {
            //        "I love anacell Best services provided by anacell in the town",
            //        "betacellular has great services",
            //        "deltacellular provides much better services than betacellular",
            //    "cetracular is worse than eurocell",
            //    "betacellular is better than deltacellular"}
            //    );
            //try
            //{
            //    //ExecuteProcessFiles();
            //    //ExecuteBasicForBegginersPrt1();
            //    //ExecuteBasicForBegginersPrt2();
            //    //ExecuteBasicForBegginersPrt3();
            //    //ExecuteBasicForBegginersPrt4();
            //    //ExecuteBasicForBegginersPrt5();

            //    //ExecuteIntermediatePrt1();
            //    //ExecuteIntermediatePrt2();
            //    ExecuteIntermediatePrt3();

            //    System.Console.WriteLine("{0} says, hello world!", "Leo");
            //    System.Console.Read();
            //}
            //catch (Exception ex)
            //{
            //    System.Console.WriteLine(ex.Message);
            //    System.Console.Read();
            //}
        }
        public static void ExecuteProcessFiles(string srcPath, string destFilePath, FileType fileType)
        {

            ProcessFiles pf = new ProcessFiles();
            var fileBatch = 8500;

            if (FileType.Photos == fileType)
            {
                pf.MoveFiles(srcPath, destFilePath, fileBatch, SearchOption.TopDirectoryOnly);
            }

            else
            {
                string[] videoExtentions = new string[] { "MP4", "AVI", "MOV", "FLV", "WMV", "MPG", "3G2", "3GP" };
                pf.DirectorySearch(srcPath, destFilePath, fileBatch, videoExtentions, SearchOption.TopDirectoryOnly);
                pf.MoveMovies(srcPath, destFilePath, fileBatch, videoExtentions, SearchOption.TopDirectoryOnly);
            }

            //System.Console.WriteLine("Process Completed");
            //System.Console.Read();

        }

        //        private static void ExecuteIntermediatePrt3()
        //        {
        //            /*
        //             * Exercise: Design a workflow engine
        //             * 
        //             * Design a workflow engine that takes a workflow object and runs it. A workflow is a series of steps or activities. 
        //             * The workflow engine class should have one method called Run() that takes a workflow, and then iterates over each 
        //             * activity in the workflow and runs.
        //             * 
        //             * We want our workflows to be extensible, so we can create new activities 
        //             * without impacting the existing activities. 
        //             * 
        //             * Educational tip: we should represent the concept of an activity using an interface. Each activity 
        //             * should have a method called Execute(). The workflow engine does not care about the concrete implementation of 
        //             * activities. All it cares about is that these activities have a common interface: they provide a method called Execute(). 
        //             * The engine simply calls this method and this way it executes a series of activities in sequence. 
        //             * 
        //             * The aim of this exercise is to help you understand how you can use interfaces to design extensible applications.
        //             * You change the behaviour of your application by creating new classes, rather than changing the existing classes. 
        //             * You’ll also see polymorphic behaviour of interfaces. 
        //             * 
        //             * Real-world use case: in a real-world application you may use a workflow in a scenario like the following:
        //             * 
        //             * 1- Upload a video to a cloud storage. 
        //             * 2- Call a web service provided by a third-party video encoding service to tell them you have a video ready for encoding. 
        //             * 3- Send an email to the owner of the video notifying them that the video started processing. 
        //             * 4- Change the status of the video record in the database to “Processing”.
        //             * 
        //             * Each of these steps can be represented by an activity. For the purpose of this exercise, 
        //             * do not worry about these complexities. Simply use Console.WriteLine() in each of your activity classes. 
        //             * Your focus should be on sending a workflow to the workflow engine and having it run the workflow 
        //             * and all the activities inside it. We don’t care about the actual activities. 2
        //             */

        //            var workflow = new WorkFlow();
        //            workflow.Add(new RunNormal());
        //            workflow.Add(new RunFast());
        //            workflow.Add(new WalkNormal());
        //            workflow.Add(new WalkSlow());

        //            var engine = new WorkflowEngine();
        //            engine.Run(workflow);
        //        }

        //        private static void ExecuteIntermediatePrt2()
        //        {
        //            /*
        //             * Exercise 1: Design a database connectionTo access a database, we need to open 
        //             * a connection to it first and close it once our job is done. Connecting to a 
        //             * database depends on the type of the target database and the database management 
        //             * system (DBMS). For example, connecting to a SQL Server database is different from 
        //             * connecting to an Oracle database. But both these connections have a few things in common: 
        //             * 
        //             * •They have a connection string 
        //             * •They can be opened
        //             * •They can be closed             * 
        //             * •They may have a timeout attribute (so if the connection could not be opened within the timeout, an exception will be thrown).
        //             * 
        //             * Your job is to represent these commonalities in a base class called DbConnection. 
        //             * This class should have two properties: 
        //             * 
        //             * ConnectionString : * string
        //             * Timeout : TimeSpan
        //             * 
        //             * A DbConnection will not be in a valid state if it doesn’t have a connection string. 
        //             * So you need to pass a connection string in the constructor of this class. Also, 
        //             * take into account the scenarios where null or an empty string is sent as the connection 
        //             * string. Make sure to throw an exception to guarantee that your class will always be in a valid state.
        //             * 
        //             * Our DbConnection should also have two methods for opening and closing a connection. We don’t 
        //             * know how to open or close a connection in a DbConnection and this should be left to the 
        //             * classes that derive from DbConnection. These classes (eg SqlConnection or OracleConnection) 
        //             * will provide the actual implementation. So you need to declare these methods as abstract.
        //             * 
        //             * Derive * two classes SqlConnection and OracleConnection from DbConnection and provide a simple implementation
        //             * of opening and closing connections using Console.WriteLine(). In the real-world, SQL Server provides an 
        //             * API for opening or closing a connection to a database. But for this exercise, we don’t need to worry about it.
        //             */

        //            //var sqlConn = new SqlConnection("connectionString");
        //            //sqlConn.OpenConnection();
        //            //sqlConn.CloseConnection();

        //            //var oracleConn = new OracleConnection("connectionString");
        //            //oracleConn.OpenConnection();
        //            //oracleConn.CloseConnection();

        //            /* Exercise 2: Design a database command

        //             * Now that we have the concept of a DbConnection, let’s work out how to represent a DbCommand. 
        //             * 
        //             * Design a class called DbCommand for executing an instruction against the database. A 
        //             * DbCommand cannot be in a valid state without having a connection. So in the constructor of 
        //             * this class, pass a DbConnection. Don’t forget to cater for the null.
        //             * 
        //             * Each DbCommand should also have the instruction to be sent to the database. In case of SQL 
        //             * Server, this instruction is expressed in T-SQL language. Use a string to represent this instruction. 
        //             * Again, a command cannot be in a valid state without this instruction. So make sure to receive it 
        //             * in the constructor and cater for the null reference or an empty string. 
        //             * 
        //             * Each command should be executable. So we need to create a method called Execute().
        //             * In this method, we need a simple implementation as follows: 
        //             * 
        //             * Open the connection
        //             * Run the instruction 
        //             * Close the connection
        //             * 
        //             * Note that here, inside the DbCommand, we have a reference to DbConnection. Depending on 
        //             * the type of DbConnection sent at runtime, opening and closing a connection will be different. 
        //             * For example, if we initialize this DbCommand with a SqlConnection, we will open and close a 
        //             * connection to a Sql Server database. This is polymorphism. Interestingly, DbCommand
        //             * doesn’t care about how a connection is opened or closed. It’s not the responsibility of the 
        //             * DbCommand. All it cares about is to send an instruction to a database.
        //             * 
        //             * For running the instruction, simply output it to the Console. In the real-world, SQL Server (or any 
        //             * other DBMS) provides an API for running an instruction against the database. We don’t need to 
        //             * worry about it for this exercise. 
        //             * 
        //             * In the main method, initialize a DbCommand with some string as the instruction and a 
        //             * SqlConnection. Execute the command and see the result on the console.
        //             * 
        //             * Then, swap the SqlConnection with an OracleConnection and see polymorphism in action.
        //             * 
        //             */

        //            var dbCommand = new DbCommand(new SqlConnection("Sql"), "Cool Sql Query");
        //            dbCommand.Execute();

        //            dbCommand = new DbCommand(new OracleConnection("Oracle"), "Cool Oracle Query");
        //            dbCommand.Execute();

        //        }

        //        private static void ExecuteIntermediatePrt1()
        //        {

        //            /*
        //             * Exercise 1: Design a Stopwatch
        //             * Design a class called Stopwatch. The job of this class is to simulate a stopwatch. It should provide two methods: 
        //             * Start and Stop. We call the start method first, and the stop method next. Then we ask the stopwatch about the 
        //             * duration between start and stop. Duration should be a value in TimeSpan. Display the duration on the console. 
        //             * 
        //             * We should also be able to use a stopwatch multiple times. So we may start and stop it and then start and stop 
        //             * it again. Make sure the duration value each time is calculated properly.
        //             * 
        //             * We should not be able to start a stopwatch twice in a row (because that may overwrite the initial start time). 
        //             * So the class should throw an InvalidOperationException if its started twice 
        //             */
        //            //var stopwatch = new Stopwatch();
        //            //stopwatch.Start();
        //            //Thread.Sleep(1000);
        //            //Console.WriteLine($"Duration: {stopwatch.Stop()}");

        //            /*
        //             * Exercise 2: Design a StackOverflow Post
        //             * Design a class called Post. This class models a StackOverflow post. It should have properties for title, 
        //             * description and the date/time it was created. We should be able to up-vote or down-vote a post. 
        //             * We should also be able to see the current vote value. In the main method, create a post, 
        //             * up-vote and down-vote it a few times and then display the the current vote value. 
        //             * 32.
        //025742             * You should not give the ability to set the Vote property from the outside, because otherwise, you may 
        //             * accidentally change the votes of a class to 0 or to a random number. And this is how we create bugs in 
        //             * our programs. The class should always protect its state and hide its implementation detail.  
        //             */

        //            //var post = new Post { title = "Cool post", description = "Coolest post ever" };
        //            //for (int i = 0; i < 100; i++)
        //            //{
        //            //    post.UpVote();
        //            //    if (i % 7 == 0)
        //            //        post.DownVote();

        //            //}
        //            //System.Console.WriteLine($"Post title: {post.title} - description: {post.description} - created date: {post.createdDate} - vote count: {post.voteCount}");

        //            /*
        //             * Exercise: Design a Stack
        //             * A Stack is a data structure for storing a list of elements in a LIFO (last in, first out) fashion. 
        //             * 
        //             * Design a class called Stack with three methods. 
        //             * 
        //             * void Push(object obj)
        //             * object Pop()
        //             * void Clear()
        //             * 
        //             * The Push() method stores the given object on top of the stack. We use the “object” 
        //             * type here so we can store any objects inside the stack. Remember the “object” class is 
        //             * the base of all classes in the .NET Framework. So any types can be automatically upcast 
        //             * to the object. Make sure to take into account the scenario that null is passed to 
        //             * this object. We should not store null references in the stack. So if null is passed to 
        //             * this method, you should throw an InvalidOperationException. Remember, when coding every 
        //             * method, you should think of all possibilities and make sure the method behaves properly 
        //             * in all these edge cases. That’s what distinguishes you from an “average” programmer. 
        //             * 
        //             * The Pop() method removes the object on top of the stack and returns it. Make sure to take 
        //             * into account the scenario that we call the Pop() method on an empty stack. In this case, 
        //             * this method should throw an InvalidOperationException. Remember, your classes should always 
        //             * be in a valid state and used properly. When they are misused, they should throw exceptions. 
        //             * Again, thinking of all these edge cases, separates you from an average programmer. 
        //             * The code written this way will be more robust and with less bugs. 
        //             * 
        //             * The Clear() method removes all objects from the stack. 
        //             * 
        //             * We should be able to use this stack class as follows:
        //             * 
        //             * var stack = new Stack();
        //             * stack.Push(1);
        //             * stack.Push(2);
        //             * stack.Push(3);
        //             * Console.WriteLine(stack.Pop());
        //             * Console.WriteLine(stack.Pop());
        //             * Console.WriteLine(stack.Pop());
        //             * 
        //             * The output of this program will be 
        //             * 3
        //             * 2
        //             * 1
        //             */

        //            //var stack = new Stack();

        //            //int ini = 0;
        //            //int max = 5;

        //            //for (int i = ini; i <= max; i++)
        //            //{
        //            //    stack.Push(i);
        //            //}

        //            //stack.Clear();

        //            //for (int i = ini; i <= max; i++)
        //            //{
        //            //    Console.WriteLine(stack.Pop());
        //            //}

        //        }
        //        private static void ExecuteBasicForBegginersPrt5()
        //        {
        //            /*
        //             * 1- Write a program that reads a text file and displays the number of words.
        //             */
        //            //{
        //            //    System.Console.WriteLine($"Enter a text file path");
        //            //    var readline = System.Console.ReadLine();

        //            //    var text = File.ReadAllText(readline);


        //            //    System.Console.WriteLine($"The number of words: { text.Split(' ').Count()}");
        //            //}
        //            /*
        //             * 2- Write a program that reads a text file and displays the longest word in the file.
        //             */
        //            {
        //                System.Console.WriteLine($"Enter a text file path");
        //                var readline = System.Console.ReadLine();

        //                var text = File.ReadAllText(readline);

        //                var allWords = text.Split(' ');
        //                var maxLetterCount = allWords.Max(w => w.Count());
        //                var maxWord = allWords.FirstOrDefault(w => w.Length == maxLetterCount);



        //                System.Console.WriteLine($"The longest word in the file: { maxWord }");
        //            }
        //        }
        //        private static void ExecuteBasicForBegginersPrt4()
        //        {
        //            /*
        //             * 1- Write a program and ask the user to enter a few numbers separated by a hyphen. 
        //             * Work out if the numbers are consecutive. For example, if the input is "5-6-7-8-9" or 
        //             * "20-19-18-17-16", display a message: "Consecutive"; otherwise, display "Not Consecutive".
        //             */
        //            //{
        //            //    System.Console.WriteLine($"Enter Number separated by hyphen");
        //            //    var readline = System.Console.ReadLine().Split('-').Select(n => Convert.ToInt32(n)).ToArray();

        //            //    var copyReadline = new int[readline.Length];
        //            //    Array.Copy(readline, copyReadline, readline.Length);
        //            //    Array.Sort(copyReadline);

        //            //    if (readline.SequenceEqual(copyReadline) || readline.Reverse().SequenceEqual(copyReadline))
        //            //    {
        //            //        System.Console.WriteLine("Consecutive");
        //            //    }
        //            //    else
        //            //    {
        //            //        System.Console.WriteLine("Not Consecutive");
        //            //    }
        //            //}
        //            /*
        //             * 2- Write a program and ask the user to enter a few numbers separated by a hyphen. 
        //             * If the user simply presses Enter, without supplying an input, exit immediately; 
        //             * otherwise, check to see if there are duplicates. If so, display "Duplicate" on the console.
        //             */
        //            //{
        //            //    while (true)
        //            //    {
        //            //        System.Console.WriteLine($"Enter Number separated by hyphen");
        //            //        var readline = System.Console.ReadLine();
        //            //        var array = readline.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
        //            //        if (string.IsNullOrWhiteSpace(readline)) break;

        //            //        var groupNumberByCount = array.GroupBy(n => n).Select(number => new
        //            //        {
        //            //            Number = number.Key,
        //            //            Count = number.Count()
        //            //        });

        //            //        StringBuilder sb = new StringBuilder();
        //            //        sb.AppendLine("Duplicate - Number : Count");
        //            //        foreach (var number in groupNumberByCount)
        //            //        {
        //            //            if (number.Count > 1)
        //            //            {
        //            //                sb.AppendLine($"{number.Number} : {number.Count}");
        //            //            }
        //            //        }

        //            //        if (!string.IsNullOrWhiteSpace(sb.ToString()))
        //            //        {
        //            //            System.Console.WriteLine(sb.ToString());
        //            //        }
        //            //    }
        //            //}
        //            /*
        //             * 3 - Write a program and ask the user to enter a time value in the 24 - hour time format(e.g. 19:00). 
        //             * A valid time should be between 00:00 and 23:59.If the time is valid, display "Ok"; otherwise, display 
        //             * "Invalid Time".If the user doesn't provide any values, consider it as invalid time. 
        //             */

        //            //{
        //            //    System.Console.WriteLine($"Enter a time value in the 24-hour time format (e.g. 19:00)");
        //            //    var readline = System.Console.ReadLine();
        //            //    TimeSpan ts;
        //            //    if (TimeSpan.TryParse(readline, out ts) && readline.Contains(":"))
        //            //    {
        //            //        System.Console.WriteLine("Ok");
        //            //    }
        //            //    else
        //            //    {
        //            //        System.Console.WriteLine("Invalid Time");
        //            //    }

        //            //}
        //            /*
        //             * 4 - Write a program and ask the user to enter a few words separated by a space.
        //             * Use the words to create a variable name with PascalCase. For example, if the user types: 
        //             * "number of students", display "NumberOfStudents".Make sure that the program is not 
        //             * dependent on the input.So, if the user types "NUMBER OF STUDENTS", the program should still display "NumberOfStudents".
        //             */
        //            //{
        //            //    System.Console.WriteLine($"Enter a few words separated by a space");
        //            //    var readline = System.Console.ReadLine();
        //            //    var titleCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(readline.ToLower());

        //            //    System.Console.WriteLine(titleCase.Replace(" ", string.Empty));
        //            //}

        //            /*
        //             * 5 - Write a program and ask the user to enter an English word.Count the number of vowels(a, e, o, u, i) 
        //             * in the word. So, if the user enters "inadequate", the program should display 6 on the console.
        //             */
        //            {
        //                System.Console.WriteLine($"Enter an English word");
        //                var readline = System.Console.ReadLine().ToLower().ToCharArray();
        //                int count = 0;

        //                var charArray = new[] { 'a', 'e', 'o', 'u', 'i' };
        //                foreach (var c in charArray)
        //                {
        //                    count += readline.Count(w => w.Equals(c));
        //                }

        //                System.Console.WriteLine($"The number of vowels in the word: {count}");
        //            }

        //        }
        //        private static void ExecuteBasicForBegginersPrt3()
        //        {
        //            BasicForBeginners basicForBeginners = new BasicForBeginners();

        //            //1.
        //            List<string> friends = new List<string>();

        //            //while (true)
        //            //{
        //            //    System.Console.WriteLine($"Enter facebook friend name or press [Enter] to Exit");
        //            //    string readLine = System.Console.ReadLine();
        //            //    if (readLine.Equals("", StringComparison.CurrentCultureIgnoreCase))
        //            //    {
        //            //        break;

        //            //    }
        //            //    friends.Add(readLine);
        //            //}
        //            //System.Console.WriteLine($"{basicForBeginners.GetFacebookInfo(friends)}.");

        //            /*
        //             * 2- Write a program and ask the user to enter their name. Use an array to reverse the name 
        //             * and then store the result in a new string. Display the reversed name on the console.             
        //             */
        //            //System.Console.WriteLine($"Enter name or press [Enter] to Exit");
        //            //var readLine = System.Console.ReadLine().ToArray();
        //            //Array.Reverse(readLine);
        //            //System.Console.WriteLine(readLine);

        //            /*
        //             * 3- Write a program and ask the user to enter 5 numbers. If a number has been previously entered, 
        //             * display an error message and ask the user to re-try. Once the user successfully enters 5 
        //             * unique numbers, sort them and display the result on the console.
        //             */
        //            //List<int> numbers = new List<int>();

        //            //while (true)
        //            //{
        //            //    if (numbers?.Count >= 5) { break; }
        //            //    System.Console.WriteLine($"Enter 5 Number or press [Enter] to Exit");
        //            //    var readLine = Convert.ToInt32(System.Console.ReadLine());
        //            //    if (numbers.Contains(readLine) )
        //            //    {
        //            //        System.Console.WriteLine($"number {readLine} has been previously entered, re-try");
        //            //        continue;
        //            //    }


        //            //    numbers.Add(Convert.ToInt32(readLine));
        //            //}
        //            //numbers.Sort();
        //            //foreach (var num in numbers)
        //            //{
        //            //    System.Console.WriteLine(num);
        //            //}

        //            /*
        //             * 4- Write a program and ask the user to continuously enter a number or type "Quit" to exit. 
        //             * The list of numbers may include duplicates. Display the unique numbers that the user has entered.
        //             */
        //            //List<int> numbers = new List<int>();

        //            //while (true)
        //            //{
        //            //    System.Console.WriteLine($"Enter a Number or type [Quit] to Exit");
        //            //    var readLine = (System.Console.ReadLine());
        //            //    if (readLine.Equals("Quit", StringComparison.CurrentCultureIgnoreCase)) { break; }
        //            //    numbers.Add(Convert.ToInt32(readLine));
        //            //}
        //            //foreach (var num in numbers.Distinct())
        //            //{
        //            //    System.Console.WriteLine(num);
        //            //}

        //            /*
        //             * 5- Write a program and ask the user to supply a list of comma separated numbers (e.g 5, 1, 9, 2, 10). 
        //             * If the list is empty or includes less than 5 numbers, display "Invalid List" and ask the user to re-try;
        //             * otherwise, display the 3 smallest numbers in the list.
        //             */

        //            while (true)
        //            {
        //                System.Console.WriteLine("Enter a list of comma separated numbers (e.g 5, 1, 9, 2, 10)");
        //                var readline = System.Console.ReadLine();
        //                if (readline.Equals("Quit", StringComparison.InvariantCultureIgnoreCase)) { break; }

        //                if (string.IsNullOrWhiteSpace(readline))
        //                {
        //                    System.Console.WriteLine("Invalid List, re-try");
        //                    continue;
        //                }
        //                var rlArray = readline.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        //                if (rlArray?.Count() < 5)
        //                {
        //                    System.Console.WriteLine("Invalid List, re-try");
        //                    continue;
        //                }

        //                System.Console.WriteLine("the 3 smallest numbers:");
        //                Array.Sort(rlArray);
        //                foreach (var num in rlArray.Take(3))
        //                {
        //                    System.Console.WriteLine(num);
        //                }
        //            }
        //        }
        //        private static void ExecuteBasicForBegginersPrt2()
        //        {
        //            BasicForBeginners basicForBeginners = new BasicForBeginners();

        //            //1.
        //            var count = basicForBeginners.NumbersDivisibleByThree();
        //            System.Console.WriteLine($"numbers between 1 and 100 are divisible by 3: {count}.");

        //            //2.
        //            int num = 4;
        //            var factorial = basicForBeginners.Factorial(num);
        //            System.Console.WriteLine($"Factorial({num}): {factorial}.");

        //            //3.
        //            List<int> numbers = new List<int>();

        //            while (true)
        //            {
        //                System.Console.WriteLine($"Enter Number to [ADD] or [OK] to Exit");
        //                string readLine = System.Console.ReadLine();
        //                if (readLine.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
        //                {
        //                    break;

        //                }
        //                numbers.Add(Convert.ToInt32(readLine));
        //            }
        //            System.Console.WriteLine($"The Sum: ({numbers.Sum()}).");


        //            //4.
        //            int numRandom = 0;
        //            int numGuest = 2;
        //            var msg = basicForBeginners.GuessRandomNumber(numGuest, ref numRandom);
        //            System.Console.WriteLine($"Winning number: {numRandom} - Your number: {numGuest} - {msg}");
        //            numGuest = 5;
        //            msg = basicForBeginners.GuessRandomNumber(numGuest, ref numRandom);
        //            System.Console.WriteLine($"Winning number: {numRandom} - Your number: {numGuest} - {msg}");
        //            numGuest = 8;
        //            msg = basicForBeginners.GuessRandomNumber(numGuest, ref numRandom);
        //            System.Console.WriteLine($"Winning number: {numRandom} - Your number: {numGuest} - {msg}");

        //            //5.
        //            List<int> nums = new List<int>();
        //            System.Console.WriteLine($"Enter Number separated by comma");
        //            var rl = System.Console.ReadLine().Split(',');
        //            var ls = rl.Select(n => Convert.ToInt32(n)).ToList();

        //            System.Console.WriteLine($"Max Number: ({ls.Max()}).");

        //        }
        //        public static void ExecuteBasicForBegginersPrt1()
        //        {
        //            BasicForBeginners basicForBeginners = new BasicForBeginners();

        //            //1.
        //            for (int n = 5; n < 11; n++)
        //            {
        //                var msg = basicForBeginners.IsValidNumber(n);
        //                System.Console.WriteLine($"Number {n} is {msg}.");
        //            }

        //            //2.
        //            int number1 = 1, number2 = 2;
        //            var maxNumber = basicForBeginners.GetMaxNumber(number1, number2);
        //            System.Console.WriteLine($"From Number {number1} and {number2}, {maxNumber} is the max number.");
        //            number1 = 1; number2 = 1;
        //            maxNumber = basicForBeginners.GetMaxNumber(number1, number2);
        //            System.Console.WriteLine($"From Number {number1} and {number2}, {maxNumber} is the max number.");
        //            number1 = 3; number2 = 2;
        //            maxNumber = basicForBeginners.GetMaxNumber(number1, number2);
        //            System.Console.WriteLine($"From Number {number1} and {number2}, {maxNumber} is the max number.");

        //            //3.
        //            System.Console.WriteLine("Enter Image width and height (width,height)");
        //            var widthHeight = System.Console.ReadLine().Split(',');
        //            int width = Convert.ToInt32(widthHeight[0]);
        //            int height = Convert.ToInt32(widthHeight[1]);

        //            var imageOrientation = basicForBeginners.GetImageOrientation(width, height);
        //            System.Console.WriteLine($"Image Orientation {imageOrientation}.");

        //            //4.
        //            int i = 65;
        //            var message = basicForBeginners.GetSpeedCameraMsg(i);
        //            System.Console.WriteLine($"Number {i} is {message}.");
        //            i = 75;
        //            message = basicForBeginners.GetSpeedCameraMsg(i);
        //            System.Console.WriteLine($"Number {i} is {message}.");
        //            i = 130;
        //            message = basicForBeginners.GetSpeedCameraMsg(i);
        //            System.Console.WriteLine($"Number {i} is {message}.");
        //        }
        //    }

        //    // IMPORT LIBRARY PACKAGES NEEDED BY YOUR PROGRAM
        //    // SOME CLASSES WITHIN A PACKAGE MAY BE RESTRICTED
        //    // DEFINE ANY CLASS AND METHOD NEEDED
        //    // CLASS BEGINS, THIS CLASS IS REQUIRED
        //    public class Solution
        //    {
        //        //METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        //        public int[] cellCompete(int[] states, int days)
        //        {
        //            var houseCount = states.Length;
        //            var newStates = new int[houseCount];
        //            // INSERT YOUR CODE HERE
        //            for (int i = 0; i < days; i++)
        //            {
        //                for (int n = 0; n < houseCount; n++)
        //                {
        //                    var houseLeft = (n == 0) ? 0 : states[n - 1];
        //                    var houseRight = (n == (houseCount - 1)) ? 0 : states[n + 1];

        //                    newStates[n] =
        //                     ((houseLeft == 0 && houseRight == 0) || (houseLeft == 1 && houseRight == 1)) ? 0 : 1;
        //                }
        //                for (int n = 0; n < houseCount; n++)
        //                {
        //                    states[n] = newStates[n];
        //                }
        //            }

        //            return states;

        //        }
        //        // METHOD SIGNATURE ENDS

        //    }

        //    // IMPORT LIBRARY PACKAGES NEEDED BY YOUR PROGRAM
        //    // SOME CLASSES WITHIN A PACKAGE MAY BE RESTRICTED
        //    // DEFINE ANY CLASS AND METHOD NEEDED
        //    // CLASS BEGINS, THIS CLASS IS REQUIRED
        //    public class GCD
        //    {
        //        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        //        public int generalizedGCD(int num, int[] arr)
        //        {
        //            var gdc = arr[0];
        //            for (int i = 1; i < num; i++)
        //            {
        //                gdc = GetGdc(arr[i], gdc);
        //            }

        //            return gdc;

        //        }
        //        // METHOD SIGNATURE ENDS

        //        public int GetGdc(int one, int two)
        //        {
        //            if (one == 0)
        //            {
        //                return two;
        //            }

        //            return GetGdc(two % one, one);
        //        }
        //    }

        //    // IMPORT LIBRARY PACKAGES NEEDED BY YOUR PROGRAM
        //    // SOME CLASSES WITHIN A PACKAGE MAY BE RESTRICTED
        //    // DEFINE ANY CLASS AND METHOD NEEDED
        //    // CLASS BEGINS, THIS CLASS IS REQUIRED
        //    public class Solution2
        //    {
        //        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        //        public List<string> topNCompetitors(int numCompetitors,
        //                                            int topNCompetitors,
        //                                            List<string> competitors,
        //                                            int numReviews, List<string> reviews)
        //        {
        //            // WRITE YOUR CODE HERE

        //            var competitorRank = new Dictionary<string, int>();

        //            foreach (var competitor in competitors)
        //            {
        //                var rank = 0;
        //                foreach (var review in reviews)
        //                {
        //                    rank += CountString(review, competitor);
        //                }


        //                competitorRank.Add(competitor, rank);
        //            }

        //            var competitorDiscussCount = competitorRank.Values.Sum();

        //            var result = competitorRank.OrderByDescending(rank => rank.Value).ThenByDescending(name => name.Key).Select(t => t.Key);
        //            if (topNCompetitors < competitorDiscussCount)
        //                return result.Take(topNCompetitors).ToList();
        //            else
        //                return result.Take(competitorDiscussCount).ToList();


        //        }
        //        // METHOD SIGNATURE ENDS

        //        public int CountString(string str, string pattern)
        //        {
        //            int count = 0;
        //            int i = 0;
        //            while ((i = str.IndexOf(pattern, i)) != -1)
        //            {
        //                i += pattern.Length;
        //                count++;
        //            }
        //            return count;
        //        }
    }
}
