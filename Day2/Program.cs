using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {

        public static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            int answer = Puzzle2(input);
            Console.WriteLine(answer);
        }

        static int Puzzle1(string[] input)
        {
            int validPasswords = 0;
            foreach (var line in input)
            {
                var parts = line.Split(':');

                PasswordPolicy policy = new PasswordPolicy(parts[0]);
                if (policy.ValidatePasswordCount(parts[1].Trim())) validPasswords++;
            }

            return validPasswords;
        }

        static int Puzzle2(string[] input)
        {
            int validPasswords = 0;
            foreach (var line in input)
            {
                var parts = line.Split(':');

                PasswordPolicy policy = new PasswordPolicy(parts[0]);
                if (policy.ValidatePasswordPositions(parts[1].Trim())) validPasswords++;
            }

            return validPasswords;
        }
    }
}
