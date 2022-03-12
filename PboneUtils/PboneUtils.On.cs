using PboneUtils.Helpers;
using PboneUtils.Items.Building.Terraforming;
using PboneUtils.Items.CellPhoneApps;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public void Load_On()
        {
            On.Terraria.Player.QuickSpawnItem_IEntitySource_int_int += Player_QuickSpawnItem_int_int;
            On.Terraria.Player.DropTombstone += Player_DropTombstone;

            On.Terraria.Player.HasUnityPotion += Player_HasUnityPotion;
            On.Terraria.Player.TakeUnityPotion += Player_TakeUnityPotion;
            On.Terraria.Player.Spawn += Player_Spawn;

            On.Terraria.Player.ScrollHotbar += Player_ScrollHotbar;
        }

        // Make the hotbar not scroll if you're holding a Terraformer and you have the scroll mode key held down
        private void Player_ScrollHotbar(On.Terraria.Player.orig_ScrollHotbar orig, Player self, int Offset)
        {
            if (self.GetModPlayer<TerraformingPlayer>().HoldingTerraformer && PboneUtilsHotkeys.TerraformingScrollModeKey.Current)
                return;

            orig(self, Offset);
        }

        // Make the player not go to spawn under very specific conditions so teleportation app works
        private void Player_Spawn(On.Terraria.Player.orig_Spawn orig, Player self, PlayerSpawnContext context)
        {
            if (context != PlayerSpawnContext.RecallFromItem)
            {
                orig(self, context);
                return;
            }

            if (!self.dead && // If you are not dead
                self.HeldItem.type == ItemID.CellPhone && // If you're holding a cell phone
                self.HasApp<TeleportationApp>() && // If you have a teleporation app
                self.altFunctionUse == 2 && // If you're right-clicking
                self.itemTime == CombinedHooks.TotalUseTime(self.HeldItem.useTime, self, self.HeldItem) / 2) // If it's the frame when cell phone is used
                return;

            orig(self, context);
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
        private int Player_QuickSpawnItem_int_int(On.Terraria.Player.orig_QuickSpawnItem_IEntitySource_int_int orig, Player self, IEntitySource source, int item, int stack)
        {
            if (!TreasureBagValueCalculator.Loading)
                return orig(self, source, item, stack);
            else
                return TreasureBagValueCalculator.HandleQuickSpawnItem(item, stack);
        }
    }
}
