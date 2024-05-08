// See https://aka.ms/new-console-template for more information

using System.Net;
using DnsServer_Core;
using DnsServer_Core.Message;

Console.WriteLine("Hello, World!");

var server = new DnsServer(IPEndPoint.Parse("127.0.0.2:53"), q =>
{
    Console.WriteLine(q.Type + " " + (String.Join(".", q.Name)));
    if (String.Join(".", q.Name) == "test.pl")
    {
        if (q.Type == QType.A)
            return new Answer() { Data = new byte[] { 1, 2, 3, 4 } };
        else if(q.Type == QType.TXT)
            return new Answer() { Data = new List<string> { "Hello", "World" }};
        else if(q.Type == QType.NS)
            return new Answer() { Data = new List<string> { "test2", "pl" }};
    }
    else if (String.Join(".", q.Name) == "test.com")
    {
    }

    return null;
});

server.Listen();