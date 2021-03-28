using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace PboneUtils.Items
{
    public abstract class RightClickToggleItem : PItem
    {
        public bool Enabled = true;

        public override bool CanRightClick() => true;
        public override void RightClick(Player player)
        {
            base.RightClick(player);
            Enabled = !Enabled;

            string text = Language.GetTextValue("Mods.PboneUtils.Common.Enabled." + Enabled.ToString());
            Color color = Enabled ? CombatText.HealLife : CombatText.DamagedFriendly;
            CombatText.NewText(player.getRect(), color, text);
        }

        public override bool ConsumeItem(Player player) => false;

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
            if (!Enabled)
                spriteBatch.Draw(Main.cdTexture, position - Main.cdTexture.Size() * 0.5f + Main.itemTexture[item.type].Size() * 0.5f, null, drawColor * 0.85f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound() {
                { "Enabled", Enabled }
            };
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            base.Load(tag);
            Enabled = tag.GetBool("Enabled");
        }
    }
}
