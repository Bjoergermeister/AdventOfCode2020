using System;
using System.IO;

namespace Day2
{
    class Program
    {

        public static void Main(string[] args)
        {
            int answer = Puzzle1();
            Console.WriteLine(answer);
        }

        static int Puzzle1()
        {
            var input = File.ReadAllLines("input.txt");

            int validPasswords = 0;
            foreach (var line in input)
            {
                var parts = line.Split(':');

                PasswordPolicy policy = new PasswordPolicy(parts[0]);
                if (policy.ValidatePassword(parts[1].Trim())) validPasswords++;
            }

            return validPasswords;
        }
    }
}
