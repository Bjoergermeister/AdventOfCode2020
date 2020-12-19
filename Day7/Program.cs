using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static Dictionary<string, List<string>> bags = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            foreach (string bag in input)
            {
                string[] parts = bag.Split(" bags contain");

                string containingBagColor = parts[0];
                if (bags.ContainsKey(containingBagColor) == false)
                {
                    bags.Add(containingBagColor, new List<string>());
                }

                foreach (string color in parts[1].Split(","))
                {
                    if (color == " no other bags.") continue;
                    int length = (color.EndsWith(".")) ? 8 : 7;
                    string containedBagColor = color.Substring(3, color.Length - length).Trim();

                    if (bags.ContainsKey(containedBagColor) == false)
                    {
                        bags.Add(containedBagColor, new List<string>());
                    }
                    bags[containedBagColor].Add(containingBagColor);
                }
            }  

            HashSet<string> visitedBags = new HashSet<string>();
            int foundBags = CountContainingBags("shiny gold", visitedBags);
            Console.WriteLine(foundBags);
        }

        static int CountContainingBags(string color, HashSet<string> visitedBags)
        {
            int foundBags = 0;

            var containingBags = bags[color];
            foreach(string containingBag in containingBags)
            {
                if (visitedBags.Contains(containingBag) == false)
                {
                    foundBags++;
                    visitedBags.Add(containingBag);
                }
                foundBags += CountContainingBags(containingBag, visitedBags);
            }

            return foundBags;
        }
    }
}
