using PboneUtils.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace PboneUtils.CrossMod.Ref.Content
{
    [ModRef("ExtensibleInventory")]
    public class ExtensibleInventoryCompatibility : SimpleModCompatibility
    {
        private static Mod ExtInv => ModLoader.GetMod("ExtensibleInventory");
        private static Assembly ExtInvAsm => ExtInv.Code;

        private static Type ExtInvPlayerType => ExtInvAsm.GetType("ExtensibleInventory.ExtensibleInventoryPlayer");
        private static Type LibraryType => ExtInvAsm.GetType("ExtensibleInventory.Inventory.InventoryLibrary");
        private static Type BookType => ExtInvAsm.GetType("ExtensibleInventory.Inventory.InventoryBook");
        private static Type PageType => ExtInvAsm.GetType("ExtensibleInventory.Inventory.InventoryPage");

        private static FieldInfo BooksField => LibraryType.GetField("Books", ReflectionHelper.AccessFlags);
        private static FieldInfo PagesField => BookType.GetField("Pages", ReflectionHelper.AccessFlags);
        private static FieldInfo ItemsField => PageType.GetField("Items", ReflectionHelper.AccessFlags);
        private static FieldInfo LibraryField => ExtInvPlayerType.GetField("Library", ReflectionHelper.AccessFlags);

        private object GetLibrary(Player player) => LibraryField?.GetValue(player.GetModPlayer(ExtInv, "ExtensibleInventoryPlayer"));

        public ExtensibleInventoryCompatibility() : base()
        {
        }

        public void DoEndlessBuffs(Player player)
        {
            EndlessPotionPlayer mPlayer = player.GetModPlayer<EndlessPotionPlayer>();

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

            object lib = GetLibrary(player);
            if (lib != null)
            {
                Dictionary<string, object> books = CastDict((IDictionary)BooksField.GetValue(lib))
                    .ToDictionary(x => (string)x.Key, x => x.Value);

                foreach (object page in books.Values
                    .Select(book => CastList((IList)PagesField.GetValue(book)).ToList())
                    .SelectMany(pages => pages))
                {

                    if (ItemsField.GetValue(page) is Item[] items)
                        mPlayer.ItemsToCountForEndlessBuffs.AddRange(CollectionHelper.FromArray(items, "Inventory"));
                }
            }

            mPlayer.ExtensibleInventoryItemsStart = mPlayer.ItemsToCountForEndlessBuffs.Count - 1;
        }
    }
}
