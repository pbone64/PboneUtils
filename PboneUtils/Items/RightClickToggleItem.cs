using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace PboneUtils.Items
{
    public abstract class RightClickToggleItem : PboneUtilsItem
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

            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendData(MessageID.SyncPlayer, number: player.whoAmI);
        }

        public override bool ConsumeItem(Player player) => false;

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
            if (!Enabled)
                spriteBatch.Draw(TextureAssets.Cd.Value,
                    position - TextureAssets.Cd.Size() * 0.5f + TextureAssets.Item[Item.type].Size() * 0.5f,
                    null, drawColor * 0.95f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
            writer.Write(Enabled);
        }



        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
            Enabled = reader.ReadBoolean();
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("Enabled", Enabled);

            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            Enabled = tag.GetBool("Enabled");

            base.LoadData(tag);
        }
    }
}
