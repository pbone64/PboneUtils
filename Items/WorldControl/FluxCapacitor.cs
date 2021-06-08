using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.WorldControl
{
    public class FluxCapacitor : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.WorldControlItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = false;
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 0)
                item.channel = false;
            else
                item.channel = true;
            return base.CanUseItem(player);
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 0)
            {
                Main.dayTime = !Main.dayTime;
                Main.time = 0;
                if (++Main.moonPhase >= 8)
                {
                    Main.moonPhase = 0;
                }

                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.WorldData);

                return true;
            }
            else if (player.altFunctionUse == 2)
            {
                Main.fastForwardTime = !Main.fastForwardTime;

                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.Assorted1, -1, -1, null, Main.myPlayer, 3f);
                return true;
            }
            return base.UseItem(player);
        }
    }
}
