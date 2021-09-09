using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Console.WriteLine("Puzzle 2: " + Puzzle2());
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

        static int Puzzle2()
        {
            Dictionary<Vector3, bool> gridCopy = new Dictionary<Vector3, bool>();
            for (int i = 0; i < 100; i++)
            {
                AddMissingSurroundingTiles();
                CloneDictionary(tiles, gridCopy);
                foreach (Vector3 position in tiles.Keys.ToList())
                {
                    int neighbours = CountNeighbours(gridCopy, position);
                    if (tiles[position] && (neighbours == 0 || neighbours > 2)) tiles[position] = false;
                    if (!tiles[position] && neighbours == 2) tiles[position] = true;
                }
            }

            int count = 0;
            foreach (bool isBlack in tiles.Values)
            {
                if (isBlack) count++;
            }

            return count;
        }

        static void AddMissingSurroundingTiles()
        {
            foreach (Vector3 position in tiles.Keys.ToList())
            {
                foreach (Vector3 direction in directionVectors.Values)
                {
                    if (tiles.ContainsKey(position + direction) == false)
                    {
                        tiles.Add(position + direction, false);
                    }
                }
            }
        }

        static int CountNeighbours(Dictionary<Vector3, bool> grid, Vector3 position)
        {
            int count = 0;
            foreach (Vector3 direction in directionVectors.Values)
            {
                if (grid.ContainsKey(position + direction) == false) continue;
                if (grid[position + direction]) count++;
            }
            return count;
        }

        static void CloneDictionary(Dictionary<Vector3, bool> origin, Dictionary<Vector3, bool> copyTo)
        {
            copyTo.Clear();
            foreach (KeyValuePair<Vector3, bool> entry in origin)
            {
                copyTo.Add(entry.Key, entry.Value);
            }
        }
    }
}
