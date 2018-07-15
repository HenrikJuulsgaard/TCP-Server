using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TCP_Server
{
    class SaveMessage
    {
        private readonly string DirLog = @"c:\\Baader\\Log\\";
        private readonly string DirImport = @"c:\\Baader\\Import\\";
        public SaveMessage()
        {
            Directory.CreateDirectory(DirLog);
            Directory.CreateDirectory(DirImport);
        }

        // Save message to file
        public void SaveMsg(string Msg, string FileName)
        {
            

                string MsgFileName = DirLog + "Log.txt";
                string LotNo = "";
                string XMLtxt = ";";
                string Time = DateTime.Now.ToString() + ": ";
            try
            { 

                if (Msg.Contains('{'))
                {
                    // If message is ok, write to log and build the XML file
                    File.AppendAllText(MsgFileName, Time + Msg);
                    LotNo = GetLotNo(Msg, @"Lot#", ":");
                    XMLtxt = CreateXML(LotNo);
                    File.WriteAllText(DirImport + FileName, XMLtxt);
                }
                else
                {
                    // If message has wrong format write error message
                    File.AppendAllText(MsgFileName, Time + " Error in the message, could not build ");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        // Method returns lot number
        public string GetLotNo(string strSource, string strStart, string strEnd)
        {
            // use for checking string before entering
            const int kNotFound = -1;

            var startIdx = strSource.IndexOf(strStart);
            if (startIdx != kNotFound)
            {
                startIdx += strStart.Length;
                var endIdx = strSource.IndexOf(strEnd, startIdx);
                if (endIdx > startIdx)
                {
                    return strSource.Substring(startIdx, endIdx - startIdx);
                }
            }
            return String.Empty;
        }

        // Create XML string to write
        private string CreateXML(string LotNo)
        {
            string startXML = " indsæt start af XML filen";
            string endXML = " indsæt slutningen af XML filen";

            return startXML + LotNo + endXML;
        }
    }
}
