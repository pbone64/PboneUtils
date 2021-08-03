using PboneUtils.Projectiles.Selection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class GreaterStaffOfRegrowth : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.GreaterStaffOfRegrowthToggle;
        public override bool ShowItemIconWhenInRange => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.shoot = ModContent.ProjectileType<GreaterStaffOfRegrowthPro>();
            Item.channel = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.tileBoost += 20;
            Item.UseSound = SoundID.Item64;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StaffofRegrowth, 1);
            recipe.AddIngredient(ItemID.PurificationPowder, 100);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
