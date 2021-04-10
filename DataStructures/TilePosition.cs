using Terraria;

namespace PboneUtils.DataStructures
{
    public struct TilePosition
    {
        public Tile Tile => Framing.GetTileSafely(X, Y);

        public int X;
        public int Y;

        public TilePosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
