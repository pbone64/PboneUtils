using PboneLib.CustomLoading;
using Terraria.ModLoader;

namespace PboneUtils.NPCs
{
    public abstract class PNPC : ModNPC, IBetterLoadable
    {
        public bool AutoloadCondition => true;
        public bool LoadCondition() => AutoloadCondition;
    }
}
