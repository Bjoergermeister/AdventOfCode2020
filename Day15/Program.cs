using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> spokenNumbers = new Dictionary<int, int>();
            int lastSpokenNumber = 0;
            int turn = 1;


            int[] numbers = File.ReadAllText("input.txt").Split(",").Select(number => Convert.ToInt32(number)).ToArray();
            

            while(turn < 2020)
            {
                int number = (turn <= numbers.Length) ? numbers[turn - 1] : lastSpokenNumber;

                if (spokenNumbers.ContainsKey(number))
                {
                    lastSpokenNumber = turn - spokenNumbers[number];
                    spokenNumbers[number] = turn;                    
                }
                else
                {
                    spokenNumbers.Add(number, turn);
                    lastSpokenNumber = 0;
                }
                turn++;                
            }

            Console.WriteLine(lastSpokenNumber);     
        }
    }
}
