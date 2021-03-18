using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        static Dictionary<int, int> spokenNumbers = new Dictionary<int, int>();
        static int lastSpokenNumber = 0;
        static int turn = 1;

        static void Main(string[] args)
        {        
            int[] numbers = File.ReadAllText("input.txt").Split(",").Select(number => Convert.ToInt32(number)).ToArray();

            Console.WriteLine($"Teil 1: {Play(2020, numbers)}");  
            Console.WriteLine($"Teil 2: {Play(30000000, numbers)}");
        }

        static int Play(int turns, int[] numbers)
        {
            while(turn < turns)
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

            return lastSpokenNumber;
        }
    }
}
