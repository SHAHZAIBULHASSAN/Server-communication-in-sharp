using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace pollingserver
{
    class Program
    { static bool result;
        static int i = 0; static int recv;
        static byte[] data = new byte[1024];
        static Socket newsock;
        static Socket client;
        static IPEndPoint newclient;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            newsock = new
                Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),
                                   9050);



            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("Waiting for a client...");

            Thread thread = new Thread(new ThreadStart(Function));
            thread.Start();





            client = newsock.Accept();

            newclient =
                         (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}",
                            newclient.Address, newclient.Port);

            string welcome = "Welcome to my test server";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length,
                              SocketFlags.None);

            client.Close();
            newsock.Close();

        }

        private static void Function()
        {
            while (true)
            {
                i++;
                Console.WriteLine("polling for accept#{0}...", i);

                result = newsock.Poll(10, SelectMode.SelectRead);

                //  result = newsock.Poll(133440, SelectMode.SelectError);
                //  result = newsock.Poll(123440, SelectMode.SelectWrite);
                if (result)
                {
                    //    Console.WriteLine("Disconnected from {0}",
                    //                  newclient.Address);
                    //    client.Close();
                    //    newsock.Close();
                    //    //client.Dispose();
                    break;
                }

            }

            i = 0;
            while (true)
            {
                Console.WriteLine("polling for receive #{0}...", i);
                i++;
                result = client.Poll(30000, SelectMode.SelectRead);
                if (result)
                {
                    data = new byte[1024];
                    i = 0;
                    recv = client.Receive(data);
                    if (recv == 0)
                        break;

                    Console.WriteLine(
                          Encoding.ASCII.GetString(data, 0, recv));
                    client.Send(data, recv, 0);
                }

            
        }

        Console.WriteLine("Disconnected from {0}",
                                  newclient.Address);
            client.Close();
            newsock.Close();
}
        
    }
    }

