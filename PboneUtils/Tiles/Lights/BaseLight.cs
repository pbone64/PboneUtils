using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.MiscModsPlayers;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PboneUtils.Tiles.Lights
{
    public abstract class BaseLight : PboneUtilsTile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.ShadowBeamFriendly; // Aka invisible

        public abstract Color LightColor { get; }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newTile.AnchorWall = true;
            TileObjectData.addTile(Type);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            base.ModifyLight(i, j, ref r, ref g, ref b);
            Vector3 light = LightColor.ToVector3();
            r = light.X;
            g = light.Y;
            b = light.Z;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            base.PostDraw(i, j, spriteBatch);

            if (Main.LocalPlayer.GetModPlayer<PbonePlayer>().MagicLight)
            {
                int x = i * 16;
                x -= (int)Main.screenPosition.X;
                int y = j * 16;
                y -= (int)Main.screenPosition.Y;

                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen)
                    zero = Vector2.Zero;

                int width = 16;
                int offsetY = 0;
                int height = 16;
                TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height);

                Rectangle dest = new Rectangle(x + (int)zero.X - (width - 16), y + (int)zero.Y - (height - 16) + offsetY, 16, 16);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, dest, null, LightColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }
    }
}
