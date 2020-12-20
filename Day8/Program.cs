using System;
using System.Collections.Generic;
using System.IO;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines("input.txt");

            Console.WriteLine($"Teil 1: {Process(instructions).Item2}");

            /* ##########
             * # Part 2 #
             * ##########
             */

            //Find all instructions which can be replaced
            List<int> replaceableInstructions = new List<int>();
            for (int i = 0; i < instructions.Length; i++)
            {
                string instruction = instructions[i].Substring(0, 3);
                if (instruction == "nop" || instruction == "jmp") replaceableInstructions.Add(i);
            }

            int currentReplacement = 0;
            while (replaceableInstructions.Count > 0)
            {
                int line = replaceableInstructions[0];

                //Switch
                instructions[line] = (instructions[line][0] == 'n')
                    ? instructions[line].Replace("nop", "jmp")
                    : instructions[line].Replace("jmp", "nop");

                var result = Process(instructions);
                if (result.Item1)
                {
                    Console.WriteLine($"Teil 2: {result.Item2}");
                    break;
                }
                //Switch back
                instructions[line] = (instructions[line][0] == 'n')
                    ? instructions[line].Replace("nop", "jmp")
                    : instructions[line].Replace("jmp", "nop");

                //Increase counter and remove line form list
                currentReplacement++;
                replaceableInstructions.RemoveAt(0);
            }

        }

        static (bool, int) Process(string[] instructions)
        {
            HashSet<int> executedInstructions = new HashSet<int>();
            int accumulator = 0;
            int programCounter = 0;

            while (executedInstructions.Contains(programCounter) == false)
            {
                executedInstructions.Add(programCounter);

                if (programCounter >= instructions.Length) return (true, accumulator);
                string line = instructions[programCounter];

                string instruction = line.Substring(0, 3);
                int number = ParseNumber(line.Substring(5, line.Length - 5), line[4]);

                if (instruction == "acc") accumulator += number;
                programCounter += (instruction == "jmp") ? number : 1;
            }

            return (false, accumulator);
        }

        static int ParseNumber(string input, char prefix)
        {
            int.TryParse(input, out int number);
            return (prefix == '+') ? number : number * -1;
        }
    }
}
