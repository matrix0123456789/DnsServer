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

    public void Write(NetworkBinaryWriter writer)
    {
        writer.Write(ID);
        var flags = (ushort) (IsResponse ? 0x8000 : 0);
        flags |= (ushort) (OPcode << 11);
        flags |= (ushort) (AuthoritativeAnswer ? 0x0400 : 0);
        flags |= (ushort) (TrunCation ? 0x0200 : 0);
        flags |= (ushort) (RecursionDesired ? 0x0100 : 0);
        flags |= (ushort) (RecursionAvailable ? 0x0080 : 0);
        flags |= (ushort) (ResponseCode & 0x000F);
        writer.Write(flags);
        writer.Write((ushort) Questions.Count);
        writer.Write((ushort) Answers.Count);
        //writer.Write((ushort) AuthorityRecords.Count);
        //writer.Write((ushort) AdditionalRecords.Count);
        writer.Write((ushort) 0);
        writer.Write((ushort) 0);
        foreach (var question in Questions)
        {
            question.Write(writer);
        }
        foreach (var answer in Answers)
        {
            answer.Write(writer);
        }
       // foreach (var authorityRecord in AuthorityRecords)
       // {
       //     authorityRecord.Write(writer);
       // }
       // foreach (var additionalRecord in AdditionalRecords)
       // {
       //     additionalRecord.Write(writer);
       // }
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
    public List<Answer> Answers { get; set; } = new();
}