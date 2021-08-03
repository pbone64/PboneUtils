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
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 30, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<MagnetPlayer>().RunicTreasureMagnet = Enabled;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SpectreTreasureMagnet>());
            recipe.AddIngredient(ItemID.FragmentSolar, 2);
            recipe.AddIngredient(ItemID.FragmentVortex, 2);
            recipe.AddIngredient(ItemID.FragmentNebula, 2);
            recipe.AddIngredient(ItemID.FragmentStardust, 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
