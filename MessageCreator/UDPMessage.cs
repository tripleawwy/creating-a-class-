using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace MessageCreator
{
    public class UDPMessage
    {
        private UDP_Datagram UDP_Datagram;

        private string SourcePort { get; set; }
        private string DestinationPort { get; set; }
        private string Checksum { get; set; }
        private string Length { get; set; }

        public string ImportantMessage { get; set; }
        public string UdpData { get; set; }


        public UDPMessage(IPv4_Packet ipv4packet)
        {
            UDP_Datagram = ipv4packet.UDP_Datagram;
            if (UDP_Datagram != null)
            {
                SetSourcePort();
                SetDestPort();
                SetChecksum();
                SetLength();
                SetUdpData();
                SetImportantMessage();
            }
            else
            {
                ImportantMessage = "UDP_Datagram not existant";
            }
        }



        private void SetUdpData()
        {
            if (UDP_Datagram.udpPayload != null && UDP_Datagram.udpPayload.Length > 0)
            {
                UdpData = Encoding.ASCII.GetString(UDP_Datagram.udpPayload);
            }
        }

        private void SetImportantMessage()
        {
            ImportantMessage = SourcePort + DestinationPort + Length;
        }

        private void SetLength()
        {
            Length = "\t Length : " + UDP_Datagram.Length + "\t Byte";
        }

        private void SetChecksum()
        {
            Checksum = "\t Checksum : " + UDP_Datagram.Checksum;
        }

        private void SetDestPort()
        {
            DestinationPort = "\t Destination Port : " + UDP_Datagram.DestinationPort;
        }

        private void SetSourcePort()
        {
            SourcePort = "Source Port : " + UDP_Datagram.SourcePort;
        }
    }
}
