using PboneUtils.ID;
using PboneUtils.Net.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader;

namespace PboneUtils.Net
{
    public class ModPacketManager
    {
        public Mod Mod;
        public Dictionary<byte, IPacketHandler> Handlers = new Dictionary<byte, IPacketHandler>();

        public ModPacketManager(Mod mod)
        {
            Mod = mod;
        }

        public void Load()
        {
            // Make sure the order here lines up with PacketID
            Handlers.Add(PacketID.SyncMysteriousTraderShop, new SyncMysteriousTraderShop());
        }

        public void WritePacket(BinaryWriter writer, byte packetId)
        {
            writer.Write(packetId);
            Handlers[packetId].Write(writer);
        }

        public void ReadPacket(BinaryReader reader, int whoAmI)
        {
            byte type = reader.ReadByte();
            Handlers[type].ReadPacket(reader, whoAmI);
        }
    }
}
