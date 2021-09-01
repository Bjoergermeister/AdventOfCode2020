using System;

namespace Day20
{
    class Tile
    {
        public enum Sides
        {
            Left,
            Top,
            Right,
            Bottom
        }

        #region Fields        
        internal int Id { get; private set; }
        internal int LeftTile { get; set; } = 0;
        internal int RightTile { get; set; } = 0;
        internal int TopTile { get; set; } = 0;
        internal int BottomTile { get; set; } = 0;

        private int orientation = 0;
        private bool isFlippedHorizontally = false;
        private bool isFlippedVertically = false;
        private char[,] tile;
        #endregion

        internal Tile(int id, string[] tile)
        {
            this.tile = new char[10, 10];
            this.Id = id;

            for (int j = 0; j < tile.Length - 1; j++)
            {
                string row = tile[j + 1];
                for (int i = 0; i < row.Length; i++)
                {
                    this.tile[i, j] = row[i];
                }
            }
        }

        internal Tile(int width, int height)
        {
            this.Id = 0;
            this.tile = new char[width, height];
        }

        #region Methods
        internal bool Fits(Tile other)
        {
            SideHash[] sides = new SideHash[]{
                other.GetHashForSide(Sides.Left),
                other.GetHashForSide(Sides.Top),
                other.GetHashForSide(Sides.Right),
                other.GetHashForSide(Sides.Bottom)
            };

            //Top
            if (this.LeftTile == 0 && FitsLeft(other, sides)) return true;
            //Right
            if (this.RightTile == 0 && FitsRight(other, sides)) return true;
            //Bottom
            if (this.BottomTile == 0 && FitsBottom(other, sides)) return true;
            //Left
            if (this.TopTile == 0 && FitsTop(other, sides)) return true;

            return false;
        }
        private bool FitsLeft(Tile other, SideHash[] otherHashes)
        {
            SideHash sideHash = GetHashForSide(Sides.Left);
            for (int i = 0; i < otherHashes.Length; i++)
            {
                SideHash otherHash = otherHashes[i];
                if (sideHash.Match(otherHash, out bool flip))
                {
                    this.SetTile(Sides.Left, other.Id);
                    other.SetTile(Sides.Right, this.Id);

                    other.RotateAndFlip((2 - i), false, flip);
                    return true;
                }
            }
            return false;
        }
        private bool FitsRight(Tile other, SideHash[] otherHashes)
        {
            SideHash sideHash = GetHashForSide(Sides.Right);
            for (int i = 0; i < otherHashes.Length; i++)
            {
                SideHash otherHash = otherHashes[i];
                if (sideHash.Match(otherHash, out bool flip))
                {
                    this.SetTile(Sides.Right, other.Id);
                    other.SetTile(Sides.Left, this.Id);

                    other.RotateAndFlip((0 - i), false, flip);
                    return true;
                }
            }
            return false;
        }
        private bool FitsTop(Tile other, SideHash[] otherHashes)
        {
            SideHash sideHash = GetHashForSide(Sides.Top);
            for (int i = 0; i < otherHashes.Length; i++)
            {
                SideHash otherHash = otherHashes[i];
                if (sideHash.Match(otherHash, out bool flip))
                {
                    this.SetTile(Sides.Top, other.Id);
                    other.SetTile(Sides.Bottom, this.Id);

                    other.RotateAndFlip((3 - i), flip, false);
                    return true;
                }
            }
            return false;
        }
        private bool FitsBottom(Tile other, SideHash[] otherHashes)
        {
            SideHash sideHash = GetHashForSide(Sides.Bottom);
            for (int i = 0; i < otherHashes.Length; i++)
            {
                SideHash otherHash = otherHashes[i];
                if (sideHash.Match(otherHash, out bool flip))
                {
                    this.SetTile(Sides.Bottom, other.Id);
                    other.SetTile(Sides.Top, this.Id);

                    other.RotateAndFlip((1 - i), flip, false);

                    return true;
                }
            }
            return false;
        }

        internal void RotateAndFlip(int times, bool flipHorizontally, bool flipVertically)
        {
            if (flipHorizontally && this.isFlippedHorizontally == false) this.isFlippedHorizontally = true;
            if (flipVertically && this.isFlippedVertically == false) this.isFlippedVertically = true;

            if (times < 0) times += 4;
            this.orientation = (this.orientation + times) % 4;
        }

        internal void SetTile(Sides side, int tileID)
        {
            if (side == Sides.Left) this.LeftTile = tileID;
            if (side == Sides.Top) this.TopTile = tileID;
            if (side == Sides.Right) this.RightTile = tileID;
            if (side == Sides.Bottom) this.BottomTile = tileID;
        }

