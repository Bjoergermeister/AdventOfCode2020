using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Day17
{
    class Program
    {
        #region Fields
        static Envelope envelope = null;
        static Dictionary<Tuple<int, int, int>, bool> cubes = new Dictionary<Tuple<int, int, int>, bool>();
        static Dictionary<Tuple<int, int, int>, bool> cubesCopy = new Dictionary<Tuple<int, int, int>, bool>();
        #endregion

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            //Initialize cubes
            for (int y = 0; y < input.Length; y++)
            {
                string currentLine = input[y];
                for (int x = 0; x < currentLine.Length; x++)
                {
                    bool isActive = currentLine[x] == '#';
                    cubes.Add(Tuple.Create(x, y, 0), isActive);
                }
            }

            envelope = new Envelope(input);

            int result = Puzzle1();
            Console.WriteLine($"Puzzle 1: {result}");
        }

        private static int Puzzle1()
        {
            int cycles = 6;

            for (int i = 0; i < cycles; i++)
            {
                CopyDictionary();
                for (int x = envelope.MinX; x <= envelope.MaxX; x++)
                {
                    for (int y = envelope.MinY; y <= envelope.MaxY; y++)
                    {
                        for (int z = envelope.MinZ; z <= envelope.MaxZ; z++)
                        {
                            int neighbours = CountNeighbours(x, y, z);

                            bool isActive = getCubeStatus(x, y, z);
                            if (isActive && (neighbours < 2 || neighbours > 3)) cubes[Tuple.Create(x, y, z)] = false;
                            if (isActive == false && neighbours == 3) cubes[Tuple.Create(x, y, z)] = true;
                        }
                    }
                }
                envelope.Expand();
            }

            return CountActiveCubes();
        }

        private static int CountNeighbours(int x, int y, int z)
        {
            int count = 0;
            for (int dx = -1; dx < 2; dx++)
            {
                for (int dy = -1; dy < 2; dy++)
                {
                    for (int dz = -1; dz < 2; dz++)
                    {
                        if (dx == 0 && dy == 0 && dz == 0) continue;
                        Tuple<int, int, int> position = Tuple.Create(x + dx, y + dy, z + dz);
                        if (cubesCopy.ContainsKey(position) && cubesCopy[position]) count++;
                    }
                }
            }
            return count;
        }

        private static bool getCubeStatus(int x, int y, int z)
        {
            Tuple<int, int, int> position = Tuple.Create(x, y, z);

            if (cubesCopy.ContainsKey(position)) return cubesCopy[position];

            cubesCopy.Add(position, false);
            return false;
        }

        private static void CopyDictionary()
        {
            cubesCopy.Clear();

            foreach (Tuple<int, int, int> key in cubes.Keys)
            {
                cubesCopy.Add(key, cubes[key]);
            }
        }
        private static int CountActiveCubes()
        {
            int count = 0;
            for (int x = envelope.MinX; x <= envelope.MaxX; x++)
            {
                for (int y = envelope.MinY; y <= envelope.MaxY; y++)
                {
                    for (int z = envelope.MinZ; z <= envelope.MaxZ; z++)
                    {
                        Tuple<int, int, int> position = Tuple.Create(x, y, z);
                        if (cubes.ContainsKey(position) && cubes[position]) count++;
                    }
                }
            }
            return count;
        }
    }
}