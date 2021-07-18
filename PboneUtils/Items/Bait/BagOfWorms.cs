using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Bait
{
    public class BagOfWorms : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.EndlessBaitToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 0, 50, 0);

            item.bait = 25;
            item.consumable = false;
        }

        public override bool ConsumeAmmo(Player player) => false;
        public override bool ConsumeItem(Player player) => false;

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Worm, 5);
            recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
