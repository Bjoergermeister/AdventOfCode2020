using System;
using System.Drawing;
using System.IO;

namespace Day12
{
    class Program
    {
        static Point ship = new Point(0, 0);
        static Point waypoint = new Point(10, 1);

        static bool part1 = true;

        static char[] directions = new char[] { 'N', 'E', 'S', 'W' };
        static int direction = 1;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            ExecuteInstructions(input);
            Console.WriteLine($"Teil 1: {Math.Abs(ship.X) + Math.Abs(ship.Y)}");

            ship = new Point(0, 0);
            part1 = false;
            ExecuteInstructions(input);
            Console.WriteLine($"Teil 2: {Math.Abs(ship.X) + Math.Abs(ship.Y)}");
        }

        static void ExecuteInstructions(string[] instructions)
        {
            foreach (string line in instructions)
            {
                char instruction = line[0];
                int.TryParse(line.Substring(1, line.Length - 1), out int distance);

                if (instruction == 'F') MoveShip(directions[direction], distance);
                else if (instruction == 'L' || instruction == 'R')
                {
                    int directionChange = (instruction == 'L') ? (4 - distance / 90) : distance / 90;
                    
                    if (part1) direction = (direction + directionChange) % 4;
                    else Rotate(directionChange);
                }
                else {
                    if (part1) MoveShip(instruction, distance);
                    else MoveWaypoint(instruction, distance);
                }
            }
        }

        static void MoveShip(char direction, int distance)
        {
            int dx = (part1) ? distance : waypoint.X * distance;
            int dy = (part1) ? distance : waypoint.Y * distance;

            if (part1)
            {
                if (direction == 'N') ship.Y += distance;
                else if (direction == 'S') ship.Y -= distance;
                else if (direction == 'E') ship.X += distance;
                else ship.X -= distance;
            }
            else
            {
                ship.X += dx;
                ship.Y += dy;
            }
        }

        static void MoveWaypoint(char direction, int distance)
        {
            if (direction == 'N') waypoint.Y += distance;
            else if (direction == 'S') waypoint.Y -= distance;
            else if (direction == 'E') waypoint.X += distance;
            else waypoint.X -= distance;
        }

        static void Rotate(int angle)
        {
            int x = waypoint.X;
            int y = waypoint.Y;
            switch(angle)
            {
                case 1: 
                    waypoint.X = y;
                    waypoint.Y = -x;
                    break;
                case 2: 
                    waypoint.X = -x;
                    waypoint.Y = -y;
                    break;
                case 3: 
                    waypoint.X = -y;
                    waypoint.Y = x;
                    break;
                default: 
                    break;
            }
        }
    }
}
