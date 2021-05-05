using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace PboneUtils
{
    public class EndlessPotionPlayer : ModPlayer
    {
        public List<Item> buffItems = new List<Item>();

        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();
            if (PboneUtilsConfig.Instance.EndlessPotions)
            {
                if (Main.GameUpdateCount % 2 == 0) // 30 times a second
                {
                    buffItems.Clear();
                    buffItems.AddRange(player.inventory);
                    buffItems.AddRange(player.bank.item);
                    buffItems.AddRange(player.bank2.item);
                    buffItems.AddRange(player.bank3.item);
                }

                foreach (Item item in buffItems)
                {
                    if (item.buffType > 0) // The first buff, obsidian skin, is 1
                    {
                        if (item.stack >= PboneUtilsConfig.Instance.EndlessPotionsSlider)
                        {
                            player.AddBuff(item.buffType, 2);
                        }
                    }
                }
            }
        }
    }
}
