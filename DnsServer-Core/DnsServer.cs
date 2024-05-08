using System.Net;
using System.Net.Sockets;
using DnsServer_Core.Message;

namespace DnsServer_Core;

public class DnsServer
{
    private readonly IPEndPoint endpoint;

    public DnsServer(IPEndPoint endpoint, Func< Question, Answer> callback)
    {
        this.endpoint= endpoint;
        this.callback= callback;
    }

    public Func<Question, Answer> callback { get; set; }

    public void Listen()
    {
        var udpListener = new UdpClient(endpoint);
        while (true)
        {
            IPEndPoint remoteEndpoint=null;
            var data=udpListener.Receive(ref remoteEndpoint);
            var stream=new MemoryStream(data);
            var reader=new NetworkBinaryReader(stream);
            var message=DnsServer_Core.Message.Message.Parse(reader);

            var responseMessage = new Message.Message();
            responseMessage.ID = message.ID;
            responseMessage.IsResponse = true;
            responseMessage.Questions= message.Questions;
            foreach (var x in responseMessage.Questions)
            {
                var answer = callback(x);
                if(answer!=null)
                responseMessage.Answers.Add(answer);
            }
            var responseStream=new MemoryStream();
            var writer=new NetworkBinaryWriter(responseStream);
            responseMessage.Write(writer);
            udpListener.Send(responseStream.ToArray(), remoteEndpoint);
        }
    }
}