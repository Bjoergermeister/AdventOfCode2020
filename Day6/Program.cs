using System;
using System.Collections.Generic;
using System.IO;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int positiveAnswers = 0;
            int groupStartIndex = 0;
            int groupEndIndex = 0;

            for(int i = 0; i <= input.Length; i++)
            {
                string line = (i < input.Length) ? input[i] : "";
                if (line.Length == 0)
                {
                    string[] copy = new string[groupEndIndex - groupStartIndex];
                    Array.Copy(input, groupStartIndex, copy, 0, groupEndIndex - groupStartIndex);
                    positiveAnswers += CountPositiveAnswersInGroup(copy);
                    groupStartIndex = i + 1;
                }

                groupEndIndex++;
            }

            Console.WriteLine(positiveAnswers);
        }

        static int CountPositiveAnswersInGroup(string[] group)
        {
            HashSet<char> positiveAnswers = new HashSet<char>();
            foreach (string person in group)
            {
                foreach (char c in person)
                {
                    if (positiveAnswers.Contains(c)) continue;
                    positiveAnswers.Add(c);
                }
            }
            return positiveAnswers.Count;
        }
    }
}
