using PboneUtils.Projectiles.Selection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class GreaterStaffOfRegrowth : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.GreaterStaffOfRegrowthToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().GreaterStaffOfRegrowthToggle;
        public override bool ShowItemIconWhenInRange => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.Swing;
            UseTime = 10;
            Item.shoot = ModContent.ProjectileType<GreaterStaffOfRegrowthPro>();
            Item.channel = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.tileBoost += 20;
            Item.UseSound = SoundID.Item64;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.StaffofRegrowth, 1).AddIngredient(ItemID.PurificationPowder, 100).AddTile(TileID.WorkBenches).Register();
        }
    }
}
