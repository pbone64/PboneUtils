using PboneUtils.Projectiles.Selection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Tools
{
    public class GreaterStaffOfRegrowth : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.GreaterStaffOfRegrowthToggle;
        public override bool ShowItemIconWhenInRange => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 10;
            item.useTime = 10;
            item.shoot = ModContent.ProjectileType<GreaterStaffOfRegrowthPro>();
            item.channel = true;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.tileBoost += 20;
            item.UseSound = SoundID.Item64;
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
