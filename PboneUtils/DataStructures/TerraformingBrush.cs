using Microsoft.Xna.Framework;
using System;

namespace PboneUtils.DataStructures
{
    public struct TerraformingBrush
    {
        public const int MAX_SIZE = 15;
        public const int MIN_SIZE = 1;

        public Point Position;
        public int Size;

        public TerraformingBrush(int size)
        {
            Position = new Point();
            Size = size;
        }

        public void IncreaseSize() => Size = Math.Min(Size + 2, MAX_SIZE);
        public void DecreaseSize() => Size = Math.Max(Size - 2, MIN_SIZE);

        public bool IsEven() => Size % 2 == 0;
        public (int width, int height) GetDimensions() => (Size, Size);
    }
}
