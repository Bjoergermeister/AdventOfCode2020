namespace Day17
{
    class Envelope
    {
        #region Fields
        internal int MinX { get; set; } = -1;
        internal int MaxX { get; set; }
        internal int MinY { get; set; } = -1;
        internal int MaxY { get; set; }
        internal int MinZ { get; set; } = -1;
        internal int MaxZ { get; set; } = 1;
        internal int MinW { get; set; } = -1;
        internal int MaxW { get; set; } = 1;
        #endregion

        internal Envelope(string[] startingGrid)
        {
            this.MaxX = startingGrid[0].Length;
            this.MaxY = startingGrid.Length;
        }

        #region Methods
        internal void Expand()
        {
            this.MinX--;
            this.MaxX++;
            this.MinY--;
            this.MaxY++;
            this.MinZ--;
            this.MaxZ++;
            this.MinW--;
            this.MaxW++;
        }
        #endregion
    }
}