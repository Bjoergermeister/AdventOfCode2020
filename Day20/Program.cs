using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day20
{
    class Program
    {
        static List<Tile> unorderedTiles = new List<Tile>();
        static List<Tile> orderedTiles = new List<Tile>();
        static Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();
        static Tile upperLeftTile = null;
        static int imageRotation = 0;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            //Prepare tiles
            int index = 0;
            while (index < input.Length)
            {
                int id = Convert.ToInt32(input[index].Substring(5, 4));
                Tile tile = new Tile(id, input[index..(index + 11)]);
                unorderedTiles.Add(tile);
                tiles.Add(id, tile);
                index += 12;
            }

            Console.WriteLine("Puzzle 1: " + Puzzle1());
            Console.WriteLine("Puzzle 2: " + Puzzle2());
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

                if (orderedTiles.Contains(currentTile) == false) orderedTiles.Add(currentTile);
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
                    upperLeftTile = tile;
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

        static ulong Puzzle2()
        {
            int gridSize = (int)Math.Sqrt(orderedTiles.Count);
            int imageSize = gridSize * 8; //Original tile is 10x10, minus borders is 8x8

            Tile image = new Tile(imageSize, imageSize);

            //Create grid of tiles
            int numberOfHashtags = 0;
            Tile firstTileInRow = upperLeftTile;
            for (int j = 0; j < gridSize; j++)
            {
                Tile currentTile = firstTileInRow;
                for (int i = 0; i < gridSize; i++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            //Constructing the image while counting the number of hashtags
                            char position = currentTile.GetIndex(x + 1, y + 1);
                            if (position == '#') numberOfHashtags++;
                            image.SetIndex(i * 8 + x, j * 8 + y, position);
                        }
                    }

                    if (i < gridSize - 1) currentTile = tiles[currentTile.RightTile];
                }
                if (j < gridSize - 1) firstTileInRow = tiles[firstTileInRow.BottomTile];
            }

            //Find sea monsters
            List<Tuple<int, int>> indices = new List<Tuple<int, int>>
            {
                Tuple.Create(0, 1),
                Tuple.Create(1, 2),
                Tuple.Create(4, 2),
                Tuple.Create(5, 1),
                Tuple.Create(6, 1),
                Tuple.Create(7, 2),
                Tuple.Create(10, 2),
                Tuple.Create(11, 1),
                Tuple.Create(12, 1),
                Tuple.Create(13, 2),
                Tuple.Create(16, 2),
                Tuple.Create(17, 1),
                Tuple.Create(18, 1),
                Tuple.Create(19, 1),
                Tuple.Create(18, 0)
            };

            int seaMonsterWidth = 20;
            int seaMonsterHeight = 3;

            while (true)
            {
                //Search sea monsters
                int seaMonsterIndices = 0;
                for (int j = 0; j < imageSize - seaMonsterHeight; j++)
                {
                    for (int i = 0; i < imageSize - seaMonsterWidth; i++)
                    {
                        if (indices.All(index => image.GetIndex(i + index.Item1, j + index.Item2) == '#')) seaMonsterIndices += indices.Count;
                    }
                }
                if (seaMonsterIndices > 0)
                {
                    return (ulong)(numberOfHashtags - seaMonsterIndices);
                }

                //If no sea monster were found, rotate and flip image and search again
                imageRotation++;
                if (imageRotation == 4) image.RotateAndFlip(1, true, false);
                else if (imageRotation == 8) image.RotateAndFlip(1, true, true);
                else image.RotateAndFlip(1, false, false);
            }
        }
    }
}
