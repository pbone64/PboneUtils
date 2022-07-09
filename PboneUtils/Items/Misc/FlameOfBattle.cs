using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class FlameOfBattle : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.SpawnRateItemsToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().SpawnRateItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 2, 50, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.WaterCandle, 5).AddIngredient(ItemID.BattlePotion, 5).AddIngredient(ItemID.SoulofNight, 5).AddTile(TileID.DemonAltar).Register();
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
