using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace PboneUtils.UI.States
{
    public class RadialMenuContainer : UIState
    {
        public RadialMenu Internal;

        public override void OnInitialize()
        {
            base.OnInitialize();

            Internal = new RadialMenu();
        }

        public override void OnActivate()
        {
            base.OnActivate();

            Internal = RadialMenu.OpenInfo;
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();

            Internal.Close();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            Internal.Draw(spriteBatch);
        }
    }
}
