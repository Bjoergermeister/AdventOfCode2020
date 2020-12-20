using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static Dictionary<string, List<Tuple<int, string>>> bags = new Dictionary<string, List<Tuple<int, string>>>();
        static HashSet<string> countedBags = new HashSet<string>();
        static Stack<string> bagsToVisit = new Stack<string>();

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            foreach (string bag in input)
            {
                string[] parts = bag.Split(" bags contain");

                string containingBagColor = parts[0];
                if (bags.ContainsKey(containingBagColor) == false)
                {
                    bags.Add(containingBagColor, new List<Tuple<int, string>>());
                }

                foreach (string color in parts[1].Split(","))
                {
                    if (color == " no other bags.") continue;
                    int length = (color.EndsWith(".")) ? 8 : 7;
                    string containedBagColor = color.Substring(3, color.Length - length).Trim();
                    int.TryParse(color.Substring(1, 1), out int amout);

                    if (bags.ContainsKey(containedBagColor) == false)
                    {
                        bags.Add(containedBagColor, new List<Tuple<int, string>>());
                    }
                    bags[containingBagColor].Add(Tuple.Create(amout, containedBagColor));
                }
            }  

            int containingCount = CountContainingBags("shiny gold");
            Console.WriteLine($"Der shiny gold Bag kann von {containingCount} verschiedenen Bags enthalten werden.");

            int containedCount = CountContainedBags("shiny gold");
            Console.WriteLine($"Der shiny gold Bag enthält {containedCount} Bags.");
        }

        static int CountContainingBags(string color)
        {
            int count = 0;

            foreach(var bag in bags)
            {
                string bagColor = bag.Key;

                if (BagCanContain(bagColor, color) == false) continue;

                if (countedBags.Contains(bagColor) == false)
                {
                    countedBags.Add(bagColor);
                    count++;
                }
                bagsToVisit.Push(bagColor);
            }

            while(bagsToVisit.Count > 0)
            {
                string nextBagColor = bagsToVisit.Pop();
                count += CountContainingBags(nextBagColor);
            }

            return count;
        }

        static int CountContainedBags(string color)
        {
            int count = 0;

            var containedBags = bags[color];
            if (containedBags.Count == 0) return count;

            foreach(var bag in containedBags)
            {
                count += CountContainedBags(bag.Item2) * bag.Item1 + bag.Item1;
            }

            return count;
        }

        static bool BagCanContain(string color, string searchedColor)
        {
            var containedColors = bags[color];
            foreach(var containedColor in containedColors)
            {
                if (containedColor.Item2 == searchedColor) return true;
            }

            return false;
        }
    }
}
