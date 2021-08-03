using Terraria;
using Microsoft.Xna.Framework;
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
            item.useStyle = ItemUseStyleID.SwingThrow;
            UseTime = 28;
            item.UseSound = SoundID.DD2_DefenseTowerSpawn;
            item.shoot = ModContent.ProjectileType<DefendersCrystalProjectile>();
            item.shootSpeed = 4;
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //position -= new Vector2(0, 12);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
