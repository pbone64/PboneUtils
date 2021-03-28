using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace PboneUtils.UI.RadialMenus
{
    public class UIRadialButton : UIElement
    {
        public Texture2D IconTexture => PboneUtils.Textures.UI.RadialMenuIcons[Type];
        public bool Hovered => IsCenterButton ? false : GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint());

        public string Name;
        public string Type;
        public bool IsCenterButton;
        public bool RedMode;
        public bool Enabled;

        public UIRadialButton() { }

        public UIRadialButton(string name, string type, bool isCenterButton, bool redMode = false)
        {
            Name = name;
            Type = type;
            IsCenterButton = isCenterButton;
            RedMode = redMode;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (Hovered && Main.mouseLeft && Main.mouseLeftRelease)
            {
                // Click logic
                if (Enabled == false && PboneUtils.UI.RadialMenus.RadialMenu.Settings.OnlyOne)
                    PboneUtils.UI.RadialMenus.RadialMenu.DisableAll();
                Enabled = !Enabled;
            }

            Texture2D buttonTexture = PboneUtils.Textures.UI.GetRadialButton(Hovered, RedMode);
            Texture2D iconTexture = IconTexture;
            // Draw button
            spriteBatch.Draw(buttonTexture, GetDimensions().Position(), null, Color.White, 0f, Vector2.Zero, Main.UIScale, SpriteEffects.None, 0f);
            // Draw icon
            spriteBatch.Draw(iconTexture, GetDimensions().Position(), null, Color.White, 0f, Vector2.Zero, Main.UIScale, SpriteEffects.None, 0f);
        }
    }
}
