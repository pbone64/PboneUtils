using PboneUtils.MiscModPlayers;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Misc
{
    public class StarMagnet : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.StarMagnetToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<StarMagnetPlayer>().StarMagnet = Enabled;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.FallenStar, 5).AddRecipeGroup(PboneUtils.Recipes.AnyDemoniteBar, 7).AddRecipeGroup(PboneUtils.Recipes.AnyShadowScale, 10).AddTile(TileID.SkyMill).Register();
        }
    }
}