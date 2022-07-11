using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Building.Equipment
{
    public class MinersEmblem : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.BuildingItemToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().BuildingItemToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 0, 25, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pickSpeed -= 0.15f;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddRecipeGroup(ModRecipeManager.Recipes.AnyCopperBar, 8).AddRecipeGroup(RecipeGroupID.IronBar, 6).AddRecipeGroup(ModRecipeManager.Recipes.AnySilverBar, 4).AddRecipeGroup(ModRecipeManager.Recipes.AnyGoldBar, 2).AddRecipeGroup(ModRecipeManager.Recipes.AnyDemoniteBar, 8).AddTile(TileID.Anvils).Register();
        }
    }
}
