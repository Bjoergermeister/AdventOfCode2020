using System;
using System.IO;

namespace Day12
{
    class Program
    {
        static int east = 0;
        static int north = 0;

        static char[] directions = new char[] { 'N', 'E', 'S', 'W' };
        static int direction = 1;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            foreach (string line in input)
            {
                char instruction = line[0];
                int.TryParse(line.Substring(1, line.Length - 1), out int distance);

                if (instruction == 'F') Move(directions[direction], distance);
                else if (instruction == 'L')
                {
                    direction -= (distance / 90);
                    if (direction < 0) direction += 4;
                }
                else if (instruction == 'R')
                {
                    direction += (distance / 90);
                    if (direction > 3) direction -= 4;
                }
                else Move(instruction, distance);
            }
            Console.WriteLine(Math.Abs(east) + Math.Abs(north));
        }

        static void Move(char direction, int distance)
        {
            if (direction == 'N') north += distance;
            else if (direction == 'S') north -= distance;
            else if (direction == 'E') east += distance;
            else east -= distance;
        }
    }
}
