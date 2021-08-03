using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Arena
{
    public class PreperationStation : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.ArenaItemsToggle;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AmmoBox);
            Item.consumable = false;
            Item.maxStack = 1;
            Item.createTile = ModContent.TileType<PreperationStationTile>();

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SharpeningStation, 5);
            recipe.AddIngredient(ItemID.AmmoBox, 5);
            recipe.AddIngredient(ItemID.CrystalBall, 5);
            recipe.AddIngredient(ItemID.BewitchingTable, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
