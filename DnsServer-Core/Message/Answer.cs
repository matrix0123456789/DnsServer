namespace DnsServer_Core.Message;

public class Answer
{
    public List<string> Name { get; set; }
    public QType Type { get; set; }
    public ushort Class { get; set; }
    public uint TTL { get; set; }
    public byte[] Data { get; set; } = new byte[0];
    public void Write(NetworkBinaryWriter writer)
    {
        writer.WriteStringSequence(Name);
        writer.Write((ushort)Type);
        writer.Write(Class);
        writer.Write(TTL);
        writer.Write((ushort)Data.Length);
        writer.Write(Data);
    }
}