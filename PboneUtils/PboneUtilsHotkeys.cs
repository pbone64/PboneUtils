using Microsoft.Xna.Framework.Input;
using PboneLib.CustomLoading.Content;
using Terraria.ModLoader;

namespace PboneUtils
{
    public class PboneUtilsHotkeys : ILoadable, ICustomLoadable
    {
        public static PboneUtilsHotkeys Instance => ModContent.GetInstance<PboneUtilsHotkeys>();
        public static ModKeybind TerraformingScrollModeKey => Instance.terraformingScrollModeKey;

        private ModKeybind terraformingScrollModeKey;

        public bool LoadCondition() => true;

        public void Load(Mod mod)
        {
            terraformingScrollModeKey = KeybindLoader.RegisterKeybind(mod, "Terraforming: Scroll Mode", Keys.LeftAlt);
        }

        public void Unload()
        {
            terraformingScrollModeKey = null;
        }
    }
}
