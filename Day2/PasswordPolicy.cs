using System;

namespace Day2
{
    class PasswordPolicy
    {
        int lowerBound;
        int upperBound;
        char character;

        internal PasswordPolicy(string s)
        {
            string[] parts = s.Split(' ');
            
            this.character = parts[1][0];
            
            //Extract range
            string range = parts[0];
            string[] bounds = range.Split('-');
            this.lowerBound = Convert.ToInt32(bounds[0]);
            this.upperBound = Convert.ToInt32(bounds[1]);
        }

        internal bool ValidatePassword(string password)
        {
            int charCount = 0;
            foreach(char c in password)
            {
                if (c == this.character) charCount++;
            }

            return charCount >= this.lowerBound && charCount <= this.upperBound;
        }
    }
}