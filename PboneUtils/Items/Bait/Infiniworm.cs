using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Bait
{
    public class Infiniworm : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.EndlessBaitToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(0, 1, 0, 0);

            item.bait = 50;
            item.consumable = false;
        }

        public override bool ConsumeAmmo(Player player) => false;
        public override bool ConsumeItem(Player player) => false;

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BagOfWorms>());
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
