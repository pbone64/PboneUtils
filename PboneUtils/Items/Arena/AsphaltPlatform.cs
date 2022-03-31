using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

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
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 200;
        }

        public override void AddRecipes()
        {
            CreateRecipe(2).AddIngredient(ItemID.AsphaltBlock).Register();
            CreateRecipe(1).AddIngredient(this, 2).ReplaceResult(ItemID.AsphaltBlock);
        }
    }
}
