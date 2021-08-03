using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Bait
{
    public class BagOfWorms : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.EndlessBaitToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 50, 0);

            Item.bait = 25;
            Item.consumable = false;
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
