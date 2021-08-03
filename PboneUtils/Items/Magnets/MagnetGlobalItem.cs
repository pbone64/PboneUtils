using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Magnets
{
    public class MagnetGlobalItem : GlobalItem
    {
        public const int DeluxeTreasureMagnetRange = 320;
        public const int HallowedTreasureMangetRange = 640;

        public const int SpectreTreasureMagnetRange = 1280;
        public const int RunicTreasureMagnetRange = 1280;
        public const int MoonLordTreasureMagnetRange = 2560;

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            // MP TODO: see how this works in mp
            base.GrabRange(item, player, ref grabRange);
            MagnetPlayer mPlayer = player.GetModPlayer<MagnetPlayer>();

            // Range increases
            if (mPlayer.HallowedTreasureMagnet)
            {
                grabRange += HallowedTreasureMangetRange;
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
            MagnetPlayer mPlayer = Main.LocalPlayer.GetModPlayer<MagnetPlayer>();

            // Super grabs
            int superGrabCooldownMax = -1;
            int superGrabRange = -1;
            int superGrabDust = -1;

            if (mPlayer.MoonLordTreasureMagnet)
            {
                superGrabCooldownMax = 5;
                superGrabRange = MoonLordTreasureMagnetRange;
                superGrabDust = 160;
            }
            else if (mPlayer.RunicTreasureMagnet)
            {
                superGrabCooldownMax = 10;
                superGrabRange = RunicTreasureMagnetRange;
                superGrabDust = 169;
            }
            else if (mPlayer.SpectreTreasureMagnet)
            {
                superGrabCooldownMax = 15;
                superGrabRange = SpectreTreasureMagnetRange;
                superGrabDust = 15;
            }

            if (superGrabCooldownMax != -1 && superGrabRange != -1)
            {
                if (Main.LocalPlayer.Distance(Item.Center) <= superGrabRange)
                {
                    if (mPlayer.SuperGrabCooldown-- <= 0)
                    {
                        const int numDust = 20;
                        for (int i = 0; i < numDust; i++)
                        {
                            Dust d = Dust.NewDustDirect(Vector2.Lerp(Item.Center, Main.LocalPlayer.Center, (float)i / (float)numDust), 1, 1, superGrabDust, 0, 0);
                            d.noGravity = true;
                            d.alpha = 200;
                        }

                        Item.noGrabDelay = 0;
                        Item.Center = Main.LocalPlayer.Center;
                        mPlayer.SuperGrabCooldown = superGrabCooldownMax;

                        if (Main.netMode != NetmodeID.SinglePlayer)
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, Item.whoAmI);
                    }
                }
            }
        }
    }
}
