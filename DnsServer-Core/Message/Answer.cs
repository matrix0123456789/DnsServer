namespace DnsServer_Core.Message;


public class Answer
{
    public List<string> Name { get; set; }
    public QType Type { get; set; }
    public ushort Class { get; set; }
    public uint TTL { get; set; }
    public Object Data { get; set; }
    public void Write(NetworkBinaryWriter writer)
    {
        writer.WriteStringSequence(Name);
        writer.Write((ushort)Type);
        writer.Write(Class);
        writer.Write(TTL);
        if(Data.GetType() == typeof(List<string>))
        {
            var data = Data as List<string>;
            var tmpStream=new MemoryStream();
            var tmpWriter = new NetworkBinaryWriter(tmpStream);
            tmpWriter.WriteStringSequence(data);
            var bytes = tmpStream.ToArray();
            writer.Write((ushort)bytes.Length);
            writer.Write(bytes, 0, bytes.Length);
        }else if (Data.GetType() == typeof(byte[]))
        {
            var data = Data as byte[];
            writer.Write((ushort)data.Length);
            writer.Write(data);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}