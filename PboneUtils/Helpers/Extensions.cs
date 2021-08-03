using PboneUtils.Items.CellPhoneApps;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Helpers
{
    public static class Extensions
    {
        public static bool IsVanilla(this Item item) => Item.type < ItemID.Count;
        public static bool IsModded(this Item item) => !Item.IsVanilla();

        public static void TryIncreaseMaxStack(this Item item, int newStack)
        {
            if (Item.maxStack < newStack)
            {
                Item.maxStack = newStack;
            }
        }

        public static void AddShopItem(this Chest shop, int item, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(item);
            nextSlot++;
        }

        public static bool HasApp<T>(this Player self) where T : AppItem
        {
            bool hasApp = false;
            T instance = ModContent.GetInstance<T>();

            for (int i = 0; i < Main.maxInventory; i++)
            {
                Item query = self.inventory[i];

                if (query.type == ItemID.CellPhone &&
                    query.GetGlobalItem<AppGlobalItem>().Apps.Contains((instance.BaseID, instance.AppName)))
                {
                    hasApp = true;
                    break;
                }
            }

            return hasApp;
        }
    }
}
