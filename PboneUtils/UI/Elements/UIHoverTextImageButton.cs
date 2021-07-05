using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace PboneUtils.UI.Elements
{
    public class UIHoverTextImageButton : UIImageButton
    {
        public string Text;

        public UIHoverTextImageButton(Texture2D texture, string text) : base(texture)
        {
            Text = text;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                Main.hoverItemName = Text;
            }
        }
    }
}
