using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Clovers
{
    public class FourLeafClover : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.CloversToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().CloversToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.accessory = true;
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<CloverPlayer>().TryChangeCloverMode(CloverPlayer.FourLeafClover);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<CloverPlayer>().TryChangeCloverMode(CloverPlayer.FourLeafClover);
        }
    }
}
