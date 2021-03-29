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

        public int UseTime { set => item.useTime = item.useAnimation = value; }
        public Texture2D ItemTexture => Main.itemTexture[item.type];

        public override void SetDefaults()
        {
            // Autosize
            if (Autosize && PboneUtils.TexturesLoaded)
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
    }
}
