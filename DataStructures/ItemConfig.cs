using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace PboneUtils.DataStructures
{
    public class ItemConfig
    {
        public Dictionary<string, bool> Data = new Dictionary<string, bool>();
        public bool RedMode;
        public int ToggleCount => Data.Count;

        public static Dictionary<string, ItemConfig> DefaultConfigs() => new Dictionary<string, ItemConfig>() {
            { "Liquid", new ItemConfig(("Water", true), ("Lava", false), ("Honey", false)) }
        };

        public ItemConfig(params (string key, bool def)[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Data.Add(args[i].key, args[i].def);
            }
        }

        public TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            foreach (KeyValuePair<string, bool> kvp in Data)
            {
                tag.Add(kvp.Key, kvp.Value);
            }

            return tag;
        }

        public void Load(TagCompound tag, string key)
        {
            Dictionary<string, bool> parsedData = new Dictionary<string, bool>();

            foreach (string s in Data.Keys)
            {
                parsedData[s] = tag.GetBool($"{key}.{s}");
            }

            Data = parsedData;
        }
    }
}
