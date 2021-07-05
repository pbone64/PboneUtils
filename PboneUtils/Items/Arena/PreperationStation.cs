using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Arena
{
    public class PreperationStation : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.ArenaItemsToggle;

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AmmoBox);
            item.consumable = false;
            item.maxStack = 1;
            item.createTile = ModContent.TileType<PreperationStationTile>();

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
