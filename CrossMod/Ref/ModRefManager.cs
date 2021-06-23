using PboneUtils.CrossMod.Ref.Content;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace PboneUtils.CrossMod.Ref
{
    internal class ModRefManager
    {
        private Dictionary<string, IModCompatibility> ModCompatabilitiesByMod;

        internal void Load()
        {
            ModCompatabilitiesByMod = new Dictionary<string, IModCompatibility>();
            LoadCompatabilities();
        }

        internal IModCompatibility GetModCompatibility(string mod) => ModCompatabilitiesByMod[mod];
        internal Mod GetMod(string mod) => GetModCompatibility(mod).GetMod();
        internal bool IsModLoaded(string mod) => GetModCompatibility(mod).IsLoaded();

        private void LoadCompatabilities()
        {
            RegisterCompatibility<FargowiltasCompatibility>();
            RegisterCompatibility<FargowiltasSoulsCompatibility>();
            RegisterCompatibility<ExtensibleInventoryCompatibility>();
            RegisterCompatibility<CensusCompatability>();
        }

        private void RegisterCompatibility<T>() where T : IModCompatibility, new()
        {
            IModCompatibility compatibility = new T();
            ModCompatabilitiesByMod.Add(compatibility.GetModName(), compatibility);
        }
    }
}
