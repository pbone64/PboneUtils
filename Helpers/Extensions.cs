using Terraria;
using Terraria.ID;

namespace PboneUtils.Helpers
{
    public static class Extensions
    {
        public static bool IsVanilla(this Item item) => item.type < ItemID.Count;
        public static bool IsModded(this Item item) => !item.IsVanilla();

        public static void TryIncreaseMaxStack(this Item item, int newStack)
        {
            if (item.maxStack < newStack)
            {
                item.maxStack = newStack;
            }
        }
    }
}
