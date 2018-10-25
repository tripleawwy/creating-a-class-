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
        //public TCP_Segment tcpSegment;
        //public UDP_Datagram udpDatagram;

               
        public Sniffer(byte[] ethernetPayload)
        {
            ipv4Packet = new IPv4_Packet(ethernetPayload);
            //CreateCompletePacket(ethernetPayload);
            
        }
        
        //private void CreateCompletePacket(byte[] ethernetPayload)
        //{
        //    ipv4Packet = new IPv4_Packet(ethernetPayload);
        //    if (ipv4Packet.Protocol == 6)
        //    {
        //        tcpSegment = new TCP_Segment(ipv4Packet.ipPayload, ipv4Packet.Protocol);
        //    }
        //    else if (ipv4Packet.Protocol == 17)
        //    {
        //        udpDatagram = new UDP_Datagram(ipv4Packet.ipPayload, ipv4Packet.Protocol);
        //    }
        //}
    }
}
