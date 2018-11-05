using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.TLS_Data.Handshake_Types
{
    public class Server_Key_Exchange
    {
        //Incoming Handshake Payload
        private readonly byte[] server_keyBuffer;

        //MainInformation
        public string AlgorithmParameters { get; set; }

        public string HexMessage { get; set; }
        public string ServerKeyMessage => //"\nParameters : " + AlgorithmParameters + 
            "\n\nHexmessage : " + HexMessage;

        public Server_Key_Exchange(byte[] handshakePayload)
        {
            server_keyBuffer = handshakePayload;
            SetHexMessage();
            //SetParams();            
        }

        //private void SetParams()
        //{
        //    throw new NotImplementedException();
        //}           

        private void SetHexMessage()
        {
            for (int i = 0; i < server_keyBuffer.Length; i++)
            {
                HexMessage = HexMessage + server_keyBuffer[i].ToString("X2") + " ";
            }
        }
    }
}
