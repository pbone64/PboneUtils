using System.IO;

namespace PboneUtils.Net
{
    public interface IPacketReader
    {
        void ReadPacket(BinaryReader reader, int whoAmI);
    }
}
