using System;
using System.Collections.Generic;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            HashSet<int> numbers = new HashSet<int>();
            int goal = 2020;

            foreach(var line in lines)
            {
                int number = Convert.ToInt32(line);
                
                if (numbers.Contains(number))
                {
                    Console.WriteLine(number * (goal - number));
                    break;
                }
                else
                {
                    numbers.Add(goal - number);
                }
            }
        }
    }
}
