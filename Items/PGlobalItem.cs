using PboneUtils.Helpers;
using Terraria;
using Terraria.ModLoader;

namespace PboneUtils.Items
{
    public class PGlobalItem : GlobalItem
    {
        public override void UpdateInventory(Item item, Player player)
        {
            base.UpdateInventory(item, player);
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();

            if (mPlayer.VoidPig && CoinHelper.CoinTypes.Contains(item.type))
            {
                CoinHelper.VoidPig(player.inventory, player.bank.item);
            }
        }

        public override bool OnPickup(Item item, Player player)
        {
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();

            if (mPlayer.PhilosophersStone && !CoinHelper.CoinTypes.Contains(item.type))
            {
                int value = item.value;
                player.SellItem(value, item.stack);

                return false;
            }

            return base.OnPickup(item, player);
        }

        public override bool CanPickup(Item item, Player player)
        {
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();
            if (mPlayer.PhilosophersStone)
            {
                return true;
            }

            return base.CanPickup(item, player);
        }

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            base.GrabRange(item, player, ref grabRange);
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();

            if (mPlayer.DeluxeTreasureMagnet)
            {
                grabRange += 640;
            }
        }
    }
}
