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

            //Input verarbeiten
            HashSet<int> ids = new HashSet<int>();
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

                int id = row * 8 + column;
                ids.Add(id);
            }   

            //Höchste ID finden
            int number = 0, heighestID = 0;
            do 
            {
                if (ids.Contains(number))
                {
                    heighestID = number;
                }
                number++;
            }while(number < 1000);

            //Sitz-ID finden
            int seatID = 0;
            while(!(ids.Contains(seatID - 1) && ids.Contains(seatID + 1) && !ids.Contains(seatID)))
            {
                seatID++;
            }

            Console.WriteLine($"Höchste Sitz-ID: {heighestID}");
            Console.WriteLine($"Sitz: {seatID}");
        }
    }
}
