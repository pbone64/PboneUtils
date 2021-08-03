using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class ShadowPearl : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.ShadowPearlToggle;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodPlatform);
            Item.consumable = false;
            Item.createTile = ModContent.TileType<ShadowPearlTile>();
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 20;

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
