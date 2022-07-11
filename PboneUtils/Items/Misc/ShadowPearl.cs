using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class ShadowPearl : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.ShadowPearlToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().ShadowPearlToggle;

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
            CreateRecipe(1).AddRecipeGroup(ModRecipeManager.Recipes.AnyDemoniteBar, 10).AddRecipeGroup(ModRecipeManager.Recipes.AnyShadowScale, 5).AddTile(TileID.DemonAltar).Register();
        }
    }
}
