using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14
{
    class Program
    {
        private enum Mode 
        {
            Normal,
            Fluctuate
        }

        #region Fields
        static Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();
        static string mask = "";
        #endregion

        static void Main(string[] args)
        {
            //Console.WriteLine(Convert.ToInt64("111111111111111111111111111111111111", 2));
            string[] input = File.ReadAllLines("input.txt");
 
            Console.WriteLine($"Teil 1: {Puzzle1(input)}");
            memory.Clear();
            Console.WriteLine($"Teil 2: {Puzzle2(input)}");            
        }

        static ulong Puzzle1(string[] input)
        {
            foreach(string s in input)
            {
                if (s.StartsWith("mask")) mask = s.Split(" = ")[1];
                else
                {
                    string[] parts = s.Split(" = ");
                    uint memoryAddress = Convert.ToUInt32(Regex.Match(parts[0], @"\d+").Value);

                    string[] numbers = ApplyMask(StringToBinaryCharArray(parts[1]), Mode.Normal);
                    ulong number = BinaryStringToLong(numbers[0]);

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

            ulong value = 0;
            foreach(var pair in memory) value += pair.Value;  

            return value;
        }

        static ulong Puzzle2(string[] input)
        {
            foreach(string s in input)
            {
                if (s.StartsWith("mask")) mask = s.Split(" = ")[1];
                else
                {
                    string[] parts = s.Split(" = ");
                    ulong number = Convert.ToUInt64(parts[1]);

                    string[] memoryAddresses = ApplyMask(StringToBinaryCharArray(Regex.Match(parts[0], @"\d+").Value), Mode.Fluctuate);

                    foreach(string memoryAddress in memoryAddresses)
                    {
                        ulong memoryAddressLong = Convert.ToUInt64(memoryAddress, 2);
                        if (memory.ContainsKey(memoryAddressLong))
                        {
                            memory[memoryAddressLong] = number;
                        }
                        else
                        {
                            memory.Add(memoryAddressLong, number);
                        }
                    }
                }
            }

            ulong value = 0;
            foreach(var pair in memory) value += pair.Value;  

            return value;
        }

        static char[] StringToBinaryCharArray(string s) 
        {
            int number = Convert.ToInt32(s);
            return Convert.ToString(number, 2).ToCharArray();
        }

        static ulong BinaryStringToLong(string s) => Convert.ToUInt64(s, 2);

        static string[] ApplyMask(char[] binaryInteger, Mode mode)
        {
            List<string> values = new List<string>();

            binaryInteger = PrepentBits(binaryInteger, mask.Length);

            if (mode == Mode.Normal)
            {
                for(int i = binaryInteger.Length - 1; i >= 0; i--)
                {
                    binaryInteger[i] = (mask[i] != 'X') ? mask[i] : binaryInteger[i];
                }
                values.Add(new String(binaryInteger));
            }
            else
            {
                int xCount = 0;
                for(int i = binaryInteger.Length - 1; i >= 0; i--)
                { 
                    if (mask[i] == '1')
                    {
                        binaryInteger[i] = '1';
                    }

                    if (mask[i] == 'X')
                    {
                        binaryInteger[i] = 'X';
                        xCount++;
                    }
                }
                /*
                Console.WriteLine("------------");
                Console.WriteLine(mask);

                Console.WriteLine("");
                */

                for(int i = 0; i < Math.Pow(2, xCount); i++)
                {
                    char[] binary = Convert.ToString(i, 2).ToCharArray();
                    binary = PrepentBits(binary, xCount);

                    char[] maskCopy = new String(binaryInteger).ToCharArray();
                    int pos = binary.Length - 1;
                    for(int j = mask.Length - 1; j >= 0; j--)
                    {
                        if (maskCopy[j] == 'X')
                        {
                            if (pos >= 0)
                            {
                                maskCopy[j] = (pos >= 0) ? binary[pos] : '0';
                                pos--;
                            }                         
                        }
                    }
                    //Console.WriteLine(new String(maskCopy));
                    values.Add(new String(maskCopy));
                }
            }

            return values.ToArray();

        }

        static char[] PrepentBits(char[] bits, int requiredLength)
        {
            if (bits.Length >= requiredLength) return bits;
            char[] newBits = Enumerable.Repeat('0', requiredLength - bits.Length).ToArray(); 
            return newBits.Concat(bits).ToArray();
        }
    }
}
