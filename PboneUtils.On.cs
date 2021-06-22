using PboneUtils.Items.CellphoneApps;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public void Load_On()
        {
            On.Terraria.Player.QuickSpawnItem_int_int += Player_QuickSpawnItem_int_int;
            On.Terraria.Player.DropTombstone += Player_DropTombstone;

            On.Terraria.Player.HasUnityPotion += Player_HasUnityPotion;
            On.Terraria.Player.TakeUnityPotion += Player_TakeUnityPotion;
        }

        private void Player_TakeUnityPotion(On.Terraria.Player.orig_TakeUnityPotion orig, Player self)
        {
            bool hasApp = false;
            WormholeApp instance = ModContent.GetInstance<WormholeApp>();

            for (int i = 0; i < Main.maxInventory; i++)
            {
                Item query = self.inventory[i];

                if (query.type == ItemID.CellPhone &&
                    query.GetGlobalItem<AppGlobalItem>().Apps.Contains((instance.BaseID, instance.AppName)))
                    hasApp = true;
            }

            if (!hasApp)
                orig(self);
        }

        private bool Player_HasUnityPotion(On.Terraria.Player.orig_HasUnityPotion orig, Player self)
        {
            bool hasApp = false;
            WormholeApp instance = ModContent.GetInstance<WormholeApp>();

            for (int i = 0; i < Main.maxInventory; i++)
            {
                Item query = self.inventory[i];

                if (query.type == ItemID.CellPhone &&
                    query.GetGlobalItem<AppGlobalItem>().Apps.Contains((instance.BaseID, instance.AppName)))
                    hasApp = true;
            }

            return orig(self) || hasApp;
        }

        private void Player_DropTombstone(On.Terraria.Player.orig_DropTombstone orig, Player self, int coinsOwned, Terraria.Localization.NetworkText deathText, int hitDirection)
        {
            if (!PboneUtilsConfig.Instance.NoMoreGraves)
                orig(self, coinsOwned, deathText, hitDirection);
        }

        private void Player_QuickSpawnItem_int_int(On.Terraria.Player.orig_QuickSpawnItem_int_int orig, Player self, int item, int stack)
        {
            if (!TreasureBagValueCalculator.Loading)
                orig(self, item, stack);
            else
                TreasureBagValueCalculator.HandleQuickSpawnItem(self, item, stack);
        }
    }
}
