using Terraria.ModLoader;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public void Load_On()
        {
            On.Terraria.Player.QuickSpawnItem_int_int += Player_QuickSpawnItem_int_int;
        }

        private void Player_QuickSpawnItem_int_int(On.Terraria.Player.orig_QuickSpawnItem_int_int orig, Terraria.Player self, int item, int stack)
        {
            if (!TreasureBagValueCalculator.Loading)
                orig(self, item, stack);
            else
                TreasureBagValueCalculator.HandleQuickSpawnItem(self, item, stack);
        }
    }
}
