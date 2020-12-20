using System;
using System.Collections.Generic;
using System.IO;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            int accumulator = 0;
            int programmCounter = 0;
            HashSet<int> executedInstructions = new HashSet<int>();

            string[] instructions = File.ReadAllLines("input.txt");

            while (executedInstructions.Contains(programmCounter) == false)
            {
                executedInstructions.Add(programmCounter);

                string line = instructions[programmCounter];

                string instruction = line.Substring(0, 3);
                int number = ParseNumber(line.Substring(5, line.Length - 5), line[4]);

                if (instruction == "acc") accumulator += number;
                programmCounter += (instruction == "jmp") ? number : 1;
            }

            Console.WriteLine(accumulator);
        }

        static int ParseNumber(string input, char prefix)
        {
            int.TryParse(input, out int number);
            return (prefix == '+') ? number : number * -1;
        }
    }
}
