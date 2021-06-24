using PboneUtils.NPCs.Town;
using Terraria.ModLoader;

namespace PboneUtils.CrossMod.Ref.Content
{
    [ModRef("Census")]
    public class CensusCompatability : SimpleModCompatibility
    {
        public CensusCompatability() : base()
        {
        }

        public void Load()
        {
            // string:"TownNPCCondition" - int:npcid - string:condition

            if (PboneUtilsConfig.Instance.Miner)
                GetMod().Call("TownNPCCondition", ModContent.NPCType<Miner>(), "Defeat Eater of Worlds/Brain of Cthulhu");

            if (PboneUtilsConfig.Instance.MysteriousTrader)
                GetMod().Call("TownNPCCondition", ModContent.NPCType<MysteriousTrader>(), "Randomly arrives in the morning and leaves at night, after King Slime or Eye of Cthulhu");
        }
    }
}
