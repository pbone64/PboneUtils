using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Tools
{
    public class FlameOfBattle : RightClickToggleItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.SpawnRateItems;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Blue;
            item.value = Item.buyPrice(0, 2, 50, 0);
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
