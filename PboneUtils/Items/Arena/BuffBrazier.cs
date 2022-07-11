using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Arena
{
    public class BuffBrazier : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.ArenaItemsToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().ArenaItemsToggle;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodPlatform);
            Item.consumable = false;
            Item.createTile = ModContent.TileType<BuffBrazierTile>();
            Item.rare = ItemRarityID.Lime;
            Item.maxStack = 1;

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Campfire, 10).AddIngredient(ItemID.HeartLantern, 10).AddIngredient(ItemID.StarinaBottle, 10).AddIngredient(ItemID.Sunflower, 15).AddIngredient(ItemID.BottledHoney, 15).Register();
        }
    }
}
