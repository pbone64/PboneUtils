using Microsoft.Xna.Framework;
using PboneUtils.UI;
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
            public RadialMenu RadialMenu = new RadialMenu();
        }

        public void Initialize()
        {
            if (!Main.dedServ)
            {
                ItemConfigurer = new ItemConfigurerContainer();
            }
        }

        public void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUIGameTime = gameTime;
        }

        public void OpenRadialMenu(string name)
        {
            ItemConfigurer.RadialMenu.Open(name);
        }

        public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex((layer) => layer.Name == "Vanilla: Cursor");
            if (index != -1)
            {
                layers.Insert(index - 1, new LegacyGameInterfaceLayer("PboneUtils: Radial Menu", delegate
                {
                    if (_lastUpdateUIGameTime != null)
                        ItemConfigurer.RadialMenu.Draw(Main.spriteBatch, _lastUpdateUIGameTime);
                    return true;
                }, InterfaceScaleType.UI));
            }
        }
    }
}
