using PboneUtils.Projectiles.Storage;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Storage
{
    public class PetrifiedSafe : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.StorageItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.Swing;
            UseTime = 28;
            Item.UseSound = SoundID.Item37;
            Item.shoot = ModContent.ProjectileType<PetrifiedSafeProjectile>();
            Item.shootSpeed = 4;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }

        // TODO shoot subtract (0, 12)

        public override void AddRecipes()
        {
        }
    }
}
