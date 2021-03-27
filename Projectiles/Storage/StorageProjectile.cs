using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.ID;
using PboneUtils.Items.Storage;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Projectiles.Storage
{
    public abstract class StorageProjectile : PProjectile
    {
        public abstract int ChestType { get; }
        public abstract Texture2D Outline { get; }
        public virtual bool Animate => true;
        public abstract LegacySoundStyle UseSound { get; }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

        }

        public override void AI()
        {
            base.AI();
            if (Animate)
            {
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

            TryInteractingWithStorageProj(projectile);
        }

        public void TryInteractingWithStorageProj(Projectile proj)
        {
            if (Main.gamePaused && !Main.gameMenu)
                return;

            Vector2 vector = proj.position - Main.screenPosition;
            if (!(Main.mouseX > vector.X) || !(Main.mouseX < vector.X + proj.width) || !(Main.mouseY > vector.Y) || !(Main.mouseY < vector.Y + proj.height))
                return;

            Player localPlayer = Main.LocalPlayer;
            int num = (int)(localPlayer.Center.X / 16f);
            int num2 = (int)(localPlayer.Center.Y / 16f);
            int num3 = (int)proj.Center.X / 16;
            int num4 = (int)proj.Center.Y / 16;
            int lastTileRangeX = localPlayer.lastTileRangeX;
            int lastTileRangeY = localPlayer.lastTileRangeY;
            if (num < num3 - lastTileRangeX || num > num3 + lastTileRangeX + 1 || num2 < num4 - lastTileRangeY || num2 > num4 + lastTileRangeY + 1)
                return;

            localPlayer.noThrow = 2;
            localPlayer.showItemIcon = true;
            localPlayer.showItemIcon2 = ModContent.ItemType<PetrifiedSafe>();
            if (PlayerInput.UsingGamepad)
                localPlayer.GamepadEnableGrappleCooldown();

            if (Main.mouseRight && Main.mouseRightRelease && Player.StopMoneyTroughFromWorking == 0)
            {
                Main.mouseRightRelease = false;
                localPlayer.tileInteractAttempted = true;
                localPlayer.tileInteractionHappened = true;
                localPlayer.releaseUseTile = false;
                if (localPlayer.chest == ChestType)
                {
                    Main.PlaySound(UseSound);
                    localPlayer.chest = -1;
                    localPlayer.GetModPlayer<PbonePlayer>().SafeGargoyleChest = -1;
                    Recipe.FindRecipes();
                    return;
                }

                localPlayer.GetModPlayer<PbonePlayer>().SafeGargoyleChest = proj.whoAmI;
                localPlayer.chest = ChestType;
                localPlayer.chestX = (int)(proj.Center.X / 16f);
                localPlayer.chestY = (int)(proj.Center.Y / 16f);
                localPlayer.talkNPC = -1;
                Main.npcShop = 0;
                Main.playerInventory = true;
                Main.PlaySound(UseSound);
                Recipe.FindRecipes();
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            base.PostDraw(spriteBatch, lightColor);

            // Uhh is outline gamepad only? Gamepad players don't exist anyways
            //spriteBatch.Draw(Outline, projectile.position - Main.screenPosition - new Vector2(2f), null, lightColor, projectile.rotation, Vector2.Zero, projectile.scale, SpriteEffects.None, 0f);
        }
    }
}
