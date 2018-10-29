using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class UDP_Datagram
    {
        public byte[] udpBuffer;
        public byte[] udpPayload;
        public int SourcePort { get; private set; }
        public int DestinationPort { get; private set; }
        public int Length { get; private set; }
        public int Checksum { get; private set; }
        public int UdpHeaderLength { get; private set; }


        public UDP_Datagram(byte[] ipPayload, int protocol)
        {
            if (protocol==17)
            {
                if (ipPayload.Length >= UdpHeaderLength)
                {
                    udpBuffer = ipPayload;
                    SetSourcePort();
                    SetDestPort();
                    SetLength();
                    SetChecksum();
                    SetUdpHeaderLength();
                    GetUdpPayload();
                }
                else
                {
                    throw new Exception("from class UDP_Datagram : invalid ipPayload");
                }
            }
            else
            {
                throw new Exception("from class UDP_Datagram : invalid Protocoltype (not UDP)");
            }

        }

        private void SetUdpHeaderLength()
        {
            UdpHeaderLength = 8;
        }

        private void GetUdpPayload()
        {
            udpPayload = new byte[udpBuffer.Length - UdpHeaderLength];
            for (int i = UdpHeaderLength; i < udpBuffer.Length; i++)
            {
                udpPayload[i - UdpHeaderLength] = udpBuffer[i];
            }
        }

        private void SetChecksum()
        {
            Checksum= (udpBuffer[6] << 8) | udpBuffer[7];
        }

        private void SetLength()
        {
            Length = (udpBuffer[4] << 8) | udpBuffer[5];
        }

        private void SetDestPort()
        {
            DestinationPort = (udpBuffer[2] << 8) | udpBuffer[3];
        }

        private void SetSourcePort()
        {
            SourcePort = (udpBuffer[0] << 8) | udpBuffer[1];
        }
    }
}
