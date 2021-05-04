using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.ID;
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
        public override Texture2D Outline => PboneUtils.Textures.Extras.PetrifiedSafeOutline;
        public override bool Animate => false;
        public override LegacySoundStyle UseSound => SoundID.DD2_BookStaffCast;

        public override Ref<int> GetWhoAmIVariable(PbonePlayer player) => player.DefendersCrystalChest;
        public override Ref<bool> GetToggleVariable(PbonePlayer player) => player.DefendersCrystalOpen;

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.Size = new Vector2(22, 38);
            projectile.tileCollide = false;
            projectile.timeLeft = 10800;
        }

        public override void AI()
        {
            base.AI();
            float timer = (float)((Math.Sin(Main.GameUpdateCount / 20f) + 1f) / 2f);
            Lighting.AddLight(projectile.Center, (Color.CornflowerBlue * timer).ToVector3());
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            
            float timer = (float)((Math.Sin(Main.GameUpdateCount / 20f) + 1f) / 2f);
            Texture2D texture = PboneUtils.Textures.Extras.DefendersCrystalGlowyThing;
            Color color = Color.White * timer;

            for (int i = 0; i < 4; i++)
            {
                Vector2 position = projectile.Center - Main.screenPosition + new Vector2(0f, 2f).RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position, null, color, projectile.rotation, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            return base.PreDraw(spriteBatch, lightColor);
        }
    }
}
