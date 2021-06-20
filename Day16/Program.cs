using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    class Program
    {
        static Dictionary<string, List<Tuple<int, int>>> rules = new Dictionary<string, List<Tuple<int, int>>>();
        static Dictionary<string, HashSet<int>> possiblePositions = new Dictionary<string, HashSet<int>>();
        static Dictionary<string, int> positions = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            int index = 0;

            string[] lines = File.ReadAllLines("input.txt");

            //Get Ranges        
            do
            {
                string ruleName = lines[index].Split(':').FirstOrDefault();
                MatchCollection matches = Regex.Matches(lines[index], @"\d+");
                int[] numbers = new int[matches.Count];

                for (int i = 0; i < matches.Count; i += 2)
                {
                    int lowerEnd = Convert.ToInt32(matches[i].Value);
                    int upperEnd = Convert.ToInt32(matches[i + 1].Value);
                    AddRule(ruleName, Tuple.Create(lowerEnd, upperEnd));
                }
                index++;
            } while (lines[index].Length > 0);

            Console.WriteLine($"Teil 1: {Puzzle1(lines, index)}");
            Console.WriteLine($"Teil 2: {Puzzle2(lines, index)}");
        }

        private static int Puzzle1(string[] lines, int index)
        {
            int result = 0;
            index += 6;

            for (int i = index; i < lines.Length; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], @"\d+");
                for (int j = 0; j < matches.Count; j++)
                {
                    int value = Convert.ToInt32(matches[j].Value);
                    if (IsValueValid(value) == false) result += value;
                }
            }
            return result;
        }

        private static long Puzzle2(string[] lines, int index)
        {
            index += 5;
            for (int i = index; i < lines.Length; i++)
            {
                if (IsTicketValid(lines[i]) == false) continue;

                MatchCollection matches = Regex.Matches(lines[i], @"\d+");
                for (int j = 0; j < matches.Count; j++)
                {
                    int value = Convert.ToInt32(matches[j].Value);

                    foreach (string ruleName in rules.Keys)
                    {
                        if (rules[ruleName].Any(range => value >= range.Item1 && value <= range.Item2) == false)
                        {
                            possiblePositions[ruleName].Remove(j + 1);
                        }
                    }
                }
            }

            while (possiblePositions.Count > 0)
            {
                foreach (KeyValuePair<string, HashSet<int>> possiblePosition in possiblePositions)
                {
                    if (possiblePosition.Value.Count > 1) continue;

                    int position = possiblePosition.Value.ToList()[0];
                    positions.Add(possiblePosition.Key, position);

                    foreach (string name in possiblePositions.Keys)
                    {
                        if (name == possiblePosition.Key) continue;
                        possiblePositions[name].Remove(position);
                    }

                    possiblePositions.Remove(possiblePosition.Key);
                }
            }

            long answer = 1;
            MatchCollection myTicketValues = Regex.Matches(lines[22], @"\d+");
            foreach (KeyValuePair<string, int> position in positions)
            {
                if (position.Key.StartsWith("departure") == false) continue;
                answer *= Convert.ToInt32(myTicketValues[position.Value - 1].Value);
            }

            return answer;
        }

        private static void PrintPossiblePositions()
        {
            foreach (KeyValuePair<string, HashSet<int>> possiblePosition in possiblePositions)
            {
                Console.WriteLine($"{possiblePosition.Key}: {String.Join(',', possiblePosition.Value)}");
            }
        }

        private static void AddRule(string name, Tuple<int, int> range)
        {
            if (rules.ContainsKey(name))
            {
                rules[name].Add(range);
                return;
            }

            rules.Add(name, new List<Tuple<int, int>> { range });
            possiblePositions.Add(name, new HashSet<int>(Enumerable.Range(1, 20)));
        }

        private static bool IsTicketValid(string line)
        {
            bool isValid = true;
            MatchCollection matches = Regex.Matches(line, @"\d+");
            for (int i = 0; i < matches.Count; i++)
            {
                int value = Convert.ToInt32(matches[i].Value);
                if (IsValueValid(value) == false) isValid = false;
            }

            return isValid;
        }

        private static bool IsValueValid(int value)
        {
            bool inRange = false;
            foreach (List<Tuple<int, int>> ranges in rules.Values)
            {
                if (ranges.Any(range => value >= range.Item1 && value <= range.Item2))
                {
                    inRange = true;
                }
            }

            return inRange;
        }
    }
}
