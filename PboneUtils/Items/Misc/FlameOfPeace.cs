using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Misc
{
    public class FlameOfPeace : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.SpawnRateItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(0, 2, 50, 0);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PeaceCandle, 5);
            recipe.AddIngredient(ItemID.CalmingPotion, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
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
                modPlayer.SpawnRateMultiplier += 9;
                modPlayer.MaxSpawnsMultiplier -= 10;
            }
        }
    }
}
