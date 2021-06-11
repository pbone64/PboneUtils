using Terraria.ModLoader;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public static void Load_On()
        {
            On.Terraria.Player.QuickSpawnItem_int_int += Player_QuickSpawnItem_int_int;
            On.Terraria.Player.DropTombstone += Player_DropTombstone;
        }

        private static void Player_DropTombstone(On.Terraria.Player.orig_DropTombstone orig, Terraria.Player self,
            int coinsOwned, Terraria.Localization.NetworkText deathText, int hitDirection)
        {
            if (!PboneUtilsConfig.Instance.NoMoreGraves)
                orig(self, coinsOwned, deathText, hitDirection);
        }

        private static void Player_QuickSpawnItem_int_int(On.Terraria.Player.orig_QuickSpawnItem_int_int orig,
            Terraria.Player self, int item, int stack)
        {
            TreasureBagValueCalculator.HandleQuickSpawnItem(orig, self, item, stack);
        }
    }
}