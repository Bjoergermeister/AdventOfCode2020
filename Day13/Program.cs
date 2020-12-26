using System;
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

            var busLines = input[1].Split(",").Where(line => int.TryParse(line, out int busLine)).Select(line => Convert.ToInt32(line));

            int smallestDistance = -1;
            int bestBusLine = 0;
            foreach (int line in busLines)
            {
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

            Console.WriteLine($"Teil 1: {smallestDistance * bestBusLine}");
        }
    }
}
