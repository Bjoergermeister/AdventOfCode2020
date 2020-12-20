using System;
using System.Collections.Generic;
using System.IO;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            HashSet<long> encounteredNumbers = new HashSet<long>();
            for (int i = 25; i < input.Length; i++)
            {
                encounteredNumbers.Clear();
                long.TryParse(input[i], out long searchedNumber);

                bool foundPair = false;
                for (int j = -25; j < 0; j++)
                {
                    long.TryParse(input[i + j], out long currentNumber);
                    if (encounteredNumbers.Contains(currentNumber))
                    {
                        foundPair = true;
                        break;
                    }
                    encounteredNumbers.Add(searchedNumber - currentNumber);
                }

                if (foundPair == false) Console.WriteLine(searchedNumber);
            }
        }
    }
}
