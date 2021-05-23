using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace PboneUtils
{
    public class PboneWorld : ModWorld
    {
        public ModWorldgenManager ModWorldGen;

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
            /*ModWorldGen = new ModWorldgenManager();

            tasks.Add(new PassLegacy("pbone's Utilities Petrified Safes", ModWorldGen.GenPetrifiedSafes));*/
        }
    }
}
