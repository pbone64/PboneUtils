using PboneUtils.Helpers;
using PboneUtils.Items.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items
{
    public class PGlobalItem : GlobalItem
    {
        public const int RunicTreasureMagnetRange = 560;
        public const int TerraTreasureMagnetRange = 560;
        public const int DeluxeTreasureMagnetRange = 320;

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

            if (mPlayer.PhilosophersStone && player.HeldItem.type == ModContent.ItemType<PhilosophersStone>() && !CoinHelper.CoinTypes.Contains(item.type))
            {
                int value = item.value;
                Main.PlaySound(SoundID.CoinPickup);
                player.SellItem(value, item.stack);

                return false;
            }

            return base.OnPickup(item, player);
        }

        public override bool CanPickup(Item item, Player player)
        {
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();
            if (mPlayer.PhilosophersStone && player.HeldItem.type == ModContent.ItemType<PhilosophersStone>())
            {
                return true;
            }

            return base.CanPickup(item, player);
        }

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            base.GrabRange(item, player, ref grabRange);
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();

            if (mPlayer.RunicTreasureMagnet)
            {
                grabRange += RunicTreasureMagnetRange;
            }
            else if (mPlayer.TerraTreasureMagnet)
            {
                grabRange += TerraTreasureMagnetRange;
            }
            else if (mPlayer.DeluxeTreasureMagnet)
            {
                grabRange += DeluxeTreasureMagnetRange;
            }
        }
    }
}
