using System;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {

        static Map map = null;
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            int answer = Puzzle2(input);
            Console.WriteLine(answer);
        }

        static int Puzzle1(string[] input)
        {
            map = new Map(input);

            return GetNumberOfTreesForSlope(3, 1);
        }

        static int Puzzle2(string[] input)
        {
            map = new Map(input);

            int trees = 0;
            trees = GetNumberOfTreesForSlope(1, 1);
            trees *= GetNumberOfTreesForSlope(3, 1);
            trees *= GetNumberOfTreesForSlope(5, 1);
            trees *= GetNumberOfTreesForSlope(7, 1);
            trees *= GetNumberOfTreesForSlope(1, 2);

            return trees;
        }

        static int GetNumberOfTreesForSlope(int xStep, int yStep)
        {
            int trees = 0;

            int x = 0;
            int y = 0;

            do
            {
                if (map.IsTree(x, y)) trees++;

                x += xStep;
                y += yStep;
            } while (y < map.Height);

            return trees;
        }
    }
}
