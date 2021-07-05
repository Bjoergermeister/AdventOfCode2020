using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day18
{
    class Program
    {
        static string currentLine = String.Empty;
        static string[] input;
        static void Main(string[] args)
        {
            input = File.ReadAllLines("input.txt");
            long result = Puzzle1();
            Console.WriteLine($"Puzzle 1: {result}");
        }

        static long Puzzle1()
        {
            long sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                Stack openingBrackets = new Stack();
                currentLine = input[i];

                for (int j = 0; j < currentLine.Length; j++)
                {
                    if (currentLine[j] == '(')
                    {
                        openingBrackets.Push(j);
                    }
                    else if (currentLine[j] == ')')
                    {
                        int index = (int)openingBrackets.Pop();
                        string result = ProcessSubstring(index, j).ToString();
                        currentLine = currentLine.Replace(currentLine.Substring(index, j - index + 1), result.ToString());
                        j = index;
                    }
                }
                sum += ProcessSubstring(0, currentLine.Length);
            }
            return sum;
        }

        static long ProcessSubstring(int start, int end)
        {
            string substring = currentLine.Substring(start, end - start);
            MatchCollection numbers = Regex.Matches(substring, @"\d+");
            MatchCollection operators = Regex.Matches(substring, @"[+|*]");

            long accumulator = Convert.ToInt64(numbers[0].Value);

            for (int i = 0; i < operators.Count; i++)
            {
                char operand = operators[i].Value[0];
                int value = Convert.ToInt32(numbers[i + 1].Value);
                accumulator = (operand == '+') ? accumulator + value : accumulator * value;
            }

            return accumulator;
        }
    }
}
