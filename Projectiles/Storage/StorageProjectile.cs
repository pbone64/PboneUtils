using Terraria;

namespace PboneUtils.Projectiles.Storage
{
    public abstract class StorageProjectile : PProjectile
    {
        public abstract int ChestType { get; }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

        }

        public override void AI()
        {
            base.AI();
            projectile.frameCounter++;
            float frameTime = 4f;
            if (projectile.frameCounter < frameTime * 1f)
            {
                projectile.frame = 0;
            }
            else if (projectile.frameCounter < frameTime * 2f)
            {
                projectile.frame = 1;
            }
            else if (projectile.frameCounter < frameTime * 3f)
            {
                projectile.frame = 2;
            }
            else if (projectile.frameCounter < frameTime * 4f)
            {
                projectile.frame = 3;
            }
            else if (projectile.frameCounter < frameTime * 5f)
            {
                projectile.frame = 4;
            }
            else if (projectile.frameCounter < frameTime * 6f)
            {
                projectile.frame = 3;
            }
            else if (projectile.frameCounter < frameTime * 7f)
            {
                projectile.frame = 2;
            }
            else if (projectile.frameCounter < frameTime * 8f)
            {
                projectile.frame = 1;
            }
            else
            {
                projectile.frameCounter = 0;
                projectile.frame = 0;
            }

            //Main.CurrentFrameFlags.HadAnActiveInteractibleProjectile = true;
            if (projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (i != projectile.whoAmI
                        && Main.projectile[i].active
                        && Main.projectile[i].owner == projectile.owner
                        && Main.projectile[i].type == projectile.type)
                    {
                        if (projectile.timeLeft >= Main.projectile[i].timeLeft)
                            Main.projectile[i].Kill();
                        else
                            projectile.Kill();
                    }
                }
            }

            if (projectile.ai[0] == 0f)
            {
                if (projectile.velocity.Length() < 0.1)
                {
                    projectile.velocity.X = 0f;
                    projectile.velocity.Y = 0f;
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = 45f;
                    return;
                }

                projectile.velocity *= 0.94f;
                if (projectile.velocity.X < 0f)
                    projectile.direction = -1;
                else
                    projectile.direction = 1;

                projectile.spriteDirection = projectile.direction;
                return;
            }

            if (Main.player[projectile.owner].Center.X < projectile.Center.X)
                projectile.direction = -1;
            else
                projectile.direction = 1;

            projectile.spriteDirection = projectile.direction;
            projectile.ai[1] += 1f;
            float acceleration = 0.005f;
            if (projectile.ai[1] > 0f)
                projectile.velocity.Y -= acceleration;
            else
                projectile.velocity.Y += acceleration;

            if (projectile.ai[1] >= 90f)
                projectile.ai[1] *= -1f;
        }
    }
}
