using PboneUtils.Items.CellPhoneApps;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Helpers
{
    public static class Extensions
    {
        public static bool HasApp<T>(this Player self) where T : AppItem
        {
            bool hasApp = false;
            T instance = ModContent.GetInstance<T>();

            for (int i = 0; i < Main.InventorySlotsTotal; i++)
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
