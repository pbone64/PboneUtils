using PboneUtils.Helpers;
using PboneUtils.Items.CellPhoneApps;
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
            On.Terraria.Player.Spawn += Player_Spawn;
        }

        // Make the player not go to spawn under very specific conditions so teleportation app works
        private void Player_Spawn(On.Terraria.Player.orig_Spawn orig, Player self)
        {
            if (!self.dead && // If you are not dead
                self.HeldItem.type == ItemID.CellPhone && // If you're holding a cell phone
                self.HasApp<TeleportationApp>() && // If you have a teleporation app
                self.altFunctionUse == 2 && // If you're right-clicking
                self.itemTime == PlayerHooks.TotalUseTime(self.HeldItem.useTime, self, self.HeldItem) / 2) // if it's the frame when cell phone is used
                return;

            orig(self);
        }

        // Make terraria not try to consume a wormhole potion if you have a wormhole app
        private void Player_TakeUnityPotion(On.Terraria.Player.orig_TakeUnityPotion orig, Player self)
        {
            if (!self.HasApp<WormholeApp>())
                orig(self);
        }

        // Make wormhole app count as a wormhole potion
        private bool Player_HasUnityPotion(On.Terraria.Player.orig_HasUnityPotion orig, Player self)
        {
            bool hasApp = self.HasApp<WormholeApp>();

            return orig(self) || hasApp;
        }

        // Stop tombstones from dropping, if enabled
        private void Player_DropTombstone(On.Terraria.Player.orig_DropTombstone orig, Player self, int coinsOwned, Terraria.Localization.NetworkText deathText, int hitDirection)
        {
            if (!PboneUtilsConfig.Instance.NoMoreGraves)
                orig(self, coinsOwned, deathText, hitDirection);
        }

        // Redirect QuickSpawnItem calls to TreasureBagValueCalculator during loading to average treasure bag sell prices
        private void Player_QuickSpawnItem_int_int(On.Terraria.Player.orig_QuickSpawnItem_int_int orig, Player self, int item, int stack)
        {
            if (!TreasureBagValueCalculator.Loading)
                orig(self, item, stack);
            else
                TreasureBagValueCalculator.HandleQuickSpawnItem(item, stack);
        }
    }
}
