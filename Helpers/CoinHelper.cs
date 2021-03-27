using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Helpers
{
    public static class CoinHelper
    {
        public static List<int> CoinTypes = new List<int>() {
            ItemID.CopperCoin, ItemID.SilverCoin, ItemID.GoldCoin, ItemID.PlatinumCoin
        };

        public const int Copper = 1;
        public const int Silver = 100;
        public const int Gold = 10000;
        public const int Platinum = 1000000;

        public static int CoinType(int item)
        {
            if (item == ItemID.CopperCoin)
                return 1;

            if (item == ItemID.SilverCoin)
                return 2;

            if (item == ItemID.GoldCoin)
                return 3;

            if (item == ItemID.PlatinumCoin)
                return 4;

            return 0;
        }

        public static void VoidPig(Item[] pInv, Item[] cInv)
        {
            int[] coinsToAdd = new int[4];
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            bool flag2 = false;
            int[] array2 = new int[40];
            for (int i = 0; i < cInv.Length; i++)
            {
                array2[i] = -1;
                if (cInv[i].stack < 1 || cInv[i].type <= ItemID.None)
                {
                    list2.Add(i);
                    cInv[i] = new Item();
                }

                if (cInv[i] != null && cInv[i].stack > 0)
                {
                    int coinType = CoinType(cInv[i].type);

                    array2[i] = coinType - 1;
                    if (coinType > 0)
                    {
                        coinsToAdd[coinType - 1] += cInv[i].stack;
                        list2.Add(i);
                        cInv[i] = new Item();
                        flag2 = true;
                    }
                }
            }

            if (!flag2)
                return;

            for (int j = 0; j < pInv.Length; j++)
            {
                if (j != 58 && pInv[j] != null && pInv[j].stack > 0 && !pInv[j].favorited)
                {
                    int coinType = CoinType(pInv[j].type);


                    if (coinType > 0)
                    {
                        coinsToAdd[coinType - 1] += pInv[j].stack;
                        list.Add(j);
                        pInv[j] = new Item();
                    }
                }
            }

            for (int k = 0; k < 3; k++)
            {
                while (coinsToAdd[k] >= 100)
                {
                    coinsToAdd[k] -= 100;
                    coinsToAdd[k + 1]++;
                }
            }

            for (int l = 0; l < 40; l++)
            {
                if (array2[l] < 0 || cInv[l].type != ItemID.None)
                    continue;

                int num3 = l;
                int num4 = array2[l];
                if (coinsToAdd[num4] > 0)
                {
                    cInv[num3].SetDefaults(71 + num4);
                    cInv[num3].stack = coinsToAdd[num4];
                    if (cInv[num3].stack > cInv[num3].maxStack)
                        cInv[num3].stack = cInv[num3].maxStack;

                    coinsToAdd[num4] -= cInv[num3].stack;
                    array2[l] = -1;
                }

                if (Main.netMode == NetmodeID.MultiplayerClient && Main.player[Main.myPlayer].chest > -1)
                    NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, Main.player[Main.myPlayer].chest, num3);

                list2.Remove(num3);
            }

            for (int m = 0; m < 40; m++)
            {
                if (array2[m] < 0 || cInv[m].type != ItemID.None)
                    continue;

                int num5 = m;
                int num6 = 3;
                while (num6 >= 0)
                {
                    if (coinsToAdd[num6] > 0)
                    {
                        cInv[num5].SetDefaults(71 + num6);
                        cInv[num5].stack = coinsToAdd[num6];
                        if (cInv[num5].stack > cInv[num5].maxStack)
                            cInv[num5].stack = cInv[num5].maxStack;

                        coinsToAdd[num6] -= cInv[num5].stack;
                        array2[m] = -1;
                        break;
                    }

                    if (coinsToAdd[num6] == 0)
                        num6--;
                }

                if (Main.netMode == NetmodeID.MultiplayerClient && Main.player[Main.myPlayer].chest > -1)
                    NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, Main.player[Main.myPlayer].chest, num5);

                list2.Remove(num5);
            }

            while (list2.Count > 0)
            {
                int num7 = list2[0];
                int num8 = 3;
                while (num8 >= 0)
                {
                    if (coinsToAdd[num8] > 0)
                    {
                        cInv[num7].SetDefaults(71 + num8);
                        cInv[num7].stack = coinsToAdd[num8];
                        if (cInv[num7].stack > cInv[num7].maxStack)
                            cInv[num7].stack = cInv[num7].maxStack;

                        coinsToAdd[num8] -= cInv[num7].stack;
                        break;
                    }

                    if (coinsToAdd[num8] == 0)
                        num8--;
                }

                if (Main.netMode == NetmodeID.MultiplayerClient && Main.player[Main.myPlayer].chest > -1)
                    NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, Main.player[Main.myPlayer].chest, list2[0]);

                list2.RemoveAt(0);
            }

            int num9 = 3;
            while (num9 >= 0 && list.Count > 0)
            {
                int num10 = list[0];
                if (coinsToAdd[num9] > 0)
                {
                    pInv[num10].SetDefaults(71 + num9);
                    pInv[num10].stack = coinsToAdd[num9];
                    if (pInv[num10].stack > pInv[num10].maxStack)
                        pInv[num10].stack = pInv[num10].maxStack;

                    coinsToAdd[num9] -= pInv[num10].stack;
                    list.RemoveAt(0);
                }

                if (coinsToAdd[num9] == 0)
                    num9--;
            }
        }
    }
}
