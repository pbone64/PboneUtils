using PboneUtils.DataStructures.MysteriousTrader;
using PboneUtils.Helpers;
using PboneUtils.ID;
using PboneUtils.NPCs.Town;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace PboneUtils
{
    public class PboneWorld : ModWorld
    {
        private ModWorldgenManager ModWorldGen;

        public static bool ForceFastForwardTime;

        private static MethodInfo startRainMethod;
        public static Action StartRain = new Action(() => startRainMethod.Invoke(null, new object[] { }));
        private static MethodInfo stopRainMethod;
        public static Action StopRain = new Action(() => stopRainMethod.Invoke(null, new object[] { }));

        private static MethodInfo startSandstormMethod;
        public static Action StartSandstorm = new Action(() => startSandstormMethod.Invoke(null, new object[] { }));
        private static MethodInfo stopSandstormMethod;
        public static Action StopStandstorm = new Action(() => stopSandstormMethod.Invoke(null, new object[] { }));

        public static List<int> MysteriousTraderShop;

        public override bool Autoload(ref string name)
        {
            startRainMethod = typeof(Main).GetMethod("StartRain", BindingFlags.Static | BindingFlags.NonPublic);
            stopRainMethod = typeof(Main).GetMethod("StopRain", BindingFlags.Static | BindingFlags.NonPublic);

            startSandstormMethod = typeof(Sandstorm).GetMethod("StartSandstorm", BindingFlags.Static | BindingFlags.NonPublic);
            stopSandstormMethod = typeof(Sandstorm).GetMethod("StopSandstorm", BindingFlags.Static | BindingFlags.NonPublic);
            return base.Autoload(ref name);
        }

        public override void Initialize()
        {
            base.Initialize();
            ForceFastForwardTime = false;
            MysteriousTraderShop = new List<int>();
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

            if (Main.time == 0 && Main.dayTime || MysteriousTraderShop.Count == 0) // Should happen once in the morning or if it hasn't been initialized ever
            {
                MysteriousTraderShop.Clear();
                CompiledMysteriousTraderShop.Instance.FillShop(MysteriousTraderShop);

                if (Main.netMode == NetmodeID.Server) // PostUpdate is only run on the server in mp, whee
                {
                    ModPacket packet = mod.GetPacket();
                    packet.Write(PacketID.SyncMysteriousTraderShop);
                    packet.Write((byte)MysteriousTraderShop.Count);

                    foreach (int i in MysteriousTraderShop)
                    {
                        packet.Write(i);
                    }

                    packet.Send();
                }
            }

            TravelingNPCHelper.DoTravellingMerchant(ModContent.NPCType<MysteriousTrader>(), 48600, () => (NPC.downedSlimeKing || NPC.downedBoss1) && Main.rand.NextBool(3));
        }

        public override TagCompound Save()
        {
#pragma warning disable IDE0028 // Simplify collection initialization
            TagCompound tag = new TagCompound();
#pragma warning restore IDE0028 // Simplify collection initialization

            tag.Add("MysteriousTraderCount", MysteriousTraderShop.Count);

            for (int i = 0; i < MysteriousTraderShop.Count; i++)
            {
                Item item = new Item();
                item.SetDefaults(MysteriousTraderShop[i]);
                tag.Add("ModMysteriousTraderItem" + i, item.modItem == null ? "TERRARIA" : item.modItem.mod.Name);
                tag.Add("MysteriousTraderItem" + i, MysteriousTraderShop[i]);
            }

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            MysteriousTraderShop = new List<int>();
            int count = tag.GetInt("MysteriousTraderCount");

            for (int i = 0; i < count; i++)
            {
                string key = "MysteriousTraderItem" + i;
                string itemMod = tag.Get<string>("Mod" + key);

                // Unloaded item, remove
                if (itemMod != "TERRARIA" && ModLoader.GetMod(itemMod) == null)
                    continue;

                MysteriousTraderShop.Add(tag.GetInt(key));

                tag.Remove(key);
                tag.Remove("Mod" + key);
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

            ModPacket packet = mod.GetPacket();
            packet.Write(PacketID.SyncMysteriousTraderShop);
            packet.Write((byte)MysteriousTraderShop.Count);

            foreach (int i in MysteriousTraderShop)
            {
                packet.Write(i);
            }

            packet.Send();
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
