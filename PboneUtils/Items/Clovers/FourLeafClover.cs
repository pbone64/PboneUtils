using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Clovers
{
    public class FourLeafClover : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.CloversToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<CloverPlayer>().TryChangeCloverMode(CloverPlayer.FourLeafClover);
        }
    }
}
