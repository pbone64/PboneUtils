using PboneLib.Services.CrossMod.Ref;
using PboneUtils.NPCs.Town;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PboneUtils.CrossMod.Ref.Content
{
    [ModRef("Census")]
    public class CensusCompatability : SimpleModCompatibility
    {
        public CensusCompatability() : base()
        {
        }

        public override void Load()
        {
            // string:"TownNPCCondition" - int:npcid - string:condition

            if (PboneUtilsConfig.Instance.Miner)
                GetMod().Call("TownNPCCondition", ModContent.NPCType<Miner>(), Language.GetTextValue("Mods.PboneUtils.Census.Description.Miner"));

            if (PboneUtilsConfig.Instance.MysteriousTrader)
                GetMod().Call("TownNPCCondition", ModContent.NPCType<MysteriousTrader>(), Language.GetTextValue("Mods.PboneUtils.Census.Description.MysteriousTrader"));
        }
    }
}
