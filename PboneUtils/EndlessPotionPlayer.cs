using PboneUtils.DataStructures;
using System.Collections.Generic;
using PboneUtils.Helpers;
using Terraria;
using Terraria.ModLoader;
using PboneUtils.CrossMod.Ref.Content;

namespace PboneUtils
{
    public class EndlessPotionPlayer : ModPlayer
    {
        public Dictionary<int, EndlessBuffSource> EndlessBuffSources = new Dictionary<int, EndlessBuffSource>();
        public HashSet<int> DisabledBuffs = new HashSet<int>();
        public List<(Item, string)> ItemsToCountForEndlessBuffs = new List<(Item, string)>();

        public int InventoryItemsStart;
        public int PiggyBankItemsStart;
        public int SafeItemsStart;
        public int DefendersForgeItemsStart;
        public int ExtensibleInventoryItemsStart;

        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();

            if (!PboneUtilsConfig.Instance.EndlessPotions)
                return;

            if (Main.GameUpdateCount % 1.5 == 0) // 40 times a second
            {
                ItemsToCountForEndlessBuffs.Clear();

                ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(player.inventory, "Inventory"));
                InventoryItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(player.bank.item, "PiggyBank"));
                PiggyBankItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(player.bank2.item, "Safe"));
                SafeItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(player.bank3.item, "DefendersForge"));
                DefendersForgeItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                if (PboneUtils.CrossMod.IsModLoaded("ExtensibleInventory"))
                    PboneUtils.CrossMod.GetModCompatibility<ExtensibleInventoryCompatibility>("ExtensibleInventory").DoEndlessBuffs(player);
            }

            EndlessBuffSources.Clear();
            foreach ((Item, string) val in ItemsToCountForEndlessBuffs)
            {
                (Item item, string key) = val;

                if (item.buffType <= 0)
                    continue;

                if (item.stack < PboneUtilsConfig.Instance.EndlessPotionsSlider ||
                    EndlessBuffSources.ContainsKey(item.buffType))
                    continue;

                EndlessBuffSources.Add(item.buffType, new EndlessBuffSource(item, key));

                if (!DisabledBuffs.Contains(item.buffType))
                    player.AddBuff(item.buffType, 20);
            }
        }
    }
}