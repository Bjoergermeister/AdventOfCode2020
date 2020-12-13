using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day4
{
    class Passport
    {
        private HashSet<string> requiredFields = new HashSet<string> {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid"
            };

        private string[] fields = null;

        internal Passport(string passport)
        {
            fields = passport.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        internal bool IsValid(bool checkConditions = false)
        {
            foreach (string field in this.fields)
            {
                //Remove field from requiredFields list
                if (requiredFields.Contains(field.Substring(0, 3)))
                {
                    requiredFields.Remove(field.Substring(0, 3));
                }

                if (checkConditions == false) continue;

                string fieldName = field.Substring(0, 3);
                string fieldValue = field.Substring(4, field.Length - 4);

                Regex hcl = new Regex(@"^#([0-9|a-f]{6})$");
                Regex ecl = new Regex(@"^amb|blu|brn|gry|grn|hzl|oth$");
                Regex pid = new Regex(@"^\d{9}$");
                
                switch(fieldName)
                {
                    case "byr": 
                        if (int.TryParse(fieldValue, out int byr) == false) return false;
                        if (byr < 1920 || byr > 2002) return false;
                        break;
                    case "iyr": 
                        if (int.TryParse(fieldValue, out int iyr) == false) return false;
                        if (iyr < 2010 || iyr > 2020) return false;
                        break;
                    case "eyr": 
                        if (int.TryParse(fieldValue, out int eyr) == false) return false;
                        if (eyr < 2020 || eyr > 2030) return false;
                        break;
                    case "hgt": 
                        if (int.TryParse(fieldValue.Substring(0, fieldValue.Length - 2), out int height) == false) return false;
                        string unit = fieldValue.Substring(fieldValue.Length - 2, 2);
                        if (unit == "cm" && (height < 150 || height > 193)) return false;
                        if (unit == "in" && (height < 59 || height > 76)) return false;
                        break;
                    case "hcl": 
                        if (regex.IsMatch(fieldValue) == false) return false;
                        break;
                    case "ecl": 
                        if (regex.IsMatch(fieldValue) == false) return false;
                        break;
                    case "pid": 
                        if (regex.IsMatch(fieldValue) == false) return false;
                        break;
                    case "cid": break;
                    default: return false;

                }
            }

            return requiredFields.Count == 0;
        }
    }
}