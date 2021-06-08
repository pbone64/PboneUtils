using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace PboneUtils
{
    public class PboneWorld : ModWorld
    {
        private ModWorldgenManager ModWorldGen;

        public static bool ForceFastForwardTime;

        public override void Initialize()
        {
            base.Initialize();
            ForceFastForwardTime = false;
        }

        public override void PostUpdate()
        {
            base.PostUpdate();
            if (ForceFastForwardTime)
            {
                Main.fastForwardTime = true;

                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.Assorted1, -1, -1, null, Main.myPlayer, 3f);
            }
        }

        public static void ForceStopTimeFastForward()
        {
            ForceFastForwardTime = false;
            Main.fastForwardTime = false;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.WorldData);
                NetMessage.SendData(MessageID.Assorted1, -1, -1, null, Main.myPlayer, 3f);
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
            writer.Write(ForceFastForwardTime);
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
            ForceFastForwardTime = reader.ReadBoolean();
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
            ModWorldGen = new ModWorldgenManager();

            tasks.Add(new PassLegacy("pbone's Utilities: Petrified Safes", ModWorldGen.GenPetrifiedSafes));
        }
    }
}
