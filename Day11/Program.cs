using System;
using System.IO;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            char[,] grid = new char[input[0].Length, input.Length];

            InitializeGrid(input, grid);
            int occupiedSeats = Simulate(grid, true);
            Console.WriteLine($"Teil 1: {occupiedSeats}");

            InitializeGrid(input, grid);
            occupiedSeats = Simulate(grid, false);
            Console.WriteLine($"Teil 2: {occupiedSeats}");
        }

        static void InitializeGrid(string[] input, char[,] grid)
        {
            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    grid[i, j] = input[j][i];
                }
            }
        }

        static int Simulate(char[,] grid, bool countDirectNeighbours)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);
            int minNeighbours = (countDirectNeighbours) ? 3 : 4;

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

                        int neighbours = CountNeighbours(gridCopy, i, j, countDirectNeighbours);

                        if (grid[i, j] == 'L' && neighbours == 0)
                        {
                            grid[i, j] = '#';
                            seatHasChanged[i, j] = true;
                        }
                        else if (grid[i, j] == '#' && neighbours > minNeighbours)
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

            return CountOccupiedSeats(grid);
        }

        static int CountNeighbours(char[,] grid, int x, int y, bool countDirectNeighbours)
        {
            int count = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i == x && j == y) continue;
                    if (i < 0 || i == grid.GetLength(0)) continue;
                    if (j < 0 || j == grid.GetLength(1)) continue;

                    char neighbour = (countDirectNeighbours) 
                        ? grid[i, j]
                        : GetNextVisibleNeighbour(grid, x, y, (i-x), (j-y));

                    if (neighbour == '#') count++;
                }
            }
            return count;
        }

        static char GetNextVisibleNeighbour(char[,] grid, int x, int y, int dx, int dy)
        {   
            while(grid[x+dx, y+dy] == '.')
            {
                dx += 1 * Math.Sign(dx);
                dy += 1 * Math.Sign(dy);

                if (x+dx < 0 || x+dx >= grid.GetLength(0)) return '.';
                if (y+dy < 0 || y+dy >= grid.GetLength(1)) return '.';
            }
            return grid[x + dx, y + dy];
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
