using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.DataStructures;
using PboneUtils.MiscModsPlayers;
using ReLogic.Content;
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
        public int ItemType;
        public bool CheckRC = true;

        public static RadialMenu OpenInfo;

        public static bool SetInfo(string name, int item)
        {
            OpenInfo = new RadialMenu();

            Player player = Main.LocalPlayer;
            OpenInfo.ItemType = item;

            if (!OpenInfo.ShouldStayOpen(player, false))
                return false;

            OpenInfo.CheckRC = false;
            OpenInfo.Name = name;
            OpenInfo.Active = true;
            OpenInfo.ButtonAmount = player.GetModPlayer<PbonePlayer>().ItemConfigs[name].Data.Count;
            OpenInfo.Position = Main.MouseScreen;

            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Active)
                return;

            Player player = Main.LocalPlayer;
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();

            (Vector2 centerPosition, Vector2[] buttonsPositions) = GetPositions();
            (bool centerHovered, bool[] buttonsHovered) = GetHoveredButtons();
            ItemConfig config = mPlayer.ItemConfigs[Name];

            Vector2 position = centerPosition - ButtonSize * 0.5f;
            bool hovered = centerHovered;
            Texture2D buttonTexture = PboneUtils.Textures.GetRadialButton(hovered, config.RedMode).Value;
            Texture2D iconTexture = (PboneUtils.Textures.CachedAssets[Name + (config.RedMode ? "Red" : "")] as Asset<Texture2D>).Value;
            Color buttonColor = Color.White;
            Color iconColor = Color.White;

            spriteBatch.Draw(buttonTexture, position, null, buttonColor, 0f, Vector2.Zero, Main.UIScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(iconTexture, position, null, iconColor, 0f, Vector2.Zero, Main.UIScale, SpriteEffects.None, 0f);

            string[] configNames = config.Data.Keys.ToArray();
            string configName;

            for (int i = 0; i < ButtonAmount; i++)
            {
                configName = configNames[i];

                position = buttonsPositions[i] - ButtonSize * 0.5f;
                hovered = buttonsHovered[i];
                buttonTexture = PboneUtils.Textures.GetRadialButton(hovered, config.RedMode).Value;
                iconTexture = PboneUtils.Textures[configName];
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

            if (!ShouldStayOpen(player, CheckRC))
            {
                Close();
                return;
            }
            CheckRC = true;

            (bool centerHovered, bool[] buttonsHovered) = GetHoveredButtons();

            if (centerHovered)
            {
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    ItemConfig config = mPlayer.ItemConfigs[Name];

                    if (config.HasRedMode)
                        config.RedMode = !config.RedMode;
                }
            }

            for (int i = 0; i < ButtonAmount; i++)
            {
                if (buttonsHovered[i])
                {
                    ItemConfig config = mPlayer.ItemConfigs[Name];
                    string[] keys = config.Data.Keys.ToArray();

                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        if (config.OnlyOne)
                        {
                            config.AllOff();
                        }

                        string key = keys[i];
                        config.Data[key] = !config.Data[key];
                        break;
                    }
                }
            }
        }

        public void Close()
        {
            Name = "";
            Active = false;
            ButtonAmount = 0;
            Position = Vector2.Zero;
            ItemType = ItemID.None;
        }

        public (Vector2 centerPosition, Vector2[] buttonPositions) GetPositions()
        {
            (Vector2 centerPosition, Vector2[] buttonsPositions) ret = (Vector2.Zero, new Vector2[ButtonAmount]);
            ret.centerPosition = Position;

            for (int i = 1; i < ButtonAmount + 1; i++)
            {
                float angle = (MathHelper.TwoPi / ButtonAmount) * (i + 0.5f);
                ret.buttonsPositions[i - 1] = new Vector2(ret.centerPosition.X, ret.centerPosition.Y + 45).RotatedBy(angle, ret.centerPosition);
                //ret.buttonsPositions[i - 1] = ret.centerPosition + Vector2.UnitX.RotatedBy(i * ((float)Math.PI * 2f) / ButtonAmount - (float)Math.PI / 11f) * 45f;
                //ret.buttonsPositions[i - 1].X = (float)Math.Cos(angle) * 42 + ret.centerPosition.X;
                //ret.buttonsPositions[i - 1].Y = (float)Math.Sin(angle) * 42 + ret.centerPosition.Y;
            }

            return ret;
        }

        public (bool centerHovered, bool[] buttonHovered) GetHoveredButtons()
        {
            (bool centerHovered, bool[] buttonHovered) ret = (false, new bool[ButtonAmount]);
            (Vector2 centerPositions, Vector2[] buttonPositions) = GetPositions();

            Vector2 mouse = Main.MouseScreen;
            ret.centerHovered = Vector2.Distance(mouse, centerPositions) < (ButtonSize.X / 2f * Main.UIScale);

            for (int i = 0; i < ButtonAmount; i++)
            {
                ret.buttonHovered[i] = Vector2.Distance(mouse, buttonPositions[i]) < (ButtonSize.X / 2f * Main.UIScale);
            }

            return ret;
        }

        public bool ShouldStayOpen(Player player, bool checkForRightClick)
        {
            if ((player.mouseInterface || player.lastMouseInterface) ||
                (player.dead) ||
                (checkForRightClick && Main.mouseRight && Main.mouseRightRelease) ||
                (player.HeldItem.type != ItemType))
                return false;

            return true;
        }

        public bool IsHovered()
        {
            (bool centerHovered, bool[] buttonsHovered) = GetHoveredButtons();
            return (centerHovered) || (buttonsHovered.Any(b => b == true));
        }
    }
}
