using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packets;
using MessageCreator;

namespace NetworkSniffer
{
    public class Sniffer   
    {
        public IPv4_Packet IPv4Packet;
        private IPv4Message IPv4Message;
        private TCPMessage TCPMessage;
        private UDPMessage UDPMessage;
        private ICMPMessage ICMPMessage;
        public TLSMessage TLSMessage;   

        private string PacketContent;
        public string TlsContent;


        public Sniffer(byte[] ethernetPayload)
        {
            IPv4Packet = new IPv4_Packet(ethernetPayload);
            //CreateIcmpMessage();
            //CreateTcpMessage();
            //CreateUdpMessage();
            ShowPacketContent();
            ShowTlsContent();
        }

        private void ShowTlsContent()
        {
            if (IPv4Packet != null)
            {
                if (IPv4Packet.TCP_Segment != null)
                {
                    if (IPv4Packet.TCP_Segment.TLS_Record !=null)
                    {
                        TLSMessage = new TLSMessage(IPv4Packet.TCP_Segment.TLS_Record);
                        TlsContent = TLSMessage.ImportantTlsMessage;
                    }
                }
            }
        }

        public string ShowPacketContent()
        {
            if (IPv4Packet != null)
            {
                IPv4Message = new IPv4Message(IPv4Packet);
                PacketContent = "\n\nIPv4 Content \n\n"+ IPv4Message.ImportantMessage;
                if (IPv4Packet.ICMP_Segment!=null)
                {
                    ICMPMessage = new ICMPMessage(IPv4Packet);
                    PacketContent = PacketContent + "\n\n\n\t\t ICMP Content \n\n\t\t " + ICMPMessage.ImportantMessage;
                }
                else if (IPv4Packet.TCP_Segment!=null)
                {
                    TCPMessage = new TCPMessage(IPv4Packet);
                    PacketContent = PacketContent + "\n\n\n\t\t TCP Content \n\n\t\t " + TCPMessage.ImportantMessage + "\n\n\n\t\t TCP Data \n\n\t\t " + TCPMessage.TcpData;
                }
                else if (IPv4Packet.UDP_Datagram != null)
                {
                    UDPMessage = new UDPMessage(IPv4Packet);
                    PacketContent = PacketContent + "\n\n\n\t\t UDP Content \n\n\t\t " + UDPMessage.ImportantMessage + "\n\n\n\t\t UDP Data \n\n\t\t " + UDPMessage.UdpData;
                }
            }
            return PacketContent;
        }

        //private void CreateTcpMessage()
        //{
        //    if (IPv4Packet != null)
        //    {
        //        IPv4Message = new IPv4Message(IPv4Packet);
        //        if (IPv4Packet.Protocol == 6)
        //        {
        //            TCPMessage = new TCPMessage(IPv4Packet);
        //        }
        //    }
        //}

        //private void CreateUdpMessage()
        //{
        //    if (IPv4Packet != null)
        //    {
        //        IPv4Message = new IPv4Message(IPv4Packet);
        //        if (IPv4Packet.Protocol == 17)
        //        {
        //            UDPMessage = new UDPMessage(IPv4Packet);
        //        }
        //    }
        //}

        //private void CreateIcmpMessage()
        //{
        //    if (IPv4Packet != null)
        //    {
        //        IPv4Message = new IPv4Message(IPv4Packet);
        //        if (IPv4Packet.Protocol == 1)
        //        {
        //            ICMPMessage = new ICMPMessage(IPv4Packet);
        //        }
        //    }
        //}
    }
}
