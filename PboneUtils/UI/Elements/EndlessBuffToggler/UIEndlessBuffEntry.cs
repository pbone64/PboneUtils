using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.DataStructures;
using PboneUtils.MiscModPlayers;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace PboneUtils.UI.Elements.EndlessBuffToggler
{
    public class UIEndlessBuffEntry : UIElement
    {
        public int Buff;
        public EndlessBuffSource Source;
        public bool Disabled;

        public UIEndlessBuffEntry(int buff, EndlessBuffSource source)
        {
            Buff = buff;
            Source = source;

            // All buff textures are 32x32
            Width.Set(32, 0f);
            Height.Set(32, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            // Drawing is being really weird, todo
            base.DrawSelf(spriteBatch);
            Texture2D texture = TextureAssets.Buff[Buff].Value;
            Vector2 position = GetDimensions().Position() + texture.Size() * (1f - 1f) / 2f;

            // Straight outta UIImage
            spriteBatch.Draw(texture,
                new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Disabled ? Color.DarkGray : Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f
            );

            bool alwaysHighlight = (Parent is IEndlessBuffGridHolder holder && holder.GetSelectedBuffHolder().Buff == Buff);

            if (ContainsPoint(Main.MouseScreen) || alwaysHighlight)
            {
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    if (Parent is IEndlessBuffGridHolder grid)
                    {
                        grid.HandleBuffEntryClick(this);
                    }
                }
                if (Main.mouseRight && Main.mouseRightRelease)
                {
                    if (Parent is UIEndlessBuffGrid grid)
                    {
                        if (grid.GetSelectedBuffHolder().Buff == Buff)
                        {
                            Disabled = !Disabled;
                            EndlessPotionPlayer player = Main.LocalPlayer.GetModPlayer<EndlessPotionPlayer>();

                            if (Disabled)
                            {
                                player.DisabledBuffs.Add(Buff);
                            }
                            else
                            {
                                player.DisabledBuffs.Remove(Buff);
                            }
                        }
                    }
                }

                texture = PboneUtils.Textures["BuffTogglerInventoryButton_MouseOver"];
                position -= new Vector2(2);

                spriteBatch.Draw(texture,
                    new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    alwaysHighlight  ? Color.White : Color.White * 0.33f, 0f, Vector2.Zero, SpriteEffects.None, 0f
                );
            }
        }
    }
}
