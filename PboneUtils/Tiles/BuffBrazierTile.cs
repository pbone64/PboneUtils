using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.Buffs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PboneUtils.Tiles
{
    public class BuffBrazierTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.tileFrameImportant[Type] = true;
			Main.tileLighted[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.addTile(Type);

            AddMapEntry(Color.Cyan);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            base.NearbyEffects(i, j, closer);
            // Intentionally not checking for closer to make it stronger :P
			if (Main.LocalPlayer.active && !Main.LocalPlayer.dead)
				Main.LocalPlayer.AddBuff(ModContent.BuffType<BuffBrazierBuff>(), 60); // Needs to be 60 otherwise it starts pulsating
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            base.ModifyLight(i, j, ref r, ref g, ref b);
			Vector3 color = Color.White.ToVector3();
			r = color.X;
			g = color.Y;
			b = color.Z;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}

			Tile tile = Main.tile[i, j];
			int width = 16;
			int offsetY = 0;
			int height = 16;
			TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height);
			Texture2D flameTexture = PboneUtils.Textures["BuffBrazierFlame"];

			ulong num190 = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (uint)i);

			for (int c = 0; c < 7; c++)
			{
				float shakeX = Utils.RandomInt(ref num190, -10, 11) * 0.15f;
				float shakeY = Utils.RandomInt(ref num190, -10, 1) * 0.35f;
				Vector2 position = new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + shakeX, j * 16 - (int)Main.screenPosition.Y + offsetY + shakeY) + zero;

				spriteBatch.Draw(flameTexture, position, new Rectangle(tile.frameX, tile.frameY, width, height), Color.White * 0.33f, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
		}
	}
}
