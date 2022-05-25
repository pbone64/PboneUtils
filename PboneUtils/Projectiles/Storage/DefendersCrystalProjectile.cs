using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneLib.ID;
using PboneUtils.Items.Storage;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Projectiles.Storage
{
    public class DefendersCrystalProjectile : StorageProjectile
    {
        public override int ChestType => BankID.DefendersForge;
        public override int ItemType => ModContent.ItemType<DefendersCrystal>();
        public override Texture2D Outline => PboneUtils.Textures["DefendersCrystalOutline"];
        public override bool Animate => false;
        public override SoundStyle UseSound => SoundID.DD2_BookStaffCast;

        public override void SetWhoAmIVariable(PortableStoragePlayer player, int value) => player.DefendersCrystalChest = value;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.Size = new Vector2(22, 38);
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10800;
        }

        public override void AI()
        {
            base.AI();
            float timer = (float)((Math.Sin(Main.GameUpdateCount / 20f) + 1f) / 2f);
            Lighting.AddLight(Projectile.Center, (Color.CornflowerBlue * timer).ToVector3());
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            
            float timer = (float)((Math.Sin(Main.GameUpdateCount / 20f) + 1f) / 2f);
            Texture2D texture = PboneUtils.Textures["DefendersCrystalGlowyThing"];
            Color color = Color.White * timer;

            for (int i = 0; i < 4; i++)
            {
                Vector2 position = Projectile.Center - Main.screenPosition + new Vector2(0f, 2f).RotatedBy(MathHelper.PiOver2 * i) * 2;
                Main.spriteBatch.Draw(texture, position, null, color, Projectile.rotation, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            return base.PreDraw(ref lightColor);
        }
    }
}
