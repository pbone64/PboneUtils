using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using PboneUtils.Projectiles.Storage;

namespace PboneUtils.Items.Storage
{
    public class DefendersCrystal : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.StorageItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.Swing;
            UseTime = 28;
            Item.UseSound = SoundID.DD2_DefenseTowerSpawn;
            Item.shoot = ModContent.ProjectileType<DefendersCrystalProjectile>();
            Item.shootSpeed = 4;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
    }
}
