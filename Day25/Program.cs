using System;
using System.IO;

namespace Day25
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            Console.WriteLine($"Puzzle 1: {Puzzle1(Convert.ToInt64(lines[0]), Convert.ToInt64(lines[1]))}");
        }

        static long Puzzle1(long cardPublicKey, long doorPublicKey)
        {
            int cardLoopSize = transform(7, cardPublicKey);
            int doorLoopSize = transform(7, doorPublicKey);

            return untransform(cardPublicKey, doorLoopSize);
        }

        static long untransform(long number, long loopSize)
        {
            long value = 1;
            for (int i = 0; i < loopSize; i++)
            {
                value *= number;
                value %= 20201227;
            }
            return value;
        }

        static int transform(long subject, long target)
        {
            long value = 1;
            int loopCount = 0;
            while (value != target)
            {
                value *= subject;
                value %= 20201227;
                loopCount++;
            }
            return loopCount;
        }
    }
}
