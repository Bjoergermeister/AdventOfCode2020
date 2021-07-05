using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Day17
{
    class Program
    {
        #region Fields
        const int CYCLES = 6;
        static bool use4d = false;
        static Envelope envelope = null;
        static HashSet<Tuple<int, int, int, int>> cubes = new HashSet<Tuple<int, int, int, int>>();
        static HashSet<Tuple<int, int, int, int>> cubesCopy = new HashSet<Tuple<int, int, int, int>>();
        #endregion

        static void Main(string[] args)
        {
            PrepareCubeDimension();
            int result = Puzzle1();
            Console.WriteLine($"Puzzle 1: {result}");
            PrepareCubeDimension();
            result = Puzzle2();
            Console.WriteLine($"Puzzle 2: {result}");
        }

        static void PrepareCubeDimension()
        {
            string[] input = File.ReadAllLines("input.txt");

            cubes.Clear();
            cubesCopy.Clear();
            //Initialize cubes
            for (int y = 0; y < input.Length; y++)
            {
                string currentLine = input[y];
                for (int x = 0; x < currentLine.Length; x++)
                {
                    if (currentLine[x] == '#') cubes.Add(Tuple.Create(x, y, 0, 0));
                }
            }

            envelope = new Envelope(input);
        }

        private static int Puzzle1()
        {
            for (int i = 0; i < CYCLES; i++)
            {
                cubesCopy = new HashSet<Tuple<int, int, int, int>>(cubes);
                for (int x = envelope.MinX; x <= envelope.MaxX; x++)
                {
                    for (int y = envelope.MinY; y <= envelope.MaxY; y++)
                    {
                        for (int z = envelope.MinZ; z <= envelope.MaxZ; z++)
                        {
                            int neighbours = CountNeighbours(x, y, z, 0);

                            bool isActive = cubesCopy.Contains(Tuple.Create(x, y, z, 0));
                            if (isActive && (neighbours < 2 || neighbours > 3)) cubes.Remove(Tuple.Create(x, y, z, 0));
                            if (isActive == false && neighbours == 3) cubes.Add(Tuple.Create(x, y, z, 0));
                        }
                    }
                }
                envelope.Expand();
            }

            return cubes.Count;
        }

        private static int Puzzle2()
        {
            use4d = true;
            for (int i = 0; i < CYCLES; i++)
            {
                cubesCopy = new HashSet<Tuple<int, int, int, int>>(cubes);
                for (int x = envelope.MinX; x <= envelope.MaxX; x++)
                {
                    for (int y = envelope.MinY; y <= envelope.MaxY; y++)
                    {
                        for (int z = envelope.MinZ; z <= envelope.MaxZ; z++)
                        {
                            for (int w = envelope.MinW; w <= envelope.MaxW; w++)
                            {
                                int neighbours = CountNeighbours(x, y, z, w);

                                bool isActive = cubesCopy.Contains(Tuple.Create(x, y, z, w));
                                Tuple<int, int, int, int> position = Tuple.Create(x, y, z, w);
                                if (isActive && (neighbours < 2 || neighbours > 3)) cubes.Remove(position);
                                if (isActive == false && neighbours == 3) cubes.Add(position);
                            }
                        }
                    }
                }
                envelope.Expand();
            }

            return cubes.Count;
        }

        private static int CountNeighbours(int x, int y, int z, int w)
        {
            int count = 0;
            for (int dx = -1; dx < 2; dx++)
            {
                for (int dy = -1; dy < 2; dy++)
                {
                    for (int dz = -1; dz < 2; dz++)
                    {
                        if (use4d)
                        {
                            for (int dw = -1; dw < 2; dw++)
                            {
                                if (dx == 0 && dy == 0 && dz == 0 && dw == 0) continue;
                                Tuple<int, int, int, int> position = Tuple.Create(x + dx, y + dy, z + dz, w + dw);
                                if (cubesCopy.Contains(position)) count++;
                            }
                        }
                        else
                        {
                            if (dx == 0 && dy == 0 && dz == 0) continue;
                            Tuple<int, int, int, int> position = Tuple.Create(x + dx, y + dy, z + dz, 0);
                            if (cubesCopy.Contains(position)) count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}