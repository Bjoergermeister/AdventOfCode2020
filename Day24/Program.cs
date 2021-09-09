using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Day24
{
    class Program
    {
        static Dictionary<Vector3, bool> tiles = new Dictionary<Vector3, bool>();
        static Dictionary<string, Vector3> directionVectors = new Dictionary<string, Vector3>();
        static void Main(string[] args)
        {
            //Initialize Vectors
            directionVectors.Add("ne", new Vector3(1.0f, 0.0f, -1.0f));
            directionVectors.Add("e", new Vector3(1.0f, -1.0f, 0.0f));
            directionVectors.Add("se", new Vector3(0.0f, -1.0f, 1.0f));
            directionVectors.Add("sw", new Vector3(-1.0f, 0.0f, 1.0f));
            directionVectors.Add("w", new Vector3(-1.0f, 1.0f, 0.0f));
            directionVectors.Add("nw", new Vector3(0.0f, 1.0f, -1.0f));

            string[] input = File.ReadAllLines("input.txt");

            Console.WriteLine("Puzzle 1: " + Puzzle1(input));
        }

        static int Puzzle1(string[] input)
        {
            int count = 0;
            foreach (string line in input)
            {
                Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

                MatchCollection matches = Regex.Matches(line, "(ne|nw|se|sw|e|w)");
                foreach (Match match in matches)
                {
                    position += directionVectors[match.Value];
                }

                if (tiles.ContainsKey(position))
                {
                    tiles[position] = !tiles[position];
                    count += (tiles[position]) ? 1 : -1;
                }
                else
                {
                    tiles.Add(position, true);
                    count++;
                }
            }

            return count;
        }
    }
}
