using PboneUtils.DataStructures;
using System.Collections.Generic;
using PboneUtils.Helpers;
using Terraria;
using PboneUtils.CrossMod.Ref.Content;
using PboneLib.CustomLoading.Implementations;

namespace PboneUtils.MiscModPlayers
{
    public class EndlessPotionPlayer : PPlayer
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

                ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(Player.inventory, "Inventory"));
                InventoryItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(Player.bank.item, "PiggyBank"));
                PiggyBankItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(Player.bank2.item, "Safe"));
                SafeItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(Player.bank3.item, "DefendersForge"));
                DefendersForgeItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                if (PboneUtils.CrossMod.IsModLoaded("ExtensibleInventory"))
                    PboneUtils.CrossMod.GetModCompatibility<ExtensibleInventoryCompatibility>("ExtensibleInventory").DoEndlessBuffs(Player);
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
                    Player.AddBuff(item.buffType, 20);
            }
        }
    }
}