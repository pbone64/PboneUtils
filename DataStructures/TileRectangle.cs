using Microsoft.Xna.Framework;
using System;

namespace PboneUtils.DataStructures
{
    public struct TileRectangle : IEquatable<TileRectangle>
    {
        public Point StartPosition;
        public Point EndPosition;

        public TileRectangle(Point start, Point end)
        {
            StartPosition = start;
            EndPosition = end;
        }

        public TilePosition[,] GetTiles()
        {
            Rectangle size = GetRectangle();

            TilePosition[,] tiles = new TilePosition[size.Width, size.Height];

            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    tiles[i, j] = new TilePosition(i, j);
                }
            }

            return tiles;
        }

        public Rectangle GetRectangle() => new Rectangle(StartPosition.X, StartPosition.Y,
    EndPosition.X - StartPosition.X, EndPosition.Y - StartPosition.Y);

        public override int GetHashCode() => Tuple.Create(StartPosition.X ^ EndPosition.X, StartPosition.Y ^ EndPosition.Y).GetHashCode();
        public override bool Equals(object obj) => obj is TileRectangle rect && Equals(rect);
        public bool Equals(TileRectangle other) => StartPosition == other.StartPosition && EndPosition == other.EndPosition;

        public static bool operator ==(TileRectangle left, TileRectangle right) => left.Equals(right);
        public static bool operator !=(TileRectangle left, TileRectangle right) => !left.Equals(right);
    }
}
