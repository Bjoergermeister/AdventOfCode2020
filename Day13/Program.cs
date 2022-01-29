using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int earliestDepartureTime = Convert.ToInt32(input[0]);
            int validLinesCount = 0;
            List<int> busLines = new List<int>();
            foreach (String busLine in input[1].Split(","))
            {
                if (int.TryParse(busLine, out int busLineInt))
                {
                    busLines.Add(busLineInt);
                    validLinesCount++;
                }
                else
                {
                    busLines.Add(-1);
                }
            }

            Console.WriteLine($"Puzzle 1: {Puzzle1(earliestDepartureTime, busLines)}");
            Console.WriteLine($"Puzzle 2: {Puzzle2(busLines, validLinesCount)}");
        }

        static int Puzzle1(int earliestDepartureTime, List<int> busLines)
        {
            int smallestDistance = -1;
            int bestBusLine = 0;
            foreach (int line in busLines)
            {
                if (line == -1)
                {
                    continue;
                }

                int multiplikator = 1;
                while (line * multiplikator < earliestDepartureTime)
                {
                    multiplikator++;
                }

                if ((line * multiplikator) - earliestDepartureTime < smallestDistance || smallestDistance == -1)
                {
                    smallestDistance = (line * multiplikator) - earliestDepartureTime;
                    bestBusLine = line;
                }
            }

            return smallestDistance * bestBusLine;
        }

        static long Puzzle2(List<int> busLines, int validLineCount)
        {
            //Chinese Remainder theorem
            List<int> numbers = new List<int>();
            List<int> remainders = new List<int>();

            //Compute numbers, remainders and product
            long product = 1;
            for (int i = 0; i < busLines.Count; i++)
            {
                if (busLines[i] == -1) continue;
                product *= busLines[i];
                numbers.Add(busLines[i]);
                remainders.Add((busLines[i] - i) % busLines[i]);
            }

            //Compute pp and multiplicative inverses
            long result = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                long pp = product / numbers[i];
                long inverse = moduloInverse(pp, numbers[i]);
                result += remainders[i] * inverse * pp;
            }

            return result % product;
        }

        static long moduloInverse(long a, long m)
        {
            if (greatestCommonDivider(a, m, out long x, out long y) != 1)
            {
                Console.WriteLine("Inverse doesnt exist");
                return 0L;
            }
            else
            {
                return (long)((x % m + m) % m);
            }
        }

        static long greatestCommonDivider(long a, long b, out long x, out long y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            long gcd = greatestCommonDivider(b % a, a, out long x1, out long y1);
            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }
    }
}
