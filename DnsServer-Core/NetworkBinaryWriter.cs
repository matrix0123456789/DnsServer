using System.Text;

namespace DnsServer_Core.Message;

public class NetworkBinaryWriter:BinaryWriter
{
    public NetworkBinaryWriter(Stream input) : base(input)
    {
    }

    public void WriteStringSequence(List<string> sequence)
    {
        foreach (var x in sequence)
        {
            Write((byte)x.Length);
            var bytes = ASCIIEncoding.ASCII.GetBytes(x);
            Write(bytes, 0, bytes.Length);
        }
        Write((byte)0);
    }
    public override void Write(ushort value)
    {
        var bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);
        Write(bytes);
    }
    public override void Write(uint value)
    {
        var bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);
        Write(bytes);
    }
}