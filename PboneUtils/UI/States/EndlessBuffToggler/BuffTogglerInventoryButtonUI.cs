using Microsoft.Xna.Framework.Graphics;
using PboneUtils.UI.Elements;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace PboneUtils.UI.States.EndlessBuffToggler
{
    public class BuffTogglerInventoryButtonUI : UIState
    {
        public UIImage Icon;
        public UIHoverTextImageButton IconHighlight;

        public override void OnInitialize()
        {
            base.OnInitialize();

            int top = 262;
            /*if (PboneUtils.CrossMod.IsModLoaded("Fargowiltas"))
            {
                top += 28;
            }

            if (PboneUtils.CrossMod.IsModLoaded("FargowiltasSouls"))
            {
                top += 58;
            }*/

            Icon = new UIImage(PboneUtils.Textures["BuffTogglerInventoryButton"]);
            Icon.Left.Set(26, 0f);
            Icon.Top.Set(top, 0f);
            Append(Icon);

            IconHighlight = new UIHoverTextImageButton(PboneUtils.Textures.GetAsset("BuffTogglerInventoryButton_MouseOver"), "dummy");
            IconHighlight.Left.Set(-2, 0f);
            IconHighlight.Top.Set(-2, 0f);
            IconHighlight.SetVisibility(1f, 0f);
            IconHighlight.OnLeftClick += IconHighlight_OnClick;
            Icon.Append(IconHighlight);

            base.OnActivate();
        }

        private void IconHighlight_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (CanShow())
            {
                PboneUtils.UI.ToggleUI<BuffTogglerUI>();
                PboneUtils.UI.GetUIState<BuffTogglerUI>().RebuildGrid();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            IconHighlight.Text = Language.GetTextValue("Mods.PboneUtils.UI.EndlessBuffTogglerInventoryButton.MouseOver");

            if (CanShow())
                base.Draw(spriteBatch);
        }

        public bool CanShow()
        {
            if (!Main.playerInventory || Main.LocalPlayer.talkNPC != -1)
                return false;

            return true;
        }
    }
}
