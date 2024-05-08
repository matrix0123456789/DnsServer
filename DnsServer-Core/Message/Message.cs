using System.Runtime.CompilerServices;

namespace DnsServer_Core.Message;

public class Message
{
    public Message()
    {
    }

    public static Message Parse(NetworkBinaryReader reader)
    {
        var ret= new Message();
ret.ID=reader.ReadUInt16();
var flags=reader.ReadUInt16();
ret.IsResponse=(flags&0x8000)!=0;
ret.OPcode=(flags&0x7800)>>11;
ret.AuthoritativeAnswer=(flags&0x0400)!=0;
ret.TrunCation =(flags&0x0200)!=0;
ret.RecursionDesired=(flags&0x0100)!=0;
ret.RecursionAvailable=(flags&0x0080)!=0;
ret.ResponseCode=(flags&0x000F);

var questionCount=reader.ReadUInt16();
var answerCount=reader.ReadUInt16();
var authorityRecordsCount=reader.ReadUInt16();
var additionalRecordsCount=reader.ReadUInt16();

for (var i = 0; i < questionCount; i++)
{
    ret.Questions.Add(Question.Parse(reader));
}
for (var i = 0; i < answerCount; i++)
{
    
}
        return ret;
    }

    public int ResponseCode { get; set; }

    public bool RecursionAvailable { get; set; }

    public bool RecursionDesired { get; set; }

    public bool TrunCation { get; set; }

    public bool AuthoritativeAnswer { get; set; }

    public int OPcode { get; set; }

    public bool IsResponse { get; set; }

    public ushort ID { get; set; }
    public List<Question> Questions { get; set; } = new();
}