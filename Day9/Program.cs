using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(number => Convert.ToInt64(number)).ToArray();

            long invalidNumber = FindInvalidNumber(input);
            Console.WriteLine($"Gesuchte Zahl: {invalidNumber}");

            //Find list of number that add up to invalidNumber
            List<long> sequence = new List<long>();
            for(int i = 0; i < input.Length - 1; i++)
            {
                bool foundSequence = false;
                long sum = 0;

                sequence.Clear();

                for(int j = i; j < input.Length; j++)
                {
                    sum += input[j];
                    sequence.Add(input[j]);

                    if (sum > invalidNumber) break;

                    if (sum == invalidNumber)
                    {
                        foundSequence = true;
                        break;
                    }
                }
                if (foundSequence) break;
            }

            //Find heighest und smallest number
            long smallest = sequence[0];
            long largest = sequence[0];            
            foreach(long l in sequence)
            {
                smallest = Math.Min(smallest, l);
                largest = Math.Max(largest, l);
            }

            Console.WriteLine($"Summe: {smallest + largest}");
        }

        static long FindInvalidNumber(long[] input)
        {
            HashSet<long> encounteredNumbers = new HashSet<long>();
            for (int i = 25; i < input.Length; i++)
            {
                encounteredNumbers.Clear();
                long searchedNumber = input[i];

                bool foundPair = false;
                for (int j = -25; j < 0; j++)
                {
                    long currentNumber = input[i + j];
                    if (encounteredNumbers.Contains(currentNumber))
                    {
                        foundPair = true;
                        break;
                    }
                    encounteredNumbers.Add(searchedNumber - currentNumber);
                }

                if (foundPair == false) return searchedNumber;
            }
            return 0;
        }
    }
}
