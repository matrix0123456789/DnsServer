// See https://aka.ms/new-console-template for more information

using System.Net;
using DnsServer_Core;
using DnsServer_Core.Message;

Console.WriteLine("Hello, World!");

var server = new DnsServer(IPEndPoint.Parse("194.182.72.177:53"), q =>
{
    Console.WriteLine(q.Type + " " + (String.Join(".", q.Name)));
    if (String.Join(".", q.Name).ToLower() == "dnstest.green-code.studio")
    {
        if (q.Type == QType.A)
            return new Answer() { Data = new byte[] { 1, 2, 3, 4 } };
        else if(q.Type == QType.TXT)
            return new Answer() { Data = new List<string> { "Hello", "World" }};
        else if(q.Type == QType.NS)
            return new Answer() { Data = new List<string> { "dnstest", "green-code", "studio"}};
    }else if (String.Join(".", q.Name).ToLower() == "abcd.dnstest.green-code.studio")
    {
        if (q.Type == QType.A)
            return new Answer() { Data = new byte[] {99,99,99,99 } };
    }else if (String.Join(".", q.Name).ToLower() == "google.dnstest.green-code.studio")
    {
        if (q.Type == QType.A)
            return new Answer() { Data = new byte[] {216,58,209,46 } };
    }

    return null;
});

server.Listen();