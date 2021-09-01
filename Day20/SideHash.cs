namespace Day20
{
    internal struct SideHash
    {
        internal int Forward { get; private set; }
        internal int Backward { get; private set; }

        internal SideHash(int forward, int backward)
        {
            this.Forward = forward;
            this.Backward = backward;
        }

        internal bool Match(SideHash other, out bool flip)
        {
            flip = false;
            if (this.Forward == other.Forward || this.Backward == other.Backward)
            {
                flip = true;
                return true;
            }
            return (this.Forward == other.Backward || this.Backward == other.Forward);
        }
    }
}