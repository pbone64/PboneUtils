using Microsoft.Xna.Framework;
using Terraria;

namespace PboneUtils.Projectiles.Terraforming
{
    public abstract class HoldoutBase : PboneUtilsProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.timeLeft = 2;

            // Thanks, ExampleLastPrismHoldout
            Vector2 rotatedRelativePoint = Owner.RotatedRelativePointOld(Owner.MountedCenter, true);
            Projectile.velocity = Main.MouseWorld - rotatedRelativePoint;

            Projectile.Center = rotatedRelativePoint;

            if (Projectile.direction == 1)
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            else
                Projectile.rotation = Projectile.velocity.ToRotation() + (MathHelper.Pi - MathHelper.PiOver4);

            Projectile.spriteDirection = Projectile.direction;

            Owner.ChangeDir(Projectile.direction);
            Owner.heldProj = Projectile.whoAmI;
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;

            Owner.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();

            if (!Owner.channel || Owner.noItems || Owner.CCed)
                Projectile.Kill();
        }
    }
}
