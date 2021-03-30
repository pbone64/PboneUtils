using Microsoft.Xna.Framework;
using PboneUtils.UI;
using System.Collections.Generic;
using System.Linq;
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

            public void Open(string name, int item)
            {
                RadialMenu.Open(name, item);
            }

            public void Close()
            {
                RadialMenu.Close();
            }

            public void Toggle(string name, int item)
            {
                if (!RadialMenu.Active)
                    RadialMenu.Open(name, item);
                else
                    RadialMenu.Close();
            }

            public bool IsHovered()
            {
                (bool centerHovered, bool[] buttonsHovered) hoveredButtons = RadialMenu.GetHoveredButtons();
                return (hoveredButtons.centerHovered) || (hoveredButtons.buttonsHovered.Count(b => b == true) > 0);
            }

            public bool IsOpen() => RadialMenu.Active;
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
