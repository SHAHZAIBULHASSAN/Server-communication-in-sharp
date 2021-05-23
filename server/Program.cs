using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server
{
    class Program
    {/// <summary>
     /// code of server
     /// </summary>
     /// <param name="args"></param>
        static void Main(string[] args)

        {



            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                handleClinet client = new handleClinet();
                client.startClient(clientSocket, Convert.ToString(counter));
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();
        }
    }

    //Class to handle each client request separatly
    public class handleClinet
    {
        TcpClient clientSocket;
        string clNo;
        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }
        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> " + "From client-" + clNo + dataFromClient);

                    rCount = Convert.ToString(requestCount);
                    serverResponse = "Server to clinet(" + clNo + ") " + rCount;
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }






        #region comment
        //   Socket  list;
        ////   sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //   list = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //   list.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1233));
        //   list.Listen(0);
        //   Socket sk = list.Accept();
        ////   byte[] buffer = new byte[2345];
        //  // = Encoding.Default.GetBytes("Hello from server");
        // //   sk.Send(buffer, 0, buffer.Length, 0);
        //   // buffer = new byte[1234];
        //   // sck.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1233));
        //  byte[]  buffer = new byte[1238];
        //   int y = sk.Receive(buffer, 0, buffer.Length, 0);
        //   Array.Resize(ref buffer, y);



        //       Console.WriteLine("received {0}",Encoding.ASCII.GetString(buffer));
        //   Console.WriteLine("enter message from server:");
        //   string stre = Console.ReadLine();
        //   byte[] Buffer = new byte[1238];
        //   //  
        //   Buffer = Encoding.Default.GetBytes(stre);
        //   sk.Send(Buffer, 0, Buffer.Length, 0);
        ////   byte[] Buffer = new byte[2345];
        //   // = Encoding.Default.GetBytes("Hello from server");
        //   //   sk.Send(buffer, 0, buffer.Length, 0);
        //   // buffer = new byte[1234];
        //   list.Close();
        //   sk.Close();
        //   Console.Read();


        //    Console.WriteLine("Connected");




        // Console.WriteLine("Hello World!");
        #endregion
    }
}
    