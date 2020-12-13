using System;
using System.Collections.Generic;
using System.IO;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            string passportString = String.Empty;
            List<Passport> passports = new List<Passport>();
            for(int i = 0; i <= input.Length; i++)
            {
                string line = (i < input.Length) ? input[i] : "";
                if (line.Length == 0 || i == input.Length)
                {
                    Passport passport = new Passport(passportString);
                    passports.Add(passport);
                    passportString = String.Empty;
                    continue;
                }
                else
                {
                    passportString += (passportString.Length == 0) ? line : " " + line;
                }
            }

            int validPassports = 0;
            foreach (Passport passport in passports)
            {
                if (passport.IsValid(true)) validPassports++;
            }

            Console.WriteLine(validPassports);
        }
    }
}
