namespace DnsServer_Core.Message;

public class Answer
{
    public List<string> Name { get; set; }
    public ushort Type { get; set; }
    public ushort Class { get; set; }
    public uint TTL { get; set; }
    public byte[] Data { get; set; }
    public void Write(NetworkBinaryWriter writer)
    {
        writer.WriteStringSequence(Name);
        writer.Write(Type);
        writer.Write(Class);
        writer.Write(TTL);
        writer.Write((ushort)Data.Length);
        writer.Write(Data);
    }
}