using System.Text;

namespace DnsServer_Core;

public class NetworkBinaryReader : BinaryReader
{
    public NetworkBinaryReader(Stream input) : base(input)
    {
    }
    public override ushort  ReadUInt16()
    {
        var bytes= base.ReadBytes(2);
        Array.Reverse(bytes);
        return BitConverter.ToUInt16(bytes, 0);
    }

    public List<string> ReadStringSequence()
    {
        var ret = new List<string>();
        while (true)
        {
            var length = this.ReadByte();
            if (length == 0)
            {
                break;
            }
            var bytes = this.ReadBytes(length);
            ret.Add(ASCIIEncoding.ASCII.GetString(bytes));
        }
        return ret;
    }
}