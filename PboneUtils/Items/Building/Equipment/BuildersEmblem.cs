using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Building.Equipment
{
    public class BuildersEmblem : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.BuildingItemToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 0, 25, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.tileSpeed += 0.15f;
            player.wallSpeed += 0.15f;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddRecipeGroup(PboneUtils.Recipes.AnyCopperBar, 8).AddRecipeGroup(RecipeGroupID.IronBar, 6).AddRecipeGroup(PboneUtils.Recipes.AnySilverBar, 4).AddRecipeGroup(PboneUtils.Recipes.AnyGoldBar, 2).AddRecipeGroup(PboneUtils.Recipes.AnyDemoniteBar, 8).AddTile(TileID.Anvils).Register();
        }
    }
}
