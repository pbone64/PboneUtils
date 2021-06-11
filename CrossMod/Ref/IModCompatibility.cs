using Terraria.ModLoader;

namespace PboneUtils.CrossMod.Ref
{
    public interface IModCompatibility
    {
        bool IsLoaded();
        Mod GetMod();
        string GetModName();
    }
}
