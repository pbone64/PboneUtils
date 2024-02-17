using PboneLib.CustomLoading.Content.Implementations.Misc;
using PboneUtils.DataStructures.MysteriousTrader;
using PboneUtils.Helpers;
using PboneUtils.NPCs.Town;
using PboneUtils.Packets;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace PboneUtils
{
    public class PboneWorld : PSystem
    {
        private ModWorldgenManager ModWorldGen;

        public static bool SuperFastTime;

        public static List<int> MysteriousTraderShop;

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
                Main.fastForwardTimeToDawn = true;

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
            if (ModContent.GetInstance<PboneUtilsConfig>().MysteriousTrader)
                TravelingNPCHelper.DoTravellingMerchant(ModContent.NPCType<MysteriousTrader>(), 48600, () => (NPC.downedSlimeKing || NPC.downedBoss1) && Main.rand.NextBool(3) && !NPC.AnyNPCs(ModContent.NPCType<MysteriousTrader>()));
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag.Add("MysteriousTraderCount", MysteriousTraderShop.Count);

            for (int i = 0; i < MysteriousTraderShop.Count; i++)
            {
                Item item = new Item();
                item.SetDefaults(MysteriousTraderShop[i]);
                tag.Add("ModMysteriousTraderItem" + i, item.ModItem == null ? "TERRARIA" : item.ModItem.Mod.Name);
                tag.Add("MysteriousTraderItem" + i, MysteriousTraderShop[i]);
            }
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

        public override void PreWorldGen()
        {
            base.PreWorldGen();
            MysteriousTraderShop = new List<int>();
        }

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
            ModWorldGen = new ModWorldgenManager();

            //tasks.Add(new PassLegacy("pbone's Utilities: Petrified Safes", new WorldGenLegacyMethod(ModWorldGen.GenPetrifiedSafes)));
        }
    }
}
