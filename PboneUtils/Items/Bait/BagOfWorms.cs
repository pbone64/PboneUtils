using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Bait
{
    public class BagOfWorms : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.EndlessBaitToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().EndlessBaitToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 50, 0);

            Item.bait = 25;
            Item.consumable = false;
        }

        public override bool ConsumeItem(Player player) => false;
        public override bool CanBeConsumedAsAmmo(Item weapon, Player player) => false;
        public override bool? CanConsumeBait(Player player) => false;

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Worm, 5).AddIngredient(ItemID.Bone, 10).AddTile(TileID.WorkBenches).Register();
        }
    }
}
