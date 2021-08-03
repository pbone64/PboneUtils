using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Tools
{
    public class ShadowPearl : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.ShadowPearlToggle;

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodPlatform);
            item.consumable = false;
            item.createTile = ModContent.TileType<ShadowPearlTile>();
            item.rare = ItemRarityID.Blue;
            item.maxStack = 20;

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup(PboneUtils.Recipes.AnyDemoniteBar, 10);
            recipe.AddRecipeGroup(PboneUtils.Recipes.AnyShadowScale, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
