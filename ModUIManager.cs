using Microsoft.Xna.Framework;
using PboneUtils.UI;
using PboneUtils.UI.States.EndlessBuffToggler;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace PboneUtils
{
    public class ModUIManager
    {
        public ItemConfigurerContainer ItemConfigurer;
        public BuffTogglerContainer BuffToggler;

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

            public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers, GameTime _lastUpdateUIGameTime)
            {
                int index = layers.FindIndex((layer) => layer.Name == "Vanilla: Cursor");
                if (index != -1)
                {
                    layers.Insert(index - 1, new LegacyGameInterfaceLayer("PboneUtils: Radial Menu", delegate
                    {
                        if (_lastUpdateUIGameTime != null)
                            RadialMenu.Draw(Main.spriteBatch, _lastUpdateUIGameTime);
                        return true;
                    }, InterfaceScaleType.UI));
                }
            }
        }

        public class BuffTogglerContainer
        {
            public UserInterface TogglerUserInterface;
            public UserInterface TogglerButtonUserInterface;

            public BuffTogglerInventoryButtonUI BuffTogglerInventoryButton;
            public BuffTogglerUI BuffTogglerMenu;

            public BuffTogglerContainer()
            {
                // Init UserInterfaces
                TogglerUserInterface = new UserInterface();
                TogglerButtonUserInterface = new UserInterface();

                // Init UIStates
                BuffTogglerInventoryButton = new BuffTogglerInventoryButtonUI();
                BuffTogglerInventoryButton.Activate();

                BuffTogglerMenu = new BuffTogglerUI();
                BuffTogglerMenu.Activate();

                // TogglerButtonUserInterface should always be set to the BuffTogglerInventoryButton state
                if (PboneUtilsConfig.Instance.EndlessPotions)
                    TogglerButtonUserInterface.SetState(BuffTogglerInventoryButton);
            }

            public void UpdateUI(GameTime gameTime)
            {
                if (TogglerUserInterface?.CurrentState != null)
                    TogglerUserInterface.Update(gameTime);
                if (TogglerButtonUserInterface?.CurrentState != null)
                    TogglerButtonUserInterface?.Update(gameTime);
            }

            public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers, GameTime _lastUpdateUIGameTime)
            {
                int index = layers.FindIndex((layer) => layer.Name == "Vanilla: Inventory");
                if (index != -1)
                {
                    layers.Insert(index - 1, new LegacyGameInterfaceLayer("PboneUtils: Buff Toggler", delegate
                    {
                        if (_lastUpdateUIGameTime != null && TogglerUserInterface?.CurrentState != null)
                            TogglerUserInterface.Draw(Main.spriteBatch, _lastUpdateUIGameTime);
                        return true;
                    }, InterfaceScaleType.UI));
                }

                index = layers.FindIndex((layer) => layer.Name == "Vanilla: Mouse Text");
                if (index != -1)
                {
                    layers.Insert(index, new LegacyGameInterfaceLayer("PboneUtils: Buff Toggler Button", delegate
                    {
                        if (_lastUpdateUIGameTime != null && TogglerButtonUserInterface?.CurrentState != null)
                            TogglerButtonUserInterface.Draw(Main.spriteBatch, _lastUpdateUIGameTime);

                        return true;
                    }, InterfaceScaleType.UI));
                }
            }

            #region Open/Close methods
            public void OpenBuffTogglerMenu()
            {
                if (TogglerUserInterface?.CurrentState != BuffTogglerMenu)
                    TogglerUserInterface?.SetState(BuffTogglerMenu);
            }

            public void CloseBuffTogglerMenu()
            {
                if (TogglerUserInterface?.CurrentState != null)
                    TogglerUserInterface?.SetState(null);
            }

            public void ToggleBuffTogglerMenu()
            {
                if (TogglerUserInterface?.CurrentState != BuffTogglerMenu)
                {
                    Main.PlaySound(SoundID.MenuOpen);
                    OpenBuffTogglerMenu();
                }
                else 
                {
                    Main.PlaySound(SoundID.MenuClose);
                    CloseBuffTogglerMenu();
                }
            }
            #endregion
        }

        public void Initialize()
        {
            if (!Main.dedServ)
            {
                ItemConfigurer = new ItemConfigurerContainer();
                BuffToggler = new BuffTogglerContainer();
            }
        }

        public void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUIGameTime = gameTime;

            if (PboneUtilsConfig.Instance.EndlessPotions)
                BuffToggler.UpdateUI(gameTime);
        }

        public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            ItemConfigurer.ModifyInterfaceLayers(layers, _lastUpdateUIGameTime);

            if (PboneUtilsConfig.Instance.EndlessPotions)
                BuffToggler.ModifyInterfaceLayers(layers, _lastUpdateUIGameTime);
        }
    }
}
