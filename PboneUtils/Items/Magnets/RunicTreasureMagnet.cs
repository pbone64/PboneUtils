using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Magnets
{
    public class RunicTreasureMagnet : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.MagnetItemsToggle;
        public override bool DrawGlowmask => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 30, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<MagnetPlayer>().RunicTreasureMagnet = Enabled;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<SpectreTreasureMagnet>()).AddIngredient(ItemID.FragmentSolar, 2).AddIngredient(ItemID.FragmentVortex, 2).AddIngredient(ItemID.FragmentNebula, 2).AddIngredient(ItemID.FragmentStardust, 2).AddTile(TileID.LunarCraftingStation).Register();
        }
    }
}
