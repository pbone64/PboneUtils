using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PboneUtils.Projectiles.Selection
{
    public abstract class SelectionProjectile : PProjectile
    {
        public virtual Action<int, int> TileAction { get; }
        public virtual Func<Rectangle, bool> PreAction { get; }

        public override string Texture => "Terraria/Projectile_" + ProjectileID.ShadowBeamFriendly; // Aka invisible

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }

        // AI Properties
        public bool Initialized { get => projectile.localAI[0] == 1; set => projectile.localAI[0] = value ? 1 : 0; }
        public float StartX { get => projectile.ai[0]; set => projectile.ai[0] = value; }
        public float StartY { get => projectile.ai[1]; set => projectile.ai[1] = value; }

        public Point StartPosition => new Vector2(projectile.ai[0], projectile.ai[1]).ToPoint();
        public Point CurrentPosition => projectile.Center.ToTileCoordinates();

        public override void AI()
        {
            base.AI();

            if (Owner.whoAmI == projectile.owner) // Only run on the owners client
            {
                // TODO ui oddities: placing liquids when pressing ui buttons, ui spam opening (i hate you terraria)
                // Kill if owner can't use items, is CCed (frozen, webbed, stoned), dead
                if (Owner.noItems || Owner.CCed || Owner.dead)
                {
                    projectile.Kill();
                }

                // If they aren't channeling, then do stuff before killing yourself
                if (!Owner.channel)
                {
                    Rectangle rect = GetRectangle();
                    if (PreAction(rect))
                    {
                        for (int i = 0; i < rect.Width / 16; i++)
                        {
                            for (int j = 0; j < rect.Height / 16; j++)
                            {
                                TileAction(rect.X / 16 + i, rect.Y / 16 + j);
                            }
                        }
                    }

                    projectile.Kill();
                }

                projectile.timeLeft = 2;
                projectile.localAI[1]++; // Timer for draw fading

                Vector2 mouseWorld = Main.MouseWorld;
                if (Owner.gravDir == -1f)
                    mouseWorld.Y = (Main.screenHeight - Main.mouseY) + Main.screenPosition.Y;

                // Move to MouseWorld
                if (projectile.Center != mouseWorld)
                {
                    projectile.netUpdate = true;
                    projectile.Center = mouseWorld;
                    projectile.netUpdate = true;
                    projectile.velocity = Vector2.Zero;
                }

                // Only run once
                if (!Initialized)
                {
                    // Save the tile coords of the start position to ai0 and ai1
                    StartX = (int)projectile.Center.X / 16;
                    StartY = (int)projectile.Center.Y / 16;

                    Initialized = true;
                }

                projectile.velocity = Vector2.Zero;

                int ownerDir = Math.Sign(Owner.velocity.X);
                if (ownerDir != 0)
                    Owner.ChangeDir(ownerDir);

                Owner.heldProj = projectile.whoAmI;
                Owner.itemRotation = 0f;
                Owner.itemAnimation = 2;
                Owner.itemTime = 2;
                Owner.itemAnimationMax = 3;
            }
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsOverWiresUI.Add(projectile.whoAmI);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            base.PostDraw(spriteBatch, lightColor);

            if (Owner.whoAmI == Main.myPlayer && !PboneUtils.UI.ItemConfigurer.IsHovered()) // Only draw on owner's client
            {
                float fade = MathHelper.Lerp(0.1f, 0.3f, (float)(Math.Sin(Main.GameUpdateCount / 10f) + 1f) / 2f);
                Color color = Color.Lerp(Color.Transparent, Color.White, fade);

                Rectangle destination = GetRectangle();
                destination.X -= (int)Main.screenPosition.X;
                destination.Y -= (int)Main.screenPosition.Y;

                spriteBatch.Draw(Main.magicPixel, destination, null, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        public Rectangle GetRectangle()
        {
            Rectangle rect = new Rectangle(
                (int)(StartX * 16f),
                (int)(StartY * 16f),
                (int)((CurrentPosition.X - (int)StartX) * 16f),
                (int)((CurrentPosition.Y - (int)StartY) * 16f)
            );

            if (rect.Width >= 0 && rect.Width < 16)
            {
                rect.Width = 16;
            }
            if (rect.Height >= 0 && rect.Height < 16)
            {
                rect.Height = 16;
            }

            // Don't be negative, please
            if (rect.Width < 0)
            {
                rect.Width *= -1;
                if (rect.Width < 16)
                    rect.Width = 16;

                rect.X -= rect.Width;
            }
            if (rect.Height < 0)
            {
                rect.Height *= -1;
                if (rect.Height < 16)
                    rect.Height = 16;

                rect.Y -= rect.Height;
            }

            return rect;
        }
    }
}
