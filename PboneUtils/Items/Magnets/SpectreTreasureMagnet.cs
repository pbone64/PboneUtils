using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Magnets
{
    public class SpectreTreasureMagnet : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.MagnetItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 25, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<MagnetPlayer>().SpectreTreasureMagnet = Enabled;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<HallowedTreasureMagnet>());
            recipe.AddIngredient(ItemID.SpectreBar, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
