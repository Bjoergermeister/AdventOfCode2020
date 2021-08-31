using System;
using System.Collections.Generic;
using System.IO;

namespace Day20
{
    class Program
    {
        static List<Tile> unorderedTiles = new List<Tile>();
        static List<Tile> orderedTiles = new List<Tile>();
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            //Prepare tiles
            int index = 0;
            while (index < input.Length)
            {
                int id = Convert.ToInt32(input[index].Substring(5, 4));
                unorderedTiles.Add(new Tile(id, input[index..(index + 11)]));
                index += 12;
            }

            ulong result = Puzzle1();
            Console.WriteLine("Puzzle 1: " + result);
        }

        static ulong Puzzle1()
        {
            Queue<Tile> nextTiles = new Queue<Tile>();

            nextTiles.Enqueue(unorderedTiles[0]);
            while (unorderedTiles.Count > 1)
            {
                Tile currentTile = nextTiles.Dequeue();
                for (int i = 0; i < unorderedTiles.Count; i++)
                {
                    Tile possiblyFittingTile = unorderedTiles[i];
                    if (currentTile.Id == possiblyFittingTile.Id) continue;

                    if (currentTile.Fits(possiblyFittingTile))
                    {
                        nextTiles.Enqueue(possiblyFittingTile);
                    };
                }

                orderedTiles.Add(currentTile);
                unorderedTiles.Remove(currentTile);
            }

            orderedTiles.Add(unorderedTiles[0]);

            //Find extrem tiles
            ulong upperLeft = 0, upperRight = 0, lowerLeft = 0, lowerRight = 0;

            foreach (Tile tile in orderedTiles)
            {
                //Upper left
                if (tile.LeftTile == 0 && tile.TopTile == 0 && tile.RightTile != 0 && tile.BottomTile != 0)
                {
                    upperLeft = (uint)tile.Id;
                    continue;
                }
                //Upper right
                if (tile.LeftTile != 0 && tile.TopTile == 0 && tile.RightTile == 0 && tile.BottomTile != 0)
                {
                    upperRight = (uint)tile.Id;
                    continue;
                }
                //Lower left
                if (tile.LeftTile == 0 && tile.TopTile != 0 && tile.RightTile != 0 && tile.BottomTile == 0)
                {
                    lowerLeft = (uint)tile.Id;
                    continue;
                }
                //Lower right
                if (tile.LeftTile != 0 && tile.TopTile != 0 && tile.RightTile == 0 && tile.BottomTile == 0)
                {
                    lowerRight = (uint)tile.Id;
                    continue;
                }
            }

            return upperLeft * upperRight * lowerLeft * lowerRight;
        }
    }
}
