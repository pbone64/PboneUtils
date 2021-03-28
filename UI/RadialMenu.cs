using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.DataStructures;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;

namespace PboneUtils.UI
{
    public class RadialMenu
    {
        public static Color ButtonOff = new Color(100, 100, 100);
        public static Color ButtonOn = new Color(200, 200, 200);
        public static Color IconOff = new Color(80, 80, 80);
        public static Color IconOn = new Color(120, 120, 120);

        public Vector2 ButtonSize => new Vector2(40);

        public string Name;
        public bool Active;
        public int ButtonAmount;
        public Vector2 Position;

        public void Draw(SpriteBatch spriteBatch, GameTime lastUpdateUIGameTime)
        {
            if (!Active)
                return;

            Player player = Main.LocalPlayer;
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();

            (Vector2 centerPosition, Vector2[] buttonsPositions) positions = GetPositions();
            (bool centerHovered, bool[] buttonsHovered) hoveredButtons = GetHoveredButtons();
            ItemConfig config = mPlayer.ItemConfigs[Name];

            Vector2 position = positions.centerPosition - ButtonSize * 0.5f;
            bool hovered = hoveredButtons.centerHovered;
            Texture2D buttonTexture = PboneUtils.Textures.UI.GetRadialButton(hovered, config.RedMode);
            Texture2D iconTexture = PboneUtils.Textures.UI.RadialMenuIcons[Name + (config.RedMode ? "Red" : "")];
            Color buttonColor = Color.White;
            Color iconColor = Color.White;

            spriteBatch.Draw(buttonTexture, position, null, buttonColor, 0f, Vector2.Zero, Main.UIScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(iconTexture, position, null, iconColor, 0f, Vector2.Zero, Main.UIScale, SpriteEffects.None, 0f);

            string[] configNames = config.Data.Keys.ToArray();
            string configName = "";

            for (int i = 0; i < ButtonAmount; i++)
            {
                configName = configNames[i];

                position = positions.buttonsPositions[i] - ButtonSize * 0.5f;
                hovered = hoveredButtons.buttonsHovered[i];
                buttonTexture = PboneUtils.Textures.UI.GetRadialButton(hovered, config.RedMode);
                iconTexture = PboneUtils.Textures.UI.RadialMenuIcons[configName];
                buttonColor = config.Data[configName] ? Color.White : (hovered ? ButtonOn : ButtonOff);
                iconColor = config.Data[configName] ? Color.White : (hovered ? IconOn : IconOff);

                spriteBatch.Draw(buttonTexture, position, null, buttonColor, 0f, Vector2.Zero, Main.UIScale, SpriteEffects.None, 0f);
                spriteBatch.Draw(iconTexture, position, null, iconColor, 0f, Vector2.Zero, Main.UIScale, SpriteEffects.None, 0f);
            }

            UILogic();
        }

        private void UILogic()
        {
            Player player = Main.LocalPlayer;
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();

            if (!ShouldStayOpen(player, true))
            {
                Active = false;
                return;
            }

            (bool centerHovered, bool[] buttonsHovered) hoveredButtons = GetHoveredButtons();

            if (hoveredButtons.centerHovered)
            {
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    ItemConfig config = mPlayer.ItemConfigs[Name];
                    config.RedMode = !config.RedMode;
                }
            }

            for (int i = 0; i < ButtonAmount; i++)
            {
                if (hoveredButtons.buttonsHovered[i])
                {
                    ItemConfig config = mPlayer.ItemConfigs[Name];
                    string[] keys = config.Data.Keys.ToArray();

                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        string key = keys[i];
                        config.Data[key] = !config.Data[key];
                    }
                }
            }
        }

        public void Open(string name)
        {
            Player player = Main.LocalPlayer;

            if (!ShouldStayOpen(player, false))
                return;

            Name = name;
            Active = true;
            ButtonAmount = player.GetModPlayer<PbonePlayer>().ItemConfigs[name].Data.Count;
            Position = Main.MouseScreen;
        }

        public (Vector2 centerPosition, Vector2[] buttonPositions) GetPositions()
        {
            (Vector2 centerPosition, Vector2[] buttonsPositions) ret = (Vector2.Zero, new Vector2[ButtonAmount]);
            ret.centerPosition = Position;

            for (int i = 0; i < ButtonAmount; i++)
            {
                float angle = (MathHelper.TwoPi / ButtonAmount) * i;
                ret.buttonsPositions[i].X = (float)Math.Cos(angle) * 42 + ret.centerPosition.X;
                ret.buttonsPositions[i].Y = (float)Math.Sin(angle) * 42 + ret.centerPosition.Y;
            }

            return ret;
        }

        public (bool centerHovered, bool[] buttonHovered) GetHoveredButtons()
        {
            (bool centerHovered, bool[] buttonHovered) ret = (false, new bool[ButtonAmount]);
            (Vector2 centerPositions, Vector2[] buttonPositions) positions = GetPositions();

            Vector2 mouse = Main.MouseScreen;
            ret.centerHovered = Vector2.Distance(mouse, positions.centerPositions) < (ButtonSize.X / 2f * Main.UIScale);

            for (int i = 0; i < ButtonAmount; i++)
            {
                ret.buttonHovered[i] = Vector2.Distance(mouse, positions.buttonPositions[i]) < (ButtonSize.X / 2f * Main.UIScale);
            }

            return ret;
        }

        public static bool ShouldStayOpen(Player player, bool checkForRightClick)
        {
            if ((player.mouseInterface || player.lastMouseInterface) ||
                (player.dead || Main.mouseItem.type > ItemID.None) ||
                (checkForRightClick && Main.mouseRight && Main.mouseRightRelease))
                return false;

            return true;
        }
    }
}
