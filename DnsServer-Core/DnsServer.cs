using System.Net;
using System.Net.Sockets;

namespace DnsServer_Core;

public class DnsServer
{
    private readonly IPEndPoint endpoint;

    public DnsServer(IPEndPoint endpoint)
    {
        this.endpoint= endpoint;
    }

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
        }
    }
}