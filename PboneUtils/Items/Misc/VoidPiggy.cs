using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Misc
{
    public class VoidPiggy : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.VoidPiggyToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<PbonePlayer>().VoidPig = Enabled;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.PiggyBank).AddIngredient(ItemID.Bone, 10).AddIngredient(ItemID.JungleSpores, 5).AddRecipeGroup(PboneUtils.Recipes.AnyShadowScale, 10).AddTile(TileID.DemonAltar).Register();
        }
    }
}
