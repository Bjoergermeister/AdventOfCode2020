using System;
using System.Collections.Generic;

namespace Day4
{
    class Passport
    {
        private string[] fields = null;

        internal Passport(string passport)
        {
            fields = passport.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        internal bool IsValid()
        {
            HashSet<string> requiredFields = new HashSet<string> {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid"
            };

            foreach (string field in this.fields)
            {
                if (requiredFields.Contains(field.Substring(0, 3)))
                {
                    requiredFields.Remove(field.Substring(0, 3));
                }
            }

            return requiredFields.Count == 0;
        }
    }
}