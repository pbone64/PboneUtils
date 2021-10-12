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
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 1, 0, 0);

            Item.bait = 50;
            Item.consumable = false;
        }

        public override bool ConsumeItem(Player player) => false;
        public override bool CanBeConsumedAsAmmo(Player player) => true;

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<BagOfWorms>()).AddIngredient(ItemID.SoulofNight, 5).AddIngredient(ItemID.SoulofLight, 5).AddTile(TileID.WorkBenches).Register();
        }
    }
}
