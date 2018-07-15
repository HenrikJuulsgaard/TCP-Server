using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Server
{
   
    class Program
    {
        static void Main(string[] args)
        {
            string ReceivedMsg = "";
            TCPSocket Socket = new TCPSocket();
            SaveMessage saveMessage = new SaveMessage();

            while (true)
            {
                ReceivedMsg = Socket.StartListening(1000,true);
                if (ReceivedMsg != "")
                {
                    saveMessage.SaveMsg(ReceivedMsg, "XMLimport.XML");
                }
            }
        }
    }
}
