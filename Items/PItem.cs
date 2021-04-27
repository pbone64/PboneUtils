using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.Helpers;

namespace PboneUtils.Items
{
    public abstract class PItem : ModItem
    {
        public virtual bool Autosize => true;
        public virtual bool ShowItemIconWhenInRange => false;
        public virtual bool DrawGlowmask => false;
        public virtual bool AutoloadCondition => true;

        public string GlowmaskTexture => Texture + "_Glow";
        public int UseTime { set => item.useTime = item.useAnimation = value; }
        public Texture2D ItemTexture => Main.itemTexture[item.type];

        public override bool Autoload(ref string name) => AutoloadCondition;

        public override void SetDefaults()
        {
            // Autosize
            if (Autosize && Main.itemTexture[item.type] != null)
            {
                Vector2 texSize = Main.itemTexture[item.type].Size();
                Vector2 correctedSize = texSize;
                DrawAnimationVertical animation = Main.itemAnimations[item.type] as DrawAnimationVertical;
                if (animation != null) // If it has a DrawAnimationVertical registered
                    correctedSize = new Vector2(texSize.X, (texSize.Y / animation.FrameCount) - animation.FrameCount * 2); // Account for the amount of frames and buffer between frames

                item.Size = correctedSize;
            }
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);

            if (ShowItemIconWhenInRange && player.IsTargetTileInItemRange(item))
            {
                player.showItemIcon = true;
                player.showItemIcon2 = item.type;
            }
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);

            if (DrawGlowmask)
            {
                Texture2D texture = ModContent.GetTexture(GlowmaskTexture);
                spriteBatch.Draw(texture, position, frame, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);

            if (DrawGlowmask)
            {
                Texture2D texture = ModContent.GetTexture(GlowmaskTexture);
                Vector2 position = new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f);
                spriteBatch.Draw(texture, position, null, Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
        }
    }
}
