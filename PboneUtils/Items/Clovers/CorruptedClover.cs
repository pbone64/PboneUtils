using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Clovers
{
    public class CorruptedClover : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.CloversToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().CloversToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.accessory = true;
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<CloverPlayer>().TryChangeCloverMode(CloverPlayer.CorruptedClover);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<CloverPlayer>().TryChangeCloverMode(CloverPlayer.CorruptedClover);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<FourLeafClover>()
                .AddIngredient(ItemID.PurpleSolution, 2)
                .AddTile(TileID.DemonAltar)
                .Register();
            CreateRecipe()
                .AddIngredient<FourLeafClover>()
                .AddIngredient(ItemID.RedSolution, 2)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
