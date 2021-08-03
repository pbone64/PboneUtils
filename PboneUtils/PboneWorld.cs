using PboneUtils.DataStructures.MysteriousTrader;
using PboneUtils.Helpers;
using PboneUtils.NPCs.Town;
using PboneUtils.Packets;
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
using Terraria.WorldBuilding;

namespace PboneUtils
{
    public class PboneWorld : ModSystem
    {
        private ModWorldgenManager ModWorldGen;

        public static bool SuperFastTime;

        private static MethodInfo startRainMethod;
        public static Action StartRain = new Action(() => startRainMethod.Invoke(null, Array.Empty<object>()));
        private static MethodInfo stopRainMethod;
        public static Action StopRain = new Action(() => stopRainMethod.Invoke(null, Array.Empty<object>()));

        private static MethodInfo startSandstormMethod;
        public static Action StartSandstorm = new Action(() => startSandstormMethod.Invoke(null, Array.Empty<object>()));
        private static MethodInfo stopSandstormMethod;
        public static Action StopStandstorm = new Action(() => stopSandstormMethod.Invoke(null, Array.Empty<object>()));

        public static List<int> MysteriousTraderShop;

        public override void Load()
        {
            startRainMethod = typeof(Main).GetMethod("StartRain", BindingFlags.Static | BindingFlags.NonPublic);
            stopRainMethod = typeof(Main).GetMethod("StopRain", BindingFlags.Static | BindingFlags.NonPublic);

            startSandstormMethod = typeof(Sandstorm).GetMethod("StartSandstorm", BindingFlags.Static | BindingFlags.NonPublic);
            stopSandstormMethod = typeof(Sandstorm).GetMethod("StopSandstorm", BindingFlags.Static | BindingFlags.NonPublic);
        }

        public override void OnWorldLoad()
        {
            base.OnWorldLoad();
            SuperFastTime = false;
            MysteriousTraderShop = new List<int>();
        }

        public override void PostUpdateWorld()
        {
            base.PostUpdateWorld();
            if (SuperFastTime)
            {
                Main.fastForwardTime = true;

                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);
            }

            if (Main.time == 0 && Main.dayTime || MysteriousTraderShop.Count == 0) // Should happen once in the morning or if it hasn't been initialized ever
            {
                MysteriousTraderShop.Clear();
                CompiledMysteriousTraderShop.Instance.FillShop(MysteriousTraderShop);

                if (Main.netMode == NetmodeID.Server) // PostUpdate is only run on the server in mp, whee
                {
                    PboneUtils.PacketManager.WriteAndSendPacket<SyncMysteriousTraderShop>();
                }
            }

            TravelingNPCHelper.DoTravellingMerchant(ModContent.NPCType<MysteriousTrader>(), 48600, () => (NPC.downedSlimeKing || NPC.downedBoss1) && Main.rand.NextBool(3));
        }

        public override TagCompound SaveWorldData()
        {
#pragma warning disable IDE0028 // Simplify collection initialization
            TagCompound tag = new TagCompound();
#pragma warning restore IDE0028 // Simplify collection initialization

            tag.Add("MysteriousTraderCount", MysteriousTraderShop.Count);

            for (int i = 0; i < MysteriousTraderShop.Count; i++)
            {
                Item item = new Item();
                item.SetDefaults(MysteriousTraderShop[i]);
                tag.Add("ModMysteriousTraderItem" + i, item.ModItem == null ? "TERRARIA" : item.ModItem.Mod.Name);
                tag.Add("MysteriousTraderItem" + i, MysteriousTraderShop[i]);
            }

            return tag;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            MysteriousTraderShop = new List<int>();
            int count = tag.GetInt("MysteriousTraderCount");

            for (int i = 0; i < count; i++)
            {
                string key = "MysteriousTraderItem" + i;
                string itemMod = tag.Get<string>("Mod" + key);

                // Unloaded item, remove
                if (itemMod != "TERRARIA" && ModLoader.GetMod(itemMod) == null)
                {
                    tag.Remove(key);
                    tag.Remove("Mod" + key);
                    continue;
                }

                MysteriousTraderShop.Add(tag.GetInt(key));
            }
        }

        public override void ModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate)
        {
            base.ModifyTimeRate(ref timeRate, ref tileUpdateRate, ref eventUpdateRate);

            if (SuperFastTime)
            {
                timeRate += 2f;
                tileUpdateRate += 2f;
                eventUpdateRate += 2f;
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
            writer.Write(SuperFastTime);

            PboneUtils.PacketManager.WritePacket<SyncMysteriousTraderShop>(writer);
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
            SuperFastTime = reader.ReadBoolean();

            PboneUtils.PacketManager.ReadPacket<SyncMysteriousTraderShop>(reader);
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
            ModWorldGen = new ModWorldgenManager();

            tasks.Add(new PassLegacy("pbone's Utilities: Petrified Safes", new WorldGenLegacyMethod(ModWorldGen.GenPetrifiedSafes)));
        }
    }
}
