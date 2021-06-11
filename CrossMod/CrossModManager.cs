using PboneUtils.CrossMod.Call;
using PboneUtils.CrossMod.Ref;
using Terraria.ModLoader;

namespace PboneUtils.CrossMod
{
    public class CrossModManager
    {
        internal ModCallManager CallManager;
        internal ModRefManager RefManager;

        internal CrossModManager()
        {
            CallManager = new ModCallManager();
            RefManager = new ModRefManager();
        }

        internal void Load()
        {
            CallManager.Load();
            RefManager.Load();
        }

        internal T GetModCompatibility<T>(string mod) where T : IModCompatibility => (T)RefManager.GetModCompatibility(mod);
        internal Mod GetMod(string mod) => RefManager.GetModCompatibility(mod).GetMod();
        internal bool IsModLoaded(string mod) => RefManager.GetModCompatibility(mod).IsLoaded();
    }
}
