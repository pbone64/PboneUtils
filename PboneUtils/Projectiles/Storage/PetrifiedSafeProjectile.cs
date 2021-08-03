using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneLib.ID;
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
        public override Texture2D Outline => PboneUtils.Textures["PetrifiedSafeOutline"];
        public override bool Animate => false;
        public override LegacySoundStyle UseSound => SoundID.Item37;

        public override void SetWhoAmIVariable(PortableStoragePlayer player, int value) => player.SafeGargoyleChest = value;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.Size = new Vector2(24, 48);
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10800;
        }
    }
}
