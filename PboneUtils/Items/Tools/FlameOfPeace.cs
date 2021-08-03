using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Tools
{
    public class FlameOfPeace : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.SpawnRateItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Pink;
            item.value = Item.buyPrice(0, 2, 50, 0);
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
