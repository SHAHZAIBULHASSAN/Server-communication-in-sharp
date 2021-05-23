using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace network
{
    class Program
    {
        static void Main(string[] args)

        {
            var sw = new Stopwatch();
            sw.Start();
            Task.WaitAll(f1(), f2(), f3());
           sw.Stop();

            var elapsed = sw.ElapsedMilliseconds;
            Console.WriteLine($"elapsed: {elapsed} ms");

            async Task f1()
            {
                await Task.Delay(4000);
                Console.WriteLine("f1 finished");
            }

            async Task f2()
            {
                await Task.Delay(7000);
                Console.WriteLine("f2 finished");
            }

            async Task f3()
            {
                await Task.Delay(2000);
                Console.WriteLine("f3 finished");
            }
            UdpClient udpClient = new UdpClient("webcode.me", 7);
            Byte[] data = Encoding.ASCII.GetBytes("Hello there");
            udpClient.Send(data, data.Length);

            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Byte[] received = udpClient.Receive(ref RemoteIpEndPoint);
            string output = Encoding.ASCII.GetString(received);

            Console.WriteLine(output);

            udpClient.Close();


            string server = "webcode.me";
            int port = 80;

            var request = $"GET / HTTP/1.1\r\nHost: {server}\r\nConnection: Close\r\n\r\n";

            Byte[] requestBytes = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];

            IPHostEntry hostEntry = Dns.GetHostEntry(server);

            var ipe = new IPEndPoint(hostEntry.AddressList[0], port);
            using var socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipe);

            if (socket.Connected)
            {
                Console.WriteLine("Connection established");
            }
            else
            {
                Console.WriteLine("Connection failed");
                return;
            }

            socket.Send(requestBytes, requestBytes.Length, 0);

            int bytes = 0;
            var sb = new StringBuilder();

            do
            {

                bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                sb.Append(Encoding.ASCII.GetString(bytesReceived, 0, bytes));
            } 
            while (bytes > 0);

            Console.WriteLine(sb.ToString());



            var name = "google.com";
            var Host = Dns.GetHostEntry(name);


            var h = Host.AddressList;


            foreach (var o in h)
            {
                Console.WriteLine("Hello World!  {0}", o);
            }
//            #region ping
//            using var ping = new Ping();
//            PingReply reply = ping.Send("172.217.18.142", 100);
//            if (reply.Status == IPStatus.Success)
//            {
//                var msg = @$"Status: {reply.Status}
//IP Address:{reply.Address}
//Time:{reply.RoundtripTime}ms";
//                Console.WriteLine(msg);
//                // var ms=$"status{reply.Buffer} Ip addree"
//            }
//            else
//            {
//                Console.WriteLine($@" reply status{reply.Status}  Ip address{reply.RoundtripTime}");


//            }
//            #endregion
        }
    }
}
