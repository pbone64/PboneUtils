using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Tools
{
    public class PhilosophersStone : RightClickToggleItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.PhilosophersStone;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
            item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<PbonePlayer>().PhilosophersStone = Enabled;
        }
    }
}
