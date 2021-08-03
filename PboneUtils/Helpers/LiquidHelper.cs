using PboneUtils.ID;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Helpers
{
    public static class LiquidHelper
    {
        public static bool PlaceLiquid(int x, int y, byte type)
        {
            Tile tile = Framing.GetTileSafely(x, y);

            if (tile.liquid == 0 || tile.liquidType() == type)
            {
                SoundEngine.PlaySound(SoundID.Splash, x, y);
                tile.liquidType(type);
                tile.liquid = byte.MaxValue;
                WorldGen.SquareTileFrame(x, y);

                if (type == LiquidID.Honey)
                {
                    tile.lava(false);
                    tile.honey(true);
                }
                else if (type == LiquidID.Lava)
                {
                    tile.lava(true);
                    tile.honey(false);
                }
                else if (type == LiquidID.Water)
                {
                    tile.lava(false);
                    tile.honey(false);
                }

                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.sendWater(x, y);

                return true;
            }

            return false;
        }

        public static bool DrainLiquid(int x, int y, byte type)
        {
            int targettedLiquid = Main.tile[x, y].liquidType();
            int nearbyLiquid = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (Main.tile[i, j].liquidType() == targettedLiquid)
                        nearbyLiquid += Main.tile[i, j].liquid;
                }
            }

            if (Main.tile[x, y].liquid <= 0)
                return false;

            int liquidType = Main.tile[x, y].liquidType();
            int liquidAmount = Main.tile[x, y].liquid;

            if (liquidType != type)
                return false;

            Main.tile[x, y].liquid = 0;

            Main.tile[x, y].lava(lava: false);
            Main.tile[x, y].honey(honey: false);

            WorldGen.SquareTileFrame(x, y, resetFrame: false);

            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.sendWater(x, y);
            else
                Liquid.AddWater(x, y);

            for (int k = x - 1; k <= x + 1; k++)
            {
                for (int l = y - 1; l <= y + 1; l++)
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
                            Liquid.AddWater(k, l);
                    }
                }
            }

            return true;
        }
    }
}
