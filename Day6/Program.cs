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

            int positiveAnswersByAnyPerson = 0;
            int positiveAnswersByEveryPerson = 0;
            int groupStartIndex = 0;
            int groupEndIndex = 0;

            for(int i = 0; i <= input.Length; i++)
            {
                string line = (i < input.Length) ? input[i] : "";
                if (line.Length == 0)
                {
                    string[] copy = new string[groupEndIndex - groupStartIndex];
                    Array.Copy(input, groupStartIndex, copy, 0, groupEndIndex - groupStartIndex);
                    positiveAnswersByAnyPerson += CountPositiveAnswersByAnyPerson(copy);
                    positiveAnswersByEveryPerson += CountPositiveAnswersByEveryPerson(copy);
                    groupStartIndex = i + 1;
                }

                groupEndIndex++;
            }

            Console.WriteLine("Positive Antworten von einigen: " + positiveAnswersByAnyPerson);
            Console.WriteLine("Positive Antworten von allen: " + positiveAnswersByEveryPerson);
        }

        static int CountPositiveAnswersByAnyPerson(string[] group)
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

        static int CountPositiveAnswersByEveryPerson(string[] group)
        {
            Dictionary<char, int> positiveAnswers = new Dictionary<char, int>();
            foreach(string person in group)
            {
                //Count positive answers to each question
                foreach(char c in person)
                {
                    if (positiveAnswers.ContainsKey(c))
                    {
                        positiveAnswers[c]++;
                    }
                    else
                    {
                        positiveAnswers.Add(c, 1);
                    }
                }
            }

            //Check for how many answers the count is as high as the number of members of the group
            int positiveAnswersByAll = 0;
            foreach(int answersCount in positiveAnswers.Values)
            {
                if (answersCount == group.Length) positiveAnswersByAll++;
            }

            return positiveAnswersByAll;
        }
    }
}
