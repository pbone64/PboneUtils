using Microsoft.Xna.Framework;
using PboneUtils.Helpers;
using PboneUtils.Items.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items
{
    public class PGlobalItem : GlobalItem
    {
        public const int HallowedTreasureManget = 640;
        public const int DeluxeTreasureMagnetRange = 320;

        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            if (TreasureBagValueCalculator.Loaded && PboneUtilsConfig.Instance.AverageBossBags && item.value == 0 && PboneUtils.BagValues.AveragedValues.ContainsKey(item.type))
            {
                item.value = PboneUtils.BagValues.AveragedValues[item.type];
            }

            if (PboneUtilsConfig.Instance.MaxStackIncrease)
            {
                // Any non-coin with a max stack greater than twenty
                if (!CoinHelper.CoinTypes.Contains(item.type) && item.maxStack >= 20)
                {
                    // Increase to 9999 if it's below
                    item.TryIncreaseMaxStack(9999);
                }
            }
        }

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

            // Range increases
            if (mPlayer.HallowedTreasureMagnet)
            {
                grabRange += HallowedTreasureManget;
            }
            else if (mPlayer.DeluxeTreasureMagnet)
            {
                grabRange += DeluxeTreasureMagnetRange;
            }
        }

        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            // MP TODO: is this run on all clients?
            base.Update(item, ref gravity, ref maxFallSpeed);
            PbonePlayer mPlayer = Main.LocalPlayer.GetModPlayer<PbonePlayer>();

            // Super grabs
            int superGrabCooldownMax = -1;
            int superGrabRange = -1;
            int superGrabDust = -1;

            if (mPlayer.RunicTreasureMagnet)
            {
                superGrabCooldownMax = 10;
                superGrabRange = 2560;
                superGrabDust = 169;
            }
            else if (mPlayer.SpectreTreasureMagnet)
            {
                superGrabCooldownMax = 15;
                superGrabRange = 1280;
                superGrabDust = 15;
            }

            if (superGrabCooldownMax != -1 && superGrabRange != -1)
            {
                if (Main.LocalPlayer.Distance(item.Center) <= superGrabRange)
                {
                    if (mPlayer.SuperGrabCooldown-- <= 0)
                    {
                        const int numDust = 20;
                        for (int i = 0; i < numDust; i++)
                        {
                            Dust d = Dust.NewDustDirect(Vector2.Lerp(item.Center, Main.LocalPlayer.Center, (float)i / (float)numDust), 1, 1, superGrabDust, 0, 0);
                            d.noGravity = true;
                            d.alpha = 200;
                        }

                        item.noGrabDelay = 0;
                        item.Center = Main.LocalPlayer.Center;
                        mPlayer.SuperGrabCooldown = superGrabCooldownMax;

                        if (Main.netMode != NetmodeID.SinglePlayer)
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item.whoAmI);
                    }
                }
            }
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (item.healLife > 0 && PboneUtilsConfig.Instance.EndlessPotions && item.stack >= PboneUtilsConfig.Instance.EndlessPotionsSlider)
            {
                return false;
            }

            return base.ConsumeItem(item, player);
        }
    }
}
