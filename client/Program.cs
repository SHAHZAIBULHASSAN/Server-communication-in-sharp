using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client
{/// <summary>
/// code of client
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {




            #region comment
            Socket client;
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint a = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1233);
            client.Connect(a);
            Console.WriteLine("enter message:");
            string stre = Console.ReadLine();

            //  
            // byte[] Buffer = Encoding.Default.GetBytes(stre);
            // client.Send(Buffer, 0, Buffer.Length, 0);

            //byte [] buffer = new byte[1243];

            // //Console.WriteLine("welcome to cleint");
            // int u=  client.Receive(Buffer, 0, Buffer.Length, 0);
            // Array.Resize(ref buffer, u);
            //    string str = Encoding.Default.GetString(Buffer);
            //     Console.WriteLine($"welcome  to  :{str}");

            // client.Close();

            //a.Close();

            #endregion





        }
    }
}
