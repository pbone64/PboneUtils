using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.IO;

namespace PboneUtils.DataStructures
{
    public class ItemConfig
    {
        public Dictionary<string, bool> Data = new Dictionary<string, bool>();
        public int ToggleCount => Data.Count;

        public bool HasRedMode;
        public bool RedMode;

        public bool OnlyOne;
        public string OnlyOneValue => Data.FirstOrDefault(kvp => kvp.Value).Key;

        public static Dictionary<string, ItemConfig> DefaultConfigs() => new Dictionary<string, ItemConfig>() {
            { "Liquid", new ItemConfig(true, true,
                ("Water", true),
                ("Lava", false),
                ("Honey", false)) },

            { "Light", new ItemConfig(true, false,
                ("Red", false),
                ("Orange", false),
                ("Yellow", false),
                ("Green", false),
                ("Blue", false),
                ("Purple", false),
                ("White", true)) }
        };

        public ItemConfig(bool onlyOne, bool hasRedMode, params (string key, bool def)[] args)
        {
            OnlyOne = onlyOne;
            HasRedMode = hasRedMode;

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

        public void AllOff()
        {
            Dictionary<string, bool> newData = new Dictionary<string, bool>();

            foreach (string s in Data.Keys)
            {
                newData[s] = false;
            }

            Data = newData;
        }
    }
}
