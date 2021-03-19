using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {            
            List<Tuple<int, int>> ranges = new List<Tuple<int, int>>();
            int result = 0;
            int index = 0;

            string[] lines = File.ReadAllLines("input.txt");

            //Get Ranges        
            do
            {                
                MatchCollection matches = Regex.Matches(lines[index], @"\d+");
                int[] numbers = new int[matches.Count];

                for(int i = 0; i < matches.Count; i++)
                {
                    numbers[i] = Convert.ToInt32(matches[i].Value);
                }

                ranges.Add(Tuple.Create(numbers[0], numbers[1]));
                ranges.Add(Tuple.Create(numbers[2], numbers[3]));
                index++;
            } while (lines[index].Length > 0);

            index += 6;

            do
            {
                MatchCollection matches = Regex.Matches(lines[index], @"\d+");
                for(int i = 0; i < matches.Count; i++)
                {
                    int value = Convert.ToInt32(matches[i].Value);

                    if(ranges.Any(range => value >= range.Item1 && value <= range.Item2) == false)
                    {
                        result += value;
                    }
                }

                index++;
            } while (index < lines.Length);

            Console.WriteLine($"Teil 1: {result}");
        }
    }
}