        internal SideHash GetHashForSide(Sides side)
        {
            int forward = 0;
            int backward = 0;

            switch (side)
            {
                case Sides.Top:
                    if (this.TopTile != 0) return new SideHash(0, 0);
                    if (GetIndex(0, 0) == '#') { forward += 1; backward += 512; }
                    if (GetIndex(1, 0) == '#') { forward += 2; backward += 256; }
                    if (GetIndex(2, 0) == '#') { forward += 4; backward += 128; }
                    if (GetIndex(3, 0) == '#') { forward += 8; backward += 64; }
                    if (GetIndex(4, 0) == '#') { forward += 16; backward += 32; }
                    if (GetIndex(5, 0) == '#') { forward += 32; backward += 16; }
                    if (GetIndex(6, 0) == '#') { forward += 64; backward += 8; }
                    if (GetIndex(7, 0) == '#') { forward += 128; backward += 4; }
                    if (GetIndex(8, 0) == '#') { forward += 256; backward += 2; }
                    if (GetIndex(9, 0) == '#') { forward += 512; backward += 1; }
                    return new SideHash(forward, backward);
                case Sides.Right:
                    if (this.RightTile != 0) return new SideHash(0, 0);
                    if (GetIndex(9, 0) == '#') { forward += 1; backward += 512; }
                    if (GetIndex(9, 1) == '#') { forward += 2; backward += 256; }
                    if (GetIndex(9, 2) == '#') { forward += 4; backward += 128; }
                    if (GetIndex(9, 3) == '#') { forward += 8; backward += 64; }
                    if (GetIndex(9, 4) == '#') { forward += 16; backward += 32; }
                    if (GetIndex(9, 5) == '#') { forward += 32; backward += 16; }
                    if (GetIndex(9, 6) == '#') { forward += 64; backward += 8; }
                    if (GetIndex(9, 7) == '#') { forward += 128; backward += 4; }
                    if (GetIndex(9, 8) == '#') { forward += 256; backward += 2; }
                    if (GetIndex(9, 9) == '#') { forward += 512; backward += 1; }
                    return new SideHash(forward, backward);
                case Sides.Bottom:
                    if (this.BottomTile != 0) return new SideHash(0, 0);
                    if (GetIndex(9, 9) == '#') { forward += 1; backward += 512; }
                    if (GetIndex(8, 9) == '#') { forward += 2; backward += 256; }
                    if (GetIndex(7, 9) == '#') { forward += 4; backward += 128; }
                    if (GetIndex(6, 9) == '#') { forward += 8; backward += 64; }
                    if (GetIndex(5, 9) == '#') { forward += 16; backward += 32; }
                    if (GetIndex(4, 9) == '#') { forward += 32; backward += 16; }
                    if (GetIndex(3, 9) == '#') { forward += 64; backward += 8; }
                    if (GetIndex(2, 9) == '#') { forward += 128; backward += 4; }
                    if (GetIndex(1, 9) == '#') { forward += 256; backward += 2; }
                    if (GetIndex(0, 9) == '#') { forward += 512; backward += 1; }
                    return new SideHash(forward, backward); ;
                case Sides.Left:
                    if (this.LeftTile != 0) return new SideHash(0, 0);
                    if (GetIndex(0, 9) == '#') { forward += 1; backward += 512; }
                    if (GetIndex(0, 8) == '#') { forward += 2; backward += 256; }
                    if (GetIndex(0, 7) == '#') { forward += 4; backward += 128; }
                    if (GetIndex(0, 6) == '#') { forward += 8; backward += 64; }
                    if (GetIndex(0, 5) == '#') { forward += 16; backward += 32; }
                    if (GetIndex(0, 4) == '#') { forward += 32; backward += 16; }
                    if (GetIndex(0, 3) == '#') { forward += 64; backward += 8; }
                    if (GetIndex(0, 2) == '#') { forward += 128; backward += 4; }
                    if (GetIndex(0, 1) == '#') { forward += 256; backward += 2; }
                    if (GetIndex(0, 0) == '#') { forward += 512; backward += 1; }
                    return new SideHash(forward, backward);
                default:
                    return new SideHash(-1, -1);
            }
        }
        internal char GetIndex(int x, int y)
        {
            int tileWidth = this.tile.GetLength(0);
            int tileHeight = this.tile.GetLength(1);

            int positionX = (isFlippedHorizontally) ? (tileWidth - 1) - x : x;
            int positionY = (isFlippedVertically) ? (tileHeight - 1) - y : y;

            switch (this.orientation)
            {
                case 0: return this.tile[positionX, positionY];
                case 1: return this.tile[positionY, (tileWidth - 1) - positionX];
                case 2: return this.tile[(tileWidth - 1) - positionX, (tileHeight - 1) - positionY];
                case 3: return this.tile[(tileHeight - 1) - positionY, positionX];
                default:
                    return '.';
            }
        }

        internal void SetIndex(int x, int y, char c)
        {
            this.tile[x, y] = c;
        }
        #endregion

    }
}