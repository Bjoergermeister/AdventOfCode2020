using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            int answer = Puzzle2(lines);
            Console.WriteLine(answer);
        }

        static int Puzzle1(string[] input)
        {
            var numbers = from line in input select Convert.ToInt32(line);
            var addingNumbers =  FindAddingNumbers(numbers, 2020);
            return addingNumbers.Item1 * addingNumbers.Item2;
        }

        static int Puzzle2(string[] input)
        {
            var numbers = from line in input select Convert.ToInt32(line);
            
            foreach(var current in numbers)
            {
                var addingNumbers = FindAddingNumbers(numbers, 2020 - current, current);
                if (addingNumbers.Item1 == 0 || addingNumbers.Item2 == 0) continue;

                return current * addingNumbers.Item1 * addingNumbers.Item2;
            }

            return 0;
        }

        static (int, int) FindAddingNumbers(IEnumerable<int> input, int goal, int excluded = -1)
        {
            HashSet<int> numbers = new HashSet<int>();

            foreach(var number in input)
            {    
                if (number == excluded) continue;            
                if (numbers.Contains(number)) return (number, goal - number);

                numbers.Add(goal - number);
            }

            return (0, 0);
        }
    }
}
