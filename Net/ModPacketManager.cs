using PboneUtils.ID;
using PboneUtils.Net.Content;
using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader;

namespace PboneUtils.Net
{
    public class ModPacketManager
    {
        public Mod Mod;
        public Dictionary<byte, IPacketReader> Readers = new Dictionary<byte, IPacketReader>();

        public ModPacketManager(Mod mod)
        {
            Mod = mod;
        }

        public void Load()
        {
            // Make sure the order here lines up with PacketID
            Readers.Add(PacketID.SyncMysteriousTraderShop, new SyncMysteriousTraderShop());
        }

        public void ReadPacket(BinaryReader reader, int whoAmI)
        {
            byte type = reader.ReadByte();
            Readers[type].ReadPacket(reader, whoAmI);
        }
    }
}
