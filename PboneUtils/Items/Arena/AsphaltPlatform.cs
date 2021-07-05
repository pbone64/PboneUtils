using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Arena
{
    public class AsphaltPlatform : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.ArenaItemsToggle;

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodPlatform);
            item.createTile = ModContent.TileType<AsphaltPlatformTile>();

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AsphaltBlock);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 2);
            recipe.SetResult(ItemID.AsphaltBlock);
            recipe.AddRecipe();
        }
    }
}
