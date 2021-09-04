using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day23
{
    class Program
    {
        static List<int> cups = new List<int>();
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            foreach (char c in input[0])
            {
                cups.Add((int)c - 48);
            }
            Console.WriteLine("Puzzle 1: " + Puzzle1());
        }

        static int Puzzle1()
        {
            List<int> selectedCups = new List<int>();
            int currentCupIndex = 0;
            for (int move = 0; move < 100; move++)
            {
                int currentCupLabel = cups[currentCupIndex];

                List<int> indicesToPickUp = new List<int>();
                for (int i = 1; i <= 3; i++)
                {
                    indicesToPickUp.Add((currentCupIndex + i) % cups.Count);
                }
                foreach (int index in indicesToPickUp)
                {
                    selectedCups.Add(cups[index]);
                }
                indicesToPickUp.Sort();
                indicesToPickUp.Reverse();
                foreach (int index in indicesToPickUp)
                {
                    cups.RemoveAt(index);
                }

                //Find destination cup
                int destinationCupLabel = currentCupLabel;
                do
                {
                    destinationCupLabel--;
                    if (destinationCupLabel == 0) destinationCupLabel = 9;
                } while (selectedCups.Contains(destinationCupLabel));

                //Get index of destination cup
                int destinationCupIndex = cups.FindIndex(label => label == destinationCupLabel);

                //Reinsert cups
                while (selectedCups.Count > 0)
                {
                    cups.Insert(destinationCupIndex + (4 - selectedCups.Count), selectedCups[0]);
                    selectedCups.RemoveAt(0);
                }
                currentCupIndex = cups.FindIndex(label => label == currentCupLabel);
                currentCupIndex = (currentCupIndex == cups.Count - 1) ? 0 : currentCupIndex + 1;
            }
            int currentIndex = cups.FindIndex(label => label == 1);
            List<int> finalCupOrder = new List<int>();
            for (int i = 1; i <= 8; i++)
            {
                finalCupOrder.Add(cups[(currentIndex + i) % cups.Count]);
            }

            return Convert.ToInt32(String.Join("", finalCupOrder));
        }
    }
}
