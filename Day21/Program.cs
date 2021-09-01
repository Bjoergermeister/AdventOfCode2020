using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day21
{
    class Program
    {
        static string[] input = null;
        static Dictionary<string, string> pairs = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            input = File.ReadAllLines("input.txt");

            Console.WriteLine("Puzzle 1: " + Puzzle1());
            Console.WriteLine("Puzzle 2: " + Puzzle2());
        }

        static int Puzzle1()
        {
            Dictionary<string, List<string>> possiblePairs = new Dictionary<string, List<string>>();
            List<List<string>> allIngredients = new List<List<string>>();

            foreach (string line in input)
            {
                (List<string> ingredients, List<string> allergenes) = GetIngredientsAndAllergenesFromLine(line);

                allIngredients.Add(ingredients);

                //Exluce allergenes from ingredients
                foreach (string allergene in allergenes)
                {
                    if (possiblePairs.ContainsKey(allergene) == false)
                    {
                        possiblePairs.Add(allergene, ingredients);
                    }
                    else
                    {
                        possiblePairs[allergene] = possiblePairs[allergene].Intersect(ingredients).ToList();
                    }
                }
            }

            while (possiblePairs.Count > 0)
            {
                foreach (KeyValuePair<string, List<string>> pair in possiblePairs)
                {
                    if (pair.Value.Count == 1)
                    {
                        string ingredientToRemove = pair.Value[0];
                        pairs.Add(pair.Key, ingredientToRemove);
                        possiblePairs.Remove(pair.Key);
                        RemoveIngredient(possiblePairs, ingredientToRemove);
                    }
                }
            }

            int result = 0;
            foreach (List<string> ingredients in allIngredients)
            {
                foreach (string ingredient in ingredients)
                {
                    if (pairs.Values.Contains(ingredient) == false) result++;
                }
            }
            return result;
        }

        static (List<string>, List<string>) GetIngredientsAndAllergenesFromLine(string line)
        {
            string[] parts = line.Split(" (");
            string allergenes = parts[1].Substring(9, parts[1].Length - 10);
            return (parts[0].Split(" ").ToList(), allergenes.Split(", ").ToList());
        }

        static void RemoveIngredient(Dictionary<string, List<string>> pairs, string ingredient)
        {
            foreach (KeyValuePair<string, List<string>> pair in pairs)
            {
                if (pair.Value.Contains(ingredient)) pair.Value.Remove(ingredient);
            }
        }

        static string Puzzle2()
        {
            List<string> sortedAllergenes = pairs.Keys.ToList();
            sortedAllergenes.Sort();
            return String.Join(",", sortedAllergenes.Select(allergene => pairs[allergene]));
        }

        static Dictionary<string, string> ReverseDictionary(Dictionary<string, string> dict)
        {
            Dictionary<string, string> reversed = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in dict)
            {
                reversed.Add(pair.Value, pair.Key);
            }

            return reversed;
        }
    }
}
