using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Arena
{
    public class PreperationStation : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.ArenaItemsToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().ArenaItemsToggle;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AmmoBox);
            Item.consumable = false;
            Item.maxStack = 1;
            Item.createTile = ModContent.TileType<PreperationStationTile>();

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.SharpeningStation, 5).AddIngredient(ItemID.AmmoBox, 5).AddIngredient(ItemID.CrystalBall, 5).AddIngredient(ItemID.BewitchingTable, 5).AddIngredient(ItemID.WarTable, 2).Register();
        }
    }
}
