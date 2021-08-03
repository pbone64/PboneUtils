using PboneLib.Services.Net;
using System.Collections.Generic;
using System.IO;

namespace PboneUtils.Packets
{
    public class SyncMysteriousTraderShop : IPacketHandler
    {
        public void ReadPacket(BinaryReader reader)
        {
            PboneWorld.MysteriousTraderShop = new List<int>();
            byte count = reader.ReadByte();

            for (byte i = 0; i < count; i++)
            {
                PboneWorld.MysteriousTraderShop.Add(reader.ReadInt32());
            }
        }

        public void WritePacket(BinaryWriter writer)
        {
            writer.Write((byte)PboneWorld.MysteriousTraderShop.Count);

            foreach (int i in PboneWorld.MysteriousTraderShop)
            {
                writer.Write(i);
            }
        }
    }
}
