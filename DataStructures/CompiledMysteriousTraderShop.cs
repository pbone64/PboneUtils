using PboneUtils.CrossMod.Content;
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

        public CompiledMysteriousTraderShop(MysteriousTraderShopInterface shopBuilder)
        {
            FillRarity(MysteriousTraderItemRarity.NonUnique, shopBuilder);
            FillRarity(MysteriousTraderItemRarity.Common, shopBuilder);
            FillRarity(MysteriousTraderItemRarity.Rare, shopBuilder);
            FillRarity(MysteriousTraderItemRarity.Legendary, shopBuilder);

            AnyCall = shopBuilder.AnyCall;

            if (Instance == null)
                Instance = this;
        }

        private void FillRarity(MysteriousTraderItemRarity rarity, MysteriousTraderShopInterface shopBuilder)
        {
            ItemCollections[rarity] = shopBuilder.Items.Where(k => k.Value == rarity).ToDictionary(k => k.Key).Keys.ToList();
        }

        public void FillShop(Chest shop, ref int nextSlot)
        {
            const int NonUniqueMin = 2;
            const int NonUniqueMax = 4;
            const int CommonMin = 2;
            const int CommonMax = 3;
            const int Rare = 1;
            const float LegendaryChance = 0.33f;

            int numNonUnique = Main.rand.Next(NonUniqueMin, NonUniqueMax + 1);
            int numCommon = Main.rand.Next(NonUniqueMin, NonUniqueMax + 1);
            int numRare = Main.rand.Next(NonUniqueMin, NonUniqueMax + 1);
            bool legendary = Main.rand.NextFloat() < LegendaryChance;

            for (int i = 0; i < numNonUnique; i++)
            {
                GiveShopRandomItem(shop, ref nextSlot, MysteriousTraderItemRarity.NonUnique, false);
            }
            GiveShopRandomItem(shop, ref nextSlot, MysteriousTraderItemRarity.NonUnique, true);
        }

        private void GiveShopRandomItem(Chest shop, ref int nextSlot, MysteriousTraderItemRarity rare, bool call)
        {
            if (call && !AnyCall)
                return;

            MysteriousTraderItem item =
                Main.rand.Next(ItemCollections[rare].Where(i => i.Source == (call ? MysteriousTraderItemSource.Call : MysteriousTraderItemSource.Base)).ToArray());

            shop.item[nextSlot].SetDefaults(item.Type);
            nextSlot++;
        }
    }

    public struct MysteriousTraderItem
    {
        internal int Type;
        internal MysteriousTraderItemSource Source;

        internal MysteriousTraderItem(int type, MysteriousTraderItemSource source)
        {
            Type = type;
            Source = source;
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
        NonUnique,
        Common,
        Rare,
        Legendary
    }
}
