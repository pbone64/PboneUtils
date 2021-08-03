using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Misc
{
    public class GreedyChest : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.GreedyChestToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<PbonePlayer>().GreedyChest = Enabled;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldChest);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
