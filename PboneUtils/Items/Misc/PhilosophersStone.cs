using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Misc
{
    public class PhilosophersStone : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.PhilosophersStoneToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<PbonePlayer>().PhilosophersStone = Enabled;
        }
    }
}
