using Terraria.ModLoader;

namespace PboneUtils.NPCs
{
    public abstract class PNPC : ModNPC
    {
        public virtual bool AutoloadCondition => true;
        public override bool Autoload(ref string name) => AutoloadCondition;
    }
}
