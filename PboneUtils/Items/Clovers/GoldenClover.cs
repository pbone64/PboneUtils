using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Clovers
{
    public class GoldenClover : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.CloversToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 25, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<CloverPlayer>().TryChangeCloverMode(CloverPlayer.GoldenClover);
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<FourLeafClover>()
                .AddIngredient(ItemID.ChlorophyteBar, 8)
                .AddIngredient(ItemID.WhitePearl)
                .AddIngredient(ItemID.BlackPearl)
                .AddIngredient(ItemID.PinkPearl)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
