using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class FlameOfPeace : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.SpawnRateItemsToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().SpawnRateItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(0, 2, 50, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.PeaceCandle, 5).AddIngredient(ItemID.CalmingPotion, 5).AddIngredient(ItemID.SoulofLight, 5).AddTile(TileID.DemonAltar).Register();
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
