using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Arena
{
    public class AsphaltPlatform : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.ArenaItemsToggle;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodPlatform);
            Item.createTile = ModContent.TileType<AsphaltPlatformTile>();

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            CreateRecipe(2).AddIngredient(ItemID.AsphaltBlock).Register();
            CreateRecipe(1).AddIngredient(this, 2).ReplaceResult(ItemID.AsphaltBlock);
        }
    }
}
