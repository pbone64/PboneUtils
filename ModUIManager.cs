using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;

namespace PboneUtils
{
    public class ModUIManager
    {
        public ItemConfigurerContainer ItemConfigurer;

        private GameTime _lastUpdateUIGameTime;

        public class ItemConfigurerContainer
        {
            public UserInterface RadialMenuInterface = new UserInterface();
            //public RadialMenu RadialMenu = new RadialMenu();
        }

        public void Initialize()
        {
            if (!Main.dedServ)
            {
                ItemConfigurer = new ItemConfigurerContainer();
                //RadialMenus.RadialMenu.Activate();
                //RadialMenus.RadialMenuInterface.SetState(RadialMenus.RadialMenu);
            }
        }

        public void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUIGameTime = gameTime;

            if (ItemConfigurer.RadialMenuInterface?.CurrentState != null)
                ItemConfigurer.RadialMenuInterface.Update(gameTime);
        }

        /*public void OpenRadialMenu(string name, bool onlyOne)
        {
            Player player = Main.LocalPlayer;
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();
            if (mPlayer.ItemConfigs.ContainsKey(name))
            {
                RadialMenus.RadialMenu.Settings = new RadialMenu.RadialMenuSettings(name, onlyOne);
                RadialMenus.RadialMenu.NeedsButtonRebuilding = true;
                RadialMenus.RadialMenu.Enabled = true;
            }
        }*/

        public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            /*int index = layers.FindIndex((layer) => layer.Name == "Vanilla: Cursor");
            if (index != -1)
            {
                layers.Insert(index - 1, new LegacyGameInterfaceLayer("PboneUtils: Radial Menu", delegate
                {
                    if (_lastUpdateUIGameTime != null && RadialMenus.RadialMenuInterface?.CurrentState != null)
                        RadialMenus.RadialMenuInterface.Draw(Main.spriteBatch, _lastUpdateUIGameTime);
                    return true;
                }, InterfaceScaleType.UI));
            }*/
        }
    }
}
