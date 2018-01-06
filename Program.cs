
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace webData
{
    class Program
    {
        static void Main(string[] args)
        {
            string url;
            string request = "/";
            int port = 80;
            if(args.Length > 0){
                url = args[0];
                if(args.Length > 1)
                    port = Int16.Parse(args[1]);
                if(args.Length > 2)
                    request = args[2];
            }
            else
                throw new ArgumentException ("URL required");
            //
            //
            string urlIp    = Dns.GetHostEntry(url).AddressList[0].ToString();
            TcpClient TC    = new TcpClient();
            IPAddress ip    = IPAddress.Parse(urlIp);
            TC.Connect(ip, port);
            StreamReader SR = new StreamReader(TC.GetStream());
            StreamWriter SW = new StreamWriter(TC.GetStream());
            SW.Write($"GET {request} HTTP/1.1\r\nHost:{url}\r\n\r\n");
            SW.Flush();
            while(TC.Connected)
            {
                string raw = SR.ReadLine();
                if(!string.IsNullOrEmpty(raw))
                    Console.WriteLine(raw);
            }
        }
    }
}
