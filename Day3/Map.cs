using System;

namespace Day3
{
    class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private string[] map;

        internal Map(string[] input)
        {
            this.map = input;
            this.Height = map.Length;
            this.Width = map[0].Length;
        }

        internal bool IsTree(int x, int y)
        {
            x = x % this.Width;
            return this.map[y][x] == '#';
        }
    }
}