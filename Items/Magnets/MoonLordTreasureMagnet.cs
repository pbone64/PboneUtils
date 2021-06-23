using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Magnets
{
    // I just cannot get this sprite looking good and, I know it's petty, but I don't want this mod to look bad
    // See you in a future versions, comrade
    public class MoonLordTreasureMagnet : RightClickToggleItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.MagnetItemsToggle;
        public override bool DrawGlowmask => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Purple;
            item.value = Item.sellPrice(0, 45, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<MagnetPlayer>().MoonLordTreasureMagnet = Enabled;
        }

        public override void AddRecipes()
        {
            //base.AddRecipes();
            //ModRecipe recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ModContent.ItemType<RunicTreasureMagnet>());
            //recipe.AddIngredient(ItemID.LunarBar, 3);
            //recipe.AddTile(TileID.LunarCraftingStation);
            //recipe.SetResult(this);
            //recipe.AddRecipe();
        }
    }
}
