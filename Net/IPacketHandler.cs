using System.IO;

namespace PboneUtils.Net
{
    public interface IPacketHandler
    {
        void ReadPacket(BinaryReader reader, int whoAmI);

        void Write(BinaryWriter writer);
    }
}
