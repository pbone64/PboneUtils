using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using PboneLib.CustomLoading.Content.Implementations.Content;

namespace PboneUtils.Items
{
    public abstract class PboneUtilsItem : PItem
    {
        public virtual bool Autosize => true;
        public virtual bool ShowItemIconWhenInRange => false;
        public virtual bool DrawGlowmask => false;
        public virtual Color ModifyGlowmaskColor => Color.White;

        public string GlowmaskTexture => Texture + "_Glow";
        public int UseTime { set => Item.useTime = Item.useAnimation = value; }
        public Texture2D ItemTexture => TextureAssets.Item[Item.type].Value;

        public override void SetDefaults()
        {
            base.SetDefaults();

            // Autosize
            /*if (Autosize && TextureAssets.Item[Item.type].Value != null)
            {
                Vector2 texSize = TextureAssets.Item[Item.type].Size();
                Vector2 correctedSize = texSize;

                if (Main.itemAnimations[Item.type] is DrawAnimationVertical animation) // If it has a DrawAnimationVertical registered
                    correctedSize = new Vector2(texSize.X, (texSize.Y / animation.FrameCount) - animation.FrameCount * 2); // Account for the amount of frames and buffer between frames

                Item.Size = correctedSize;
            }*/
            Item.Size = new Vector2(24);
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);

            if (ShowItemIconWhenInRange && player.IsTargetTileInItemRange(Item))
            {
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = Item.type;
            }
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);

            if (DrawGlowmask)
            {
                Texture2D texture = ModContent.Request<Texture2D>(GlowmaskTexture).Value;
                spriteBatch.Draw(texture, position, frame, ModifyGlowmaskColor, 0f, origin, scale, SpriteEffects.None, 0f);
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);

            if (DrawGlowmask)
            {
                Texture2D texture = ModContent.Request<Texture2D>(GlowmaskTexture).Value;
                Vector2 position = new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f);
                spriteBatch.Draw(texture, position, null, ModifyGlowmaskColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
        }
    }
}
