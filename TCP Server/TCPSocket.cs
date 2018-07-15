using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace TCP_Server
{
    class TCPSocket
    {

        // Incoming data from the client.  
        public static string data = null;

        public string StartListening(int Port, bool ConsoleEnable)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];
            string ReceivedMsg = "";

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {   
                    // Write to console
                    if (ConsoleEnable)
                    {
                        Console.WriteLine("Waiting for a connection...");
                    }

                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;
                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("}") > -1)
                        {
                            break;
                        }
                    }

                    // Write to console
                    if (ConsoleEnable)
                    {
                        // Show the data on the console.  
                        Console.WriteLine("Text received : {0}", data);
                    }

                    ReceivedMsg = data;

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Write to console
            if (ConsoleEnable)
            {
                Console.WriteLine("\nPress ENTER to continue...");
                Console.Read();
            }

            return ReceivedMsg;
        }
    }
}
