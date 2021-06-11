using Terraria;

namespace PboneUtils.DataStructures
{
    public struct EndlessBuffSource
    {
        public Item Item;
        public string Key;

        public EndlessBuffSource(Item item, string key)
        {
            Item = item;
            Key = key;
        }
    }
}