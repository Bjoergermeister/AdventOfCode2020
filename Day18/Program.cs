using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            long result = ProcessLine(false);
            Console.WriteLine($"Puzzle 1: {result}");

            input = File.ReadAllLines("input.txt");
            result = ProcessLine(true);
            Console.WriteLine($"Puzzle 2: {result}");
        }

        static long ProcessLine(bool prioritize)
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
                        string result = ProcessSubstring(index, j, prioritize).ToString();
                        currentLine = currentLine.Replace(currentLine.Substring(index, j - index + 1), result.ToString());
                        j = index;
                    }
                }
                sum += ProcessSubstring(0, currentLine.Length, prioritize);
            }
            return sum;
        }

        static long ProcessSubstring(int start, int end, bool prioritize)
        {
            string substring = currentLine.Substring(start, end - start);
            List<long> numbers = Regex.Matches(substring, @"\d+").Select(m => Convert.ToInt64(m.Value)).ToList();
            List<char> operators = Regex.Matches(substring, @"[+|*]").Select(m => m.Value[0]).ToList();

            if (prioritize)
            {
                for (int i = operators.Count - 1; i >= 0; i--)
                {
                    if (operators[i] == '*') continue;

                    numbers[i] = numbers[i] + numbers[i + 1];
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                }
            }

            long accumulator = numbers[0];

            for (int i = 0; i < operators.Count; i++)
            {
                char operand = operators[i];
                long value = numbers[i + 1];
                accumulator = (operand == '+') ? accumulator + value : accumulator * value;
            }

            return accumulator;
        }
    }
}
