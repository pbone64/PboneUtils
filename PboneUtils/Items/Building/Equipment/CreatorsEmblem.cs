using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Building.Equipment
{
    public class CreatorsEmblem : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.BuildingItemToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().BuildingItemToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 0, 55, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.tileSpeed += 0.25f;
            player.wallSpeed += 0.25f;
            player.pickSpeed -= 0.25f;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<BuildersEmblem>()).AddIngredient(ModContent.ItemType<MinersEmblem>()).AddIngredient(ItemID.HellstoneBar, 4).AddTile(TileID.TinkerersWorkbench).Register();
        }
    }
}
