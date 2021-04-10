using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.ID;
using Terraria.Audio;
using Terraria.ID;

namespace PboneUtils.Projectiles.Storage
{
    public class PetrifiedSafeProjectile : StorageProjectile
    {
        public override int ChestType => BankID.Safe;
        public override Texture2D Outline => PboneUtils.Textures.Extras.PetrifiedSafeOutline;
        public override bool Animate => false;
        public override LegacySoundStyle UseSound => SoundID.Item37;

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.Size = new Vector2(24, 48);
            projectile.tileCollide = false;
            projectile.timeLeft = 10800;
        }
    }
}
