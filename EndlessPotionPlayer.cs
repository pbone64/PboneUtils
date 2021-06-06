using PboneUtils.DataStructures;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace PboneUtils
{
    public class EndlessPotionPlayer : ModPlayer
    {
        public Dictionary<int, EndlessBuffSource> EndlessBuffSources = new Dictionary<int, EndlessBuffSource>();
        public HashSet<int> DisabledBuffs = new HashSet<int>();
        public List<Item> ItemsToCountForEndlessBuffs = new List<Item>();

        public int InventoryItemsStart = 0;
        public int PiggyBankItemsStart = 0;
        public int SafeItemsStart = 0;
        public int DefendersForgeItemStart = 0;

        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();
            if (PboneUtilsConfig.Instance.EndlessPotions)
            {
                if (Main.GameUpdateCount % 1.5 == 0) // 40 times a second
                {
                    ItemsToCountForEndlessBuffs.Clear();

                    ItemsToCountForEndlessBuffs.AddRange(player.inventory);
                    InventoryItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                    ItemsToCountForEndlessBuffs.AddRange(player.bank.item);
                    PiggyBankItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                    ItemsToCountForEndlessBuffs.AddRange(player.bank2.item);
                    SafeItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                    ItemsToCountForEndlessBuffs.AddRange(player.bank3.item);
                    DefendersForgeItemStart = ItemsToCountForEndlessBuffs.Count - 1;
                }

                EndlessBuffSources.Clear();
                for (int i = 0; i < ItemsToCountForEndlessBuffs.Count; i++)
                {
                    Item item = ItemsToCountForEndlessBuffs[i];

                    if (item.buffType > 0) // The first buff, obsidian skin, is 1
                    {
                        if (item.stack >= PboneUtilsConfig.Instance.EndlessPotionsSlider && !EndlessBuffSources.ContainsKey(item.buffType))
                        {
                            string key =
                                (i <= DefendersForgeItemStart ?
                                    (i <= SafeItemsStart ?
                                        (i <= PiggyBankItemsStart ? "Inventory" : "PiggyBank")
                                    : "Safe")
                                : "DefendersForge");

                            EndlessBuffSources.Add(item.buffType, new EndlessBuffSource(item, key));

                            if (!DisabledBuffs.Contains(item.buffType))
                                player.AddBuff(item.buffType, 2);
                        }
                    }
                }
            }
        }
    }
}
