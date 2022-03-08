using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19
{
    class Program
    {
        static int index = 0;
        static Dictionary<int, List<List<object>>> rules = new Dictionary<int, List<List<object>>>();
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            //Extract rules
            do
            {
                string s = input[index];
                int colonIndex = s.IndexOf(':');
                int number = Convert.ToInt32(s.Substring(0, colonIndex));

                List<List<object>> subrules = new List<List<object>>();
                foreach (string part in s.Substring(colonIndex + 1, s.Length - colonIndex - 1).Split('|'))
                {
                    var subrule = Regex.Matches(part, @"\d+").Select(m => (object)Convert.ToInt32(m.Value)).ToList();
                    if (subrule.Count == 0)
                    {
                        subrule.Add(part.Trim()[1]);
                    }
                    subrules.Add(subrule);
                }
                rules.Add(number, subrules);

                index++;
            } while (input[index].Length > 0);

            Console.WriteLine($"Puzzle 1: {Puzzle1(input)}");

            //Change rules            
            rules[8] = new List<List<object>>() {
                new List<object>{ 42 },
                new List<object>{ 42, 8}
            };
            rules[11] = new List<List<object>>(){
                new List<object>{ 42, 31 },
                new List<object>{ 42, 11, 31 }
            };

            Console.WriteLine($"Puzzle 2: {Puzzle1(input)}");
        }

        static int Puzzle1(string[] input)
        {
            int validMessages = 0;
            for (int i = index + 1; i < input.Length; i++)
            {
                List<int> choices = FitsRule(0, 0, input[i]);
                if (choices.Count > 0 && choices.Any())
                {
                    foreach (int choice in choices)
                    {
                        if (choice == input[i].Length)
                        {
                            validMessages++;
                        }
                    }
                }
            }
            return validMessages;
        }

        static List<int> FitsRule(int ruleIndex, int choice, string s)
        {
            List<int> newChoices = new List<int>();
            foreach (List<object> subrule in rules[ruleIndex])
            {
                List<int> currentChoices = new List<int>() { choice };
                foreach (object rule in subrule)
                {
                    if (rule is Char)
                    {
                        for (int i = currentChoices.Count - 1; i >= 0; i--)
                        {
                            if (currentChoices[i] < s.Length && s[currentChoice] == (char)rule)
                            {
                                currentChoices[i]++;
                            }
                            else
                            {
                                currentChoices.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {
                        for (int i = currentChoices.Count - 1; i >= 0; i--)
                        {
                            List<int> newChoicesFromRule = FitsRule((int)rule, currentChoices[i], s);
                            currentChoices.RemoveAt(i);
                            if (newChoicesFromRule.Count > 0)
                            {
                                currentChoices.InsertRange(i, newChoicesFromRule);
                            }
                        }
                    }
                }
                newChoices.AddRange(currentChoices);
            }
            return newChoices;
        }
    }
}
