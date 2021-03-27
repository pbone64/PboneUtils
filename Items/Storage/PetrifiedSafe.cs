using Microsoft.Xna.Framework;
using PboneUtils.Projectiles.Storage;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Storage
{
    public class PetrifiedSafe : PItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.SwingThrow;
            UseTime = 28;
            item.UseSound = SoundID.Item37;
            item.shoot = ModContent.ProjectileType<PetrifiedSafeProjectile>();
            item.shootSpeed = 4;
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position += new Vector2(0, 16);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
