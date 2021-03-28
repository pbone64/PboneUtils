using PboneUtils.Helpers;
using PboneUtils.ID;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Liquid
{
    public class BottomlessHoneyBucket : PItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 12;
            item.useTime = 5;
            item.useTurn = true;
            item.autoReuse = true;
            item.shootSpeed = 4;
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.tileBoost += 2;
        }

        public override bool UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                if (player.IsTargetTileInItemRange(item))
                {
                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].honey())
                    {
                        Main.PlaySound(SoundID.Splash, (int)player.position.X, (int)player.position.Y);
                        Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType(LiquidID.Honey);
                        Main.tile[Player.tileTargetX, Player.tileTargetY].honey(true);
                        Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = byte.MaxValue;
                        WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);

                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);

                        return true;
                    }
                }
            }

            return base.UseItem(player);
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);

            if (player.IsTargetTileInItemRange(item))
            {
                player.showItemIcon = true;
                player.showItemIcon2 = item.type;
            }
        }
    }
}
