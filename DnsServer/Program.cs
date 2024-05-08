// See https://aka.ms/new-console-template for more information

using System.Net;
using DnsServer_Core;
using DnsServer_Core.Message;

Console.WriteLine("Hello, World!");

var server = new DnsServer(IPEndPoint.Parse("127.0.0.2:53"), q =>
{
    Console.WriteLine(q.Type + " " + (String.Join(".", q.Name)));
    var ans = new Answer();
    ans.Name = q.Name;
    ans.Type = q.Type;
    if (String.Join(".", q.Name) == "test.pl")
    {
        ans.Data = new byte[] { 1, 2, 3, 4 };
    }
    else if (String.Join(".", q.Name) == "test.com")
    {
        ans.Data = new byte[] { 127, 0, 0, 1 };
    }
    else
    {
        return null;
    }

    return ans;
});

server.Listen();