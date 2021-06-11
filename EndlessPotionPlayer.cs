using System;
using System.Collections;
using PboneUtils.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PboneUtils.Helpers;
using Terraria;
using Terraria.ModLoader;

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

        private static Mod ExtInv => ModLoader.GetMod("ExtensibleInventory");

        private static Assembly ExtInvAsm => ExtInv.Code;

        private static Type ExtInvPlayerType => ExtInvAsm.GetType("ExtensibleInventory.ExtensibleInventoryPlayer");

        private static Type LibraryType => ExtInvAsm.GetType("ExtensibleInventory.Inventory.InventoryLibrary");

        private static Type BookType => ExtInvAsm.GetType("ExtensibleInventory.Inventory.InventoryBook");

        private static Type PageType => ExtInvAsm.GetType("ExtensibleInventory.Inventory.InventoryPage");

        private static FieldInfo BooksField => LibraryType.GetField("Books", ReflectionHelper.AccessFlags);

        private static FieldInfo PagesField => BookType.GetField("Pages", ReflectionHelper.AccessFlags);

        private static FieldInfo ItemsField => PageType.GetField("Items", ReflectionHelper.AccessFlags);

        private object Library => ExtInvPlayerType.GetField("Library", ReflectionHelper.AccessFlags)
            ?.GetValue(player.GetModPlayer(ExtInv, "ExtensibleInventoryPlayer"));

        public static List<(Item, string)> FromArray(Item[] items, string key) => items.Select(t => (t, key)).ToList();

        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();

            if (!PboneUtilsConfig.Instance.EndlessPotions)
                return;

            if (Main.GameUpdateCount % 1.5 == 0) // 40 times a second
            {
                ItemsToCountForEndlessBuffs.Clear();

                ItemsToCountForEndlessBuffs.AddRange(FromArray(player.inventory, "Inventory"));
                InventoryItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(FromArray(player.bank.item, "PiggyBank"));
                PiggyBankItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(FromArray(player.bank2.item, "Safe"));
                SafeItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                ItemsToCountForEndlessBuffs.AddRange(FromArray(player.bank3.item, "DefendersForge"));
                DefendersForgeItemsStart = ItemsToCountForEndlessBuffs.Count - 1;

                if (ModLoader.GetMod("ExtensibleInventory") != null)
                {
                    IEnumerable<DictionaryEntry> CastDict(IDictionary dictionary)
                    {
                        foreach (DictionaryEntry entry in dictionary)
                            yield return entry;
                    }

                    IEnumerable<object> CastList(IList list)
                    {
                        foreach (object item in list)
                            yield return item;
                    }

                    if (Library != null)
                    {
                        Dictionary<string, object> books = CastDict((IDictionary) BooksField.GetValue(Library))
                            .ToDictionary(x => (string) x.Key, x => x.Value);

                        foreach (object page in books.Values
                            .Select(book => CastList((IList) PagesField.GetValue(book)).ToList())
                            .SelectMany(pages => pages))
                        {
                            if (ItemsField.GetValue(page) is Item[] items)
                                ItemsToCountForEndlessBuffs.AddRange(FromArray(items, "Inventory"));
                        }
                    }

                    ExtensibleInventoryItemsStart = ItemsToCountForEndlessBuffs.Count - 1;
                }
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