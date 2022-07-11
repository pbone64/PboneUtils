using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class GreedyChest : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.GreedyChestToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().GreedyChestToggle;

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
            CreateRecipe(1).AddIngredient(ItemID.GoldChest).AddIngredient(ItemID.SoulofNight, 2).AddTile(TileID.DemonAltar).Register();
        }
    }
}
