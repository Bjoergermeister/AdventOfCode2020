using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static List<int> input = null;
        static Dictionary<int, long> knownSteps = new Dictionary<int, long>();

        static void Main(string[] args)
        {
            Dictionary<int, int> joltageDifferences = new Dictionary<int, int>();

            input = File.ReadAllLines("input.txt").Select(number => Convert.ToInt32(number)).ToList();
            input.Insert(0, 0);
            input.Sort();
            input.Insert(input.Count, input[input.Count - 1] + 3);

            int currentJoltage = 0;
            foreach (int number in input)
            {
                int difference = number - currentJoltage;

                if (joltageDifferences.ContainsKey(difference)) joltageDifferences[difference]++;
                else joltageDifferences.Add(difference, 1);

                currentJoltage = number;
            }

            Console.WriteLine($"Teil 1: {joltageDifferences[1] * joltageDifferences[3]}");
     
            long totalWays = NumberOfWays(0);
            Console.WriteLine($"Teil 2: {totalWays}");
        }

        static long NumberOfWays(int index)
        {
            if (index == input.Count - 1) return 1;
            if (knownSteps.ContainsKey(index)) return knownSteps[index];

            long numberOfWays = 0;
            for(int i = index + 1; i < input.Count; i++)
            {
                if (input[i] - input[index] <= 3)
                {
                    numberOfWays += NumberOfWays(i);
                }
            }
            knownSteps.Add(index, numberOfWays);
            return numberOfWays;
        }
    }
}
