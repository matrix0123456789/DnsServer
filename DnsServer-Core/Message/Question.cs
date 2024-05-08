namespace DnsServer_Core.Message;

public class Question
{
    public static Question Parse(NetworkBinaryReader reader)
    {
        var ret = new Question();
        ret.Name = reader.ReadStringSequence();
        ret.Type = reader.ReadUInt16();
        ret.Class = reader.ReadUInt16();
        
        return ret;
    }
    public void Write(NetworkBinaryWriter writer)
    {
        writer.WriteStringSequence(Name);
        writer.Write(Type);
        writer.Write(Class);
    }

    public ushort Type { get; set; }
    public ushort Class { get; set; }

    public List<string> Name { get; set; }
}