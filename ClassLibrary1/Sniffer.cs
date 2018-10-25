using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace NetworkSniffer
{
    public class Sniffer   
    {
        public IPv4_Packet ipv4Packet;
        public TCP_Segment tcpSegment;
        public UDP_Datagram udpDatagram;

               
        public Sniffer(byte[] ethernetPayload)
        {           
           
        }

        
        //public Sniffer(byte[] ethernetPayload)
        //{
        //    CompletePacket(ethernetPayload);           
        //}
        //public void CompletePacket(byte[] ethernetPayload)
        //{
        //    IPv4_Packet pv4_Packet = new IPv4_Packet(ethernetPayload);
        //    if (pv4_Packet.Protocol==6)
        //    {
        //        TCP_Segment tCP_Segment = new TCP_Segment(pv4_Packet);
        //    }
        //    else if (pv4_Packet.Protocol==17)
        //    {
        //        UDP_Datagram uDP_Datagram = new UDP_Datagram(pv4_Packet);
        //    }
        //}
    }
}
