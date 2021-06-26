using System.IO;

namespace PboneUtils.Net.Content
{
    public class SyncMysteriousTraderShop : IPacketReader
    {
        public void ReadPacket(BinaryReader reader, int whoAmI)
        {
            byte count = reader.ReadByte();

            for (byte i = 0; i < count; i++)
            {
                PboneWorld.MysteriousTraderShop[i] = reader.ReadInt32();
            }
        }
    }
}
