using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.DataStructures;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;

namespace PboneUtils.UI.RadialMenus
{
    public class RadialMenu : UIState
    {
        public class RadialMenuSettings
        {
            public int NumButtons;
            public string Name;
            public string CenterName;
            public bool OnlyOne;

            public RadialMenuSettings(string name)
            {
                Name = name;
            }

            public RadialMenuSettings(string name, bool onlyOne)
            {
                Name = name;
                OnlyOne = onlyOne;
            }
        }

        public RadialMenuSettings Settings;
        public bool NeedsButtonRebuilding = false;
        public bool Enabled = false;

        public UIRadialButton CenterButton = new UIRadialButton();
        public List<UIRadialButton> RadialButtons;

        public override void OnInitialize()
        {
            base.OnInitialize();

            RadialButtons = new List<UIRadialButton>();
            Settings = new RadialMenuSettings("");
        }

        public void DisableAll()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (NeedsButtonRebuilding)
            {
                Player player = Main.LocalPlayer;
                PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();

                string name = Settings.Name;
                ItemConfig config = mPlayer.ItemConfigs[name];

                Settings.NumButtons = config.ToggleCount;

                RadialButtons.Clear();

                // TODO somehow type is null????
                UIRadialButton cButton = new UIRadialButton(name, $"{name}Center", true, false);
                cButton.Left.Set(Main.MouseScreen.X - 20, 0f);
                cButton.Top.Set(Main.MouseScreen.Y - 20, 0f);
                CenterButton = cButton;

                int i = 1;
                foreach (KeyValuePair<string, bool> kvp in config.Data)
                {
                    UIRadialButton button = new UIRadialButton(name, kvp.Key, false, false);

                    Vector2 pos = Main.MouseScreen - new Vector2(20);
                    pos = pos.RotatedBy((MathHelper.Pi * 2) / Settings.NumButtons * i, Main.MouseScreen);
                    button.Left.Set(pos.X, 0f);
                    button.Top.Set(pos.Y, 0f);

                    i++;
                }

                NeedsButtonRebuilding = false;
            }

            if (Main.mouseRight && Main.mouseRightRelease)
            {
                CenterButton = null;
                RadialButtons.Clear();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (!string.IsNullOrEmpty(Settings.Name) && Enabled)
            {
                CenterButton.Draw(spriteBatch);

                foreach (UIRadialButton button in RadialButtons)
                {
                    button.Draw(spriteBatch);
                }
            }
        }
    }
}
