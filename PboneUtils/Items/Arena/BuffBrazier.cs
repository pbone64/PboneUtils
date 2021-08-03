using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Arena
{
    public class BuffBrazier : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.ArenaItemsToggle;

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodPlatform);
            item.consumable = false;
            item.createTile = ModContent.TileType<BuffBrazierTile>();
            item.rare = ItemRarityID.Lime;
            item.maxStack = 1;

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Campfire, 10);
            recipe.AddIngredient(ItemID.HeartLantern, 10);
            recipe.AddIngredient(ItemID.StarinaBottle, 10);
            recipe.AddIngredient(ItemID.Sunflower, 15);
            recipe.AddIngredient(ItemID.BottledHoney, 15);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
