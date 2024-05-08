// See https://aka.ms/new-console-template for more information

using System.Net;
using DnsServer_Core;

Console.WriteLine("Hello, World!");

var server = new DnsServer(IPEndPoint.Parse("127.0.0.2:53"));
server.Listen();