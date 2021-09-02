using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Day22
{
    class Program
    {
        static Queue<int> deck1 = new Queue<int>();
        static Queue<int> deck2 = new Queue<int>();
        static bool recursionEnabled = false;

        static void Main(string[] args)
        {
            ProcessInput();
            Console.WriteLine("Puzzle 1: " + Puzzle1());
            recursionEnabled = true;
            ProcessInput();
            Console.WriteLine("Puzzle 2: " + Puzzle2());
        }

        static void ProcessInput()
        {
            deck1.Clear();
            deck2.Clear();
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
        }

        static int Puzzle1()
        {
            (int winner, Queue<int> winnersDeck) = PlayRound(CopyDeck(deck1), CopyDeck(deck2));

            int result = 0;
            while (winnersDeck.Count > 0)
            {
                result += winnersDeck.Count * winnersDeck.Dequeue();
            }

            return result;
        }

        static int Puzzle2()
        {
            (int winner, Queue<int> winnersDeck) = PlayRound(CopyDeck(deck1), CopyDeck(deck2));

            int result = 0;
            while (winnersDeck.Count > 0)
            {
                result += winnersDeck.Count * winnersDeck.Dequeue();
            }

            return result;
        }

        static Queue<int> CopyDeck(Queue<int> deck, int count = 0)
        {
            count = (count == 0) ? deck.Count : count;
            int[] copiedDeck = new int[deck.Count];
            deck.CopyTo(copiedDeck, 0);
            int[] shrinkedDeck = new int[count];
            for (int i = 0; i < count; i++)
            {
                shrinkedDeck[i] = copiedDeck[i];
            }
            return new Queue<int>(shrinkedDeck);
        }

        static (int, Queue<int>) PlayRound(Queue<int> deck1, Queue<int> deck2)
        {
            HashSet<string> decks = new HashSet<string>();
            while (deck1.Count > 0 && deck2.Count > 0)
            {
                if (recursionEnabled)
                {
                    string hash = HashDecks(CopyDeck(deck1), CopyDeck(deck2));
                    if (decks.Contains(hash)) return (1, deck1);
                    else decks.Add(HashDecks(CopyDeck(deck1), CopyDeck(deck2)));
                }

                int card1 = deck1.Dequeue();
                int card2 = deck2.Dequeue();

                if (recursionEnabled && card1 <= deck1.Count && card2 <= deck2.Count)
                {
                    (int winner, Queue<int> winnersDeck) = PlayRound(CopyDeck(deck1, card1), CopyDeck(deck2, card2));

                    if (winner == 1)
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
                else
                {
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
            }

            return (deck1.Count == 0)
            ? (2, deck2)
            : (1, deck1);
        }

        static string HashDecks(Queue<int> deck1, Queue<int> deck2)
        {
            StringBuilder hashBuilder = new StringBuilder();
            while (deck1.Count > 0)
            {
                hashBuilder.Append(deck1.Dequeue().ToString());
            }
            hashBuilder.Append(":");
            while (deck2.Count > 0)
            {
                hashBuilder.Append(deck2.Dequeue().ToString());
            }
            return hashBuilder.ToString();
        }
    }
}
