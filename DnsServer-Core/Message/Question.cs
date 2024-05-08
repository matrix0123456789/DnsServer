namespace DnsServer_Core.Message;

public class Question
{
    public static Question Parse(NetworkBinaryReader reader)
    {
        var ret = new Question();
        ret.Name = reader.ReadStringSequence();
        var typeInt=reader.ReadUInt16();
        if (typeInt <= 16)
            ret.Type = (QType)typeInt;
        else
            ret.Type= QType.Unknown;
        
        ret.Class = reader.ReadUInt16();
        
        return ret;
    }
    public void Write(NetworkBinaryWriter writer)
    {
        writer.WriteStringSequence(Name);
        writer.Write((ushort)Type);
        writer.Write(Class);
    }

    public QType Type { get; set; }
    public ushort Class { get; set; }

    public List<string> Name { get; set; }
}