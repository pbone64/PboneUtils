using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Misc
{
    public class FlameOfBattle : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.SpawnRateItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 2, 50, 0);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WaterCandle, 5);
            recipe.AddIngredient(ItemID.BattlePotion, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            PbonePlayer modPlayer = player.GetModPlayer<PbonePlayer>();

            if (Enabled)
            {
                modPlayer.SpawnRateMultiplier = 0.075f;
                modPlayer.MaxSpawnsMultiplier = 12;
            }
        }
    }
}
