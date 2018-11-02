using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packets.TLS_Data.Handshake_Types;

namespace Packets.TLS_Data
{
    public class TLS_Handshake
    {
        public Client_Hello Client_Hello;

        //Incoming TLS_Record Payload
        private readonly byte[] handshakeBuffer;

        //Outgoing TLS_Handshake Payload
        public byte[] handshakePayload;

        //MainInformation
        public string Type { get; set; }
        public int Length { get; set; }
        public int HeaderLength => 4;

        public TLS_Handshake(byte[] tlsPayload)
        {
            handshakeBuffer = tlsPayload;
            SetType();
            SetLength();
            SetPayload();
            if (Type == "CLIENT_HELLO")
            {
                Client_Hello = new Client_Hello(handshakePayload);
            }
        }

        private void SetPayload()
        {
            handshakePayload = new byte[handshakeBuffer.Length - HeaderLength];
            for (int i = HeaderLength; i < handshakeBuffer.Length; i++)
            {
                handshakePayload[i - HeaderLength] = handshakeBuffer[i]; 
            }
        }

        private void SetLength()
        {
            Length = handshakeBuffer[1] << 16 | handshakeBuffer[2] << 8 | handshakeBuffer[3];
        }

        private void SetType()
        {
            switch (handshakeBuffer[0])
            {
                case 0:
                    Type = "HELLO_REQUEST";
                    break;
                case 1:
                    Type = "CLIENT_HELLO";
                    break;
                case 2:
                    Type = "SERVER_HELLO";
                    break;
                case 11:
                    Type = "CERTIFICATE";
                    break;
                case 12:
                    Type = "SERVER_KEY_EXCHANGE";
                    break;
                case 13:
                    Type = "CERTIFICATE_REQUEST";
                    break;
                case 14:
                    Type = "SERVER_DONE";
                    break;
                case 15:
                    Type = "CERTIFICATE VERIFY";
                    break;
                case 16:
                    Type = "CLIENT_KEY_EXCHANGE";
                    break;
                case 20:
                    Type = "FINISHED";
                    break;
            }

        }
    }
}
