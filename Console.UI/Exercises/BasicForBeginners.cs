using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Exercises
{
    public class BasicForBeginners
    {
        /// <summary>
        /// 1- Write a program and ask the user to enter a number. The number should be between 1 to 10. 
        /// If the user enters a valid number, display "Valid" on the console. Otherwise, display "Invalid". 
        /// (This logic is used a lot in applications where values entered into input boxes need to be validated.)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string IsValidNumber(int number)
        {
            string result = "Invalid";

            if (number >= 1 && number <= 10)
            {
                result = "Valid";
            }

            return result;
        }

        /// <summary>
        /// 2- Write a program which takes two numbers from the console and displays the maximum of the two.
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int GetMaxNumber(int number1, int number2)
        {
            return (number1 >= number2) ? number1 : number2;
        }

        /// <summary>
        /// 3- Write a program and ask the user to enter the width and height of an image. 
        /// Then tell if the image is landscape or portrait.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public ImageOrientation GetImageOrientation(int width, int height)
        {
            return (width >= height) ? ImageOrientation.Landscape : ImageOrientation.Portrait;
        }

        /// <summary>
        /// 4- Your job is to write a program for a speed camera. 
        /// For simplicity, ignore the details such as camera, sensors, etc and focus purely on the logic. 
        /// Write a program that asks the user to enter the speed limit. Once set, the program asks for the speed of a car. 
        /// If the user enters a value less than the speed limit, program should display Ok on the console. 
        /// If the value is above the speed limit, the program should calculate the number of demerit points. 
        /// For every 5km/hr above the speed limit, 1 demerit points should be incurred and displayed on the console. 
        /// If the number of demerit points is above 12, the program should display License Suspended.
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public string GetSpeedCameraMsg(int carSpeed)
        {
            int speedLimit = 70;

            if (carSpeed < speedLimit)
                return ("Ok");
            else
            {
                const int kmPerDemeritPoint = 5;
                int demeritPoints = (carSpeed - speedLimit) / kmPerDemeritPoint;
                if (demeritPoints > 12)
                    return ("License Suspended");
                else
                    return ("Demerit points: " + demeritPoints);
            }

        }

        /// <summary>
        /// 1- Write a program to count how many numbers between 1 and 100 are divisible by 3 with no remainder. 
        /// Display the count on the console.
        /// </summary>
        /// <returns></returns>
        public int NumbersDivisibleByThree()
        {
            int count = 0;
            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 2- Write a program and continuously ask the user to enter a number or "ok" to exit. 
        /// Calculate the sum of all the previously entered numbers and display it on the console.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public int GetSum(List<int> numbers)
        {
            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return sum;
        }

        /// <summary>
        /// 3- Write a program and ask the user to enter a number. Compute the factorial of the number and print it on the console. 
        /// For example, if the user enters 5, the program should calculate 5 x 4 x 3 x 2 x 1 and display it as 5! = 120.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int Factorial(int number)
        {
            int result = 1;
            for (int i = number; i >= 1; i--)
            {
                result *= i;
            }

            return result;
        }

        /// <summary>
        /// 4- Write a program that picks a random number between 1 and 10. Give the user 4 chances to guess the number.
        /// If the user guesses the number, display “You won"; otherwise, display “You lost". (To make sure the program 
        /// is behaving correctly, you can display the secret number on the console first.)
        /// </summary>
        /// <param name="numGuest"></param>
        /// <param name="numRandom"></param>
        /// <returns></returns>
        public string GuessRandomNumber(int numGuest, ref int numRandom)
        {
            numRandom = (numRandom == 0) ? new Random().Next(1, 10) : numRandom;

            if (numRandom == numGuest)
            {
                return "You Won!";
            }
            return "You Lost!";
        }


        public string GetFacebookInfo(List<String> friends)
        {
            string fbInfo;
            if (friends?.Count == 1)
            {
                fbInfo = $"{friends[0]} likes your post";
            }
            else if (friends?.Count == 2)
            {
                fbInfo = $"{friends[0]} and {friends[1]} likes your post";
            }
            else
            {
                fbInfo = $"{friends[0]}, {friends[1]} and {friends.Count - 2} other like your post";
            }

            return fbInfo;
        }
    }
}