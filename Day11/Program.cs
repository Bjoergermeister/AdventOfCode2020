using System;
using System.IO;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int width = input[0].Length;
            int height = input.Length;

            char[,] grid = new char[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = input[j][i];
                }
            }

            bool[,] seatHasChanged = new bool[width, height];

            do
            {
                //Copy input array
                char[,] gridCopy = new char[width, height];
                Array.Copy(grid, gridCopy, grid.Length);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (grid[i, j] == '.') continue;

                        int neighbours = CountNeighbours(gridCopy, i, j);
                        if (grid[i, j] == 'L' && neighbours == 0)
                        {
                            grid[i, j] = '#';
                            seatHasChanged[i, j] = true;
                        }
                        else if (grid[i, j] == '#' && neighbours > 3)
                        {
                            grid[i, j] = 'L';
                            seatHasChanged[i, j] = true;
                        }
                        else
                        {
                            seatHasChanged[i, j] = false;
                        }
                    }
                }
            } while (AtLeastOneSeatChanged(seatHasChanged));

            int occupiedSeats = CountOccupiedSeats(grid);
            Console.WriteLine($"Teil 1: {occupiedSeats}");
        }

        static int CountNeighbours(char[,] grid, int x, int y)
        {
            int count = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i == x && j == y) continue;
                    if (i < 0 || i == grid.GetLength(0)) continue;
                    if (j < 0 || j == grid.GetLength(1)) continue;

                    if (grid[i, j] == '#') count++;
                }
            }
            return count;
        }

        static bool AtLeastOneSeatChanged(bool[,] seatHasChanged)
        {
            for (int i = 0; i < seatHasChanged.GetLength(0); i++)
            {
                for (int j = 0; j < seatHasChanged.GetLength(1); j++)
                {
                    if (seatHasChanged[i, j]) return true;
                }
            }
            return false;
        }

        static int CountOccupiedSeats(char[,] grid)
        {
            int count = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '#') count++;
                }
            }

            return count;
        }
    }
}
