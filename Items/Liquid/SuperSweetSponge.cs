using PboneUtils.Helpers;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Liquid
{
    public class SuperSweetSponge : PItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 12;
            item.useTime = 5;
            item.useTurn = true;
            item.autoReuse = true;
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.tileBoost += 2;
        }

        public override bool UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                if (player.IsTargetTileInItemRange(item) && Main.tile[Player.tileTargetX, Player.tileTargetY].honey())
                {
					int targettedLiquid = Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType();
					int nearbyLiquid = 0;
					for (int i = Player.tileTargetX - 1; i <= Player.tileTargetX + 1; i++)
					{
						for (int j = Player.tileTargetY - 1; j <= Player.tileTargetY + 1; j++)
						{
							if (Main.tile[i, j].liquidType() == targettedLiquid)
								nearbyLiquid += Main.tile[i, j].liquid;
						}
					}

					if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid <= 0)
						return false;

					int liquidType = Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType();
					int liquidAmount = Main.tile[Player.tileTargetX, Player.tileTargetY].liquid;

					Main.PlaySound(SoundID.Splash, (int)player.position.X, (int)player.position.Y);

					Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 0;
					Main.tile[Player.tileTargetX, Player.tileTargetY].lava(lava: false);
					Main.tile[Player.tileTargetX, Player.tileTargetY].honey(honey: false);
					WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, resetFrame: false);

					if (Main.netMode == NetmodeID.MultiplayerClient)
						NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
					else
						Terraria.Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);

					for (int k = Player.tileTargetX - 1; k <= Player.tileTargetX + 1; k++)
					{
						for (int l = Player.tileTargetY - 1; l <= Player.tileTargetY + 1; l++)
						{
							if (liquidAmount < 256 && Main.tile[k, l].liquidType() == targettedLiquid)
							{
								int removeAmount = Main.tile[k, l].liquid;
								if (removeAmount + liquidAmount > 255)
									removeAmount = 255 - liquidAmount;

								liquidAmount += removeAmount;
								Main.tile[k, l].liquid -= (byte)removeAmount;
								Main.tile[k, l].liquidType(liquidType);
								if (Main.tile[k, l].liquid == 0)
								{
									Main.tile[k, l].lava(lava: false);
									Main.tile[k, l].honey(honey: false);
								}

								WorldGen.SquareTileFrame(k, l, resetFrame: false);
								if (Main.netMode == NetmodeID.MultiplayerClient)
									NetMessage.sendWater(k, l);
								else
									Terraria.Liquid.AddWater(k, l);
							}
						}
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
