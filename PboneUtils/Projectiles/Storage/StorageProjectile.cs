using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.Items.Storage;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;

namespace PboneUtils.Projectiles.Storage
{
    public abstract class StorageProjectile : PboneUtilsProjectile
    {
        public abstract int ChestType { get; }
        public abstract int ItemType { get; }
        public abstract Texture2D Outline { get; }

        public abstract void SetWhoAmIVariable(PortableStoragePlayer player, int value);
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
                Projectile.frameCounter++;
                float frameTime = 4f;
                if (Projectile.frameCounter < frameTime * 1f)
                {
                    Projectile.frame = 0;
                }
                else if (Projectile.frameCounter < frameTime * 2f)
                {
                    Projectile.frame = 1;
                }
                else if (Projectile.frameCounter < frameTime * 3f)
                {
                    Projectile.frame = 2;
                }
                else if (Projectile.frameCounter < frameTime * 4f)
                {
                    Projectile.frame = 3;
                }
                else if (Projectile.frameCounter < frameTime * 5f)
                {
                    Projectile.frame = 4;
                }
                else if (Projectile.frameCounter < frameTime * 6f)
                {
                    Projectile.frame = 3;
                }
                else if (Projectile.frameCounter < frameTime * 7f)
                {
                    Projectile.frame = 2;
                }
                else if (Projectile.frameCounter < frameTime * 8f)
                {
                    Projectile.frame = 1;
                }
                else
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame = 0;
                }
            }

            //Main.CurrentFrameFlags.HadAnActiveInteractibleProjectile = true;
            if (Projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (i != Projectile.whoAmI
                        && Main.projectile[i].active
                        && Main.projectile[i].owner == Projectile.owner
                        && Main.projectile[i].type == Projectile.type)
                    {
                        if (Projectile.timeLeft >= Main.projectile[i].timeLeft)
                            Main.projectile[i].Kill();
                        else
                            Projectile.Kill();
                    }
                }
            }

            if (Projectile.ai[0] == 0f)
            {
                if (Projectile.velocity.Length() < 0.1)
                {
                    Projectile.velocity.X = 0f;
                    Projectile.velocity.Y = 0f;
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = 45f;
                    return;
                }

                Projectile.velocity *= 0.94f;
                if (Projectile.velocity.X < 0f)
                    Projectile.direction = -1;
                else
                    Projectile.direction = 1;

                Projectile.spriteDirection = Projectile.direction;
                return;
            }

            if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
                Projectile.direction = -1;
            else
                Projectile.direction = 1;

            Projectile.spriteDirection = Projectile.direction;
            Projectile.ai[1] += 1f;
            float acceleration = 0.005f;
            if (Projectile.ai[1] > 0f)
                Projectile.velocity.Y -= acceleration;
            else
                Projectile.velocity.Y += acceleration;

            if (Projectile.ai[1] >= 90f)
                Projectile.ai[1] *= -1f;

            TryInteractingWithStorageProj(Projectile);
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
            localPlayer.cursorItemIconEnabled = true;
            localPlayer.cursorItemIconID = ItemType;
            if (PlayerInput.UsingGamepad)
                localPlayer.GamepadEnableGrappleCooldown();

            if (Main.mouseRight && Main.mouseRightRelease/* && Player.StopMoneyTroughFromWorking == 0*/)
            {
                Main.mouseRightRelease = false;
                localPlayer.tileInteractAttempted = true;
                localPlayer.tileInteractionHappened = true;
                localPlayer.releaseUseTile = false;
                if (localPlayer.chest == ChestType)
                {
                    SoundEngine.PlaySound(UseSound);
                    localPlayer.chest = -1;
                    SetWhoAmIVariable(localPlayer.GetModPlayer<PortableStoragePlayer>(), -1);
                    Recipe.FindRecipes();
                    return;
                }

                SetWhoAmIVariable(localPlayer.GetModPlayer<PortableStoragePlayer>(), Projectile.whoAmI);

                localPlayer.chest = ChestType;
                localPlayer.chestX = (int)(proj.Center.X / 16f);
                localPlayer.chestY = (int)(proj.Center.Y / 16f);
                localPlayer.SetTalkNPC(-1);
                Main.SetNPCShopIndex(0);
                Main.playerInventory = true;
                SoundEngine.PlaySound(UseSound);
                Recipe.FindRecipes();
            }
        }

        public override void PostDraw(Color lightColor)
        {
            base.PostDraw(lightColor);

            // Uhh is outline gamepad only? Gamepad players don't exist anyways
            //Main.spriteBatch.Draw(Outline, Projectile.position - Main.screenPosition - new Vector2(2f), null, lightColor, Projectile.rotation, Vector2.Zero, Projectile.scale, SpriteEffects.None, 0f);
        }
    }
}
