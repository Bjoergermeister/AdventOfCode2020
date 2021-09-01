using System;
using System.Collections.Generic;
using System.IO;

namespace Day22
{
    class Program
    {
        static Queue<int> deck1 = new Queue<int>();
        static Queue<int> deck2 = new Queue<int>();
        static void Main(string[] args)
        {
            //Process input
            bool spaceOccured = false;
            string[] input = File.ReadAllLines("input.txt");
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i].Length == 0)
                {
                    spaceOccured = true;
                    i++;
                    continue;
                }
                int card = Convert.ToInt32(input[i]);
                if (spaceOccured) deck2.Enqueue(card);
                else deck1.Enqueue(card);
            }

            Console.WriteLine("Puzzle 1: " + Puzzle1());
        }

        static int Puzzle1()
        {
            while (deck1.Count > 0 && deck2.Count > 0)
            {
                int card1 = deck1.Dequeue();
                int card2 = deck2.Dequeue();

                if (card1 > card2)
                {
                    deck1.Enqueue(card1);
                    deck1.Enqueue(card2);
                }
                else
                {
                    deck2.Enqueue(card2);
                    deck2.Enqueue(card1);
                }
            }

            Queue<int> winnersDeck = (deck1.Count > 0) ? deck1 : deck2;

            int result = 0;
            while (winnersDeck.Count > 0)
            {
                result += winnersDeck.Count * winnersDeck.Dequeue();
            }

            return result;
        }
    }
}
