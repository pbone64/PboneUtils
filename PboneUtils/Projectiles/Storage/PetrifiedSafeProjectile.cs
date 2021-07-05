using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.ID;
using PboneUtils.Items.Storage;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Projectiles.Storage
{
    public class PetrifiedSafeProjectile : StorageProjectile
    {
        public override int ChestType => BankID.Safe;
        public override int ItemType => ModContent.ItemType<PetrifiedSafe>();
        public override Texture2D Outline => PboneUtils.Textures.Extras.PetrifiedSafeOutline;
        public override bool Animate => false;
        public override LegacySoundStyle UseSound => SoundID.Item37;

        public override void SetWhoAmIVariable(PortableStoragePlayer player, int value) => player.SafeGargoyleChest = value;

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.Size = new Vector2(24, 48);
            projectile.tileCollide = false;
            projectile.timeLeft = 10800;
        }
    }
}
