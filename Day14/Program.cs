using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14
{
    class Program
    {
        static Dictionary<int, long> memory = new Dictionary<int, long>();
        static string mask = "";

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            foreach(string s in input)
            {
                if (s.StartsWith("mask")) mask = s.Split(" = ")[1];
                else
                {
                    string[] parts = s.Split(" = ");
                    int memoryAddress = Convert.ToInt32(Regex.Match(parts[0], @"\d+").Value);

                    long number = BStringToInt(ApplyMask(IntToBString(parts[1]).ToCharArray()));

                    if (memory.ContainsKey(memoryAddress))
                    {
                        memory[memoryAddress] = number;
                    }
                    else
                    {
                        memory.Add(memoryAddress, number);
                    }
                }
            }  

            long value = 0;
            foreach(var pair in memory) value += pair.Value;  

            Console.WriteLine($"Teil 1: {value}");
        }

        static string IntToBString(string s) 
        {
            int number = Convert.ToInt32(s);
            return Convert.ToString(number, 2);
        }

        static long BStringToInt(string s) => Convert.ToInt64(s, 2);

        static string ApplyMask(char[] binaryInteger)
        {
            binaryInteger = PrepentBits(binaryInteger, mask.Length);

            for(int i = binaryInteger.Length - 1; i >= 0; i--)
            {
                if (mask[mask.Length - (binaryInteger.Length - i)] != 'X')
                {
                    binaryInteger[i] = mask[mask.Length - (binaryInteger.Length - i)];
                } 
            }

            return new String(binaryInteger);
        }

        static char[] PrepentBits(char[] bits, int requiredLength)
        {
            char[] newBits = Enumerable.Repeat('0', requiredLength - bits.Length).ToArray(); 
            return newBits.Concat(bits).ToArray();
        }
    }
}
