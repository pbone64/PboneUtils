using PboneUtils.CrossMod.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace PboneUtils.DataStructures
{
    public class CompiledMysteriousTraderShop
    {
        public static CompiledMysteriousTraderShop Instance;
        public bool AnyCall;

        public Dictionary<MysteriousTraderItemRarity, List<MysteriousTraderItem>> ItemCollections = new Dictionary<MysteriousTraderItemRarity, List<MysteriousTraderItem>>() {
            { MysteriousTraderItemRarity.NonUnique, new List<MysteriousTraderItem>() },
            { MysteriousTraderItemRarity.Common, new List<MysteriousTraderItem>() },
            { MysteriousTraderItemRarity.Rare, new List<MysteriousTraderItem>() },
            { MysteriousTraderItemRarity.Legendary, new List<MysteriousTraderItem>() }
        };

        public CompiledMysteriousTraderShop(MysteriousTraderShopManager shopBuilder)
        {
            FillRarity(MysteriousTraderItemRarity.NonUnique, shopBuilder);
            FillRarity(MysteriousTraderItemRarity.Common, shopBuilder);
            FillRarity(MysteriousTraderItemRarity.Rare, shopBuilder);
            FillRarity(MysteriousTraderItemRarity.Legendary, shopBuilder);

            AnyCall = shopBuilder.AnyCall;

            if (Instance == null)
                Instance = this;
        }

        private void FillRarity(MysteriousTraderItemRarity rarity, MysteriousTraderShopManager shopBuilder)
        {
            ItemCollections[rarity] = shopBuilder.Items.Where(k => k.Value == rarity).ToDictionary(k => k.Key).Keys.ToList();
        }

        public void FillShop(List<int> shop)
        {
            const int NonUniqueMin = 2;
            const int NonUniqueMax = 4;
            const int CommonMin = 2;
            const int CommonMax = 3;
            const int Rare = 1;
            const float LegendaryChance = 0.1f;

            int numNonUnique = Main.rand.Next(NonUniqueMin, NonUniqueMax + 1);
            int numCommon = Main.rand.Next(CommonMin, CommonMax + 1);
            int numRare = Rare;
            bool legendary = Main.rand.NextFloat() < LegendaryChance;

            int extraChancesInCaseOfFaliure = 4;

            cache = (MysteriousTraderItemRarity.Invalid, null);

            DoGiveItems(shop, MysteriousTraderItemRarity.NonUnique, numNonUnique, ref extraChancesInCaseOfFaliure);
            DoGiveItems(shop, MysteriousTraderItemRarity.Common, numCommon, ref extraChancesInCaseOfFaliure);
            DoGiveItems(shop, MysteriousTraderItemRarity.Rare, numRare, ref extraChancesInCaseOfFaliure);

            if (legendary)
                GiveShopRandomItem(shop, MysteriousTraderItemRarity.Legendary, false);

            // Recheck for modded chance
            legendary = Main.rand.NextFloat() < LegendaryChance;
            if (legendary)
                GiveShopRandomItem(shop, MysteriousTraderItemRarity.Legendary, true);
        }

        private void DoGiveItems(List<int> shop, MysteriousTraderItemRarity rare, int num, ref int extraChances)
        {
            for (int i = 0; i < num; i++)
            {
                if (GiveShopRandomItem(shop, rare, false) == false)
                {
                    i--;
                    extraChances--;
                }
            }
            GiveShopRandomItem(shop, rare, true);
        }

        private (MysteriousTraderItemRarity cachedRare, MysteriousTraderItem[] items) cache;
        private bool? GiveShopRandomItem(List<int> shop, MysteriousTraderItemRarity rare, bool call)
        {
            if (call && !AnyCall)
                return null;

            if (cache.cachedRare != rare)
                cache = (rare, ItemCollections[rare].Where(
                    i => i.Source == (call ? MysteriousTraderItemSource.Call : MysteriousTraderItemSource.Base)).ToArray());

            MysteriousTraderItem item =
                Main.rand.Next(cache.items);

            if (!item.Condition())
                return false;

            shop.Add(item.Type);
            return true;
        }
    }

    public struct MysteriousTraderItem
    {
        internal int Type;
        internal MysteriousTraderItemSource Source;
        internal Func<bool> Condition;

        internal MysteriousTraderItem(int type, MysteriousTraderItemSource source, Func<bool> condition)
        {
            Type = type;
            Source = source;
            Condition = condition;
        }
    }

    // Base = added by PboneUtils, Call = added by other mod
    public enum MysteriousTraderItemSource
    {
        Base,
        Call
    }

    public enum MysteriousTraderItemRarity
    {
        Invalid = -1,
        NonUnique,
        Common,
        Rare,
        Legendary
    }
}
