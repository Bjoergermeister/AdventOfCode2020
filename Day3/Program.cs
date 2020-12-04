using System;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            int answer = Puzzle1(input);
            Console.WriteLine(answer);
        }

        static int Puzzle1(string[] input)
        {
            int trees = 0;
            Map map = new Map(input);

            int x = -3;
            int y = -1;

            while (y < map.Height - 1)
            {
                x += 3;
                y += 1;

                if (map.IsTree(x, y)) trees++;
            }

            return trees;
        }
    }
}
