using System;
using System.Collections.Generic;
using System.IO;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            
            int heighestID = 0;

            foreach(string seat in input)
            {
                int row = 127;
                for(int i = 6; i >= 0; i--)
                {
                    if (seat[i] == 'F') row -= (int)Math.Pow(2, 6 - i);
                }

                int column = 7;
                for(int i = 9; i >= 6; i--)
                {
                    if (seat[i] == 'L') column -= (int)Math.Pow(2, 9 - i);
                }

                //Calculate and compare id
                int id = row * 8 + column;
                heighestID = Math.Max(id, heighestID);
            }

            Console.WriteLine(heighestID);
        }
    }
}
