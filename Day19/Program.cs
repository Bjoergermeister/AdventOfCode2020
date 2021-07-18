using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19
{
    class Program
    {
        static string[] input;
        static int index = 0;
        static Dictionary<int, List<List<object>>> rules = new Dictionary<int, List<List<object>>>();
        static void Main(string[] args)
        {
            input = File.ReadAllLines("input.txt");

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

            int result = Puzzle1();
            Console.WriteLine($"Puzzle 1: {result}");
        }

        static int Puzzle1()
        {
            int validMessages = 0;
            for (int i = index + 1; i < input.Length; i++)
            {
                if (FitsRule(0, input[i], out int usedCharacters) && usedCharacters == input[i].Length)
                {
                    validMessages++;
                }
            }
            return validMessages;
        }

        static bool FitsRule(int ruleIndex, string s, out int usedCharacters)
        {
            usedCharacters = 0;
            foreach (List<object> subrule in rules[ruleIndex])
            {
                int usedCharactersToFitRule = 0;
                bool fitsAllSubrules = true;
                foreach (object rule in subrule)
                {
                    if (rule is Char)
                    {
                        if (s[0] == (char)rule)
                        {
                            usedCharacters = 1;
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        string substring = s.Substring(usedCharactersToFitRule, s.Length - usedCharactersToFitRule);
                        if (FitsRule((int)rule, substring, out usedCharacters))
                        {
                            //Console.WriteLine(usedCharacters);
                            usedCharactersToFitRule += usedCharacters;
                        }
                        else
                        {
                            fitsAllSubrules = false;
                            break;
                        }
                    }
                }
                if (fitsAllSubrules)
                {
                    usedCharacters = usedCharactersToFitRule;
                    return true;
                }
            }
            return false;
        }
    }
}
