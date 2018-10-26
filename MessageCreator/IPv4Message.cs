using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace MessageCreator
{
    public class IPv4Message
    {
        private readonly IPv4_Packet IPv4Packet;

        private string Version { get; set; }
        private string HeaderLength { get; set; }
        private string TotalPacketLength { get; set; }
        private string TTL { get; set; }
        private string Protocol { get; set; }
        private string SourceIpAdress { get; set; }
        private string DestIpAdress { get; set; }

        public string ImportantMessage { get; set; }


        public IPv4Message(IPv4_Packet ipv4Packet)
        {
            IPv4Packet = ipv4Packet;
            if (ipv4Packet!=null)
            {
                SetVersion();
                SetheaderLength();
                SetTotalLength();
                SetTTL();
                Setproto();
                SetSource();
                SetDest();
                SetImportantMessage();
            }
            else
            {
                ImportantMessage = "Ipv4_Packet is not existant";
            }
        }

        private void SetImportantMessage()
        {
            ImportantMessage = SourceIpAdress + DestIpAdress + Protocol + TotalPacketLength;
        }

        private void SetDest()
        {
            DestIpAdress = "\t Destination Adress : " + IPv4Packet.DestIpAdress;
        }

        private void SetSource()
        {
            SourceIpAdress = "Source Adress : " + IPv4Packet.SourceIpAdress;
        }

        private void Setproto()
        {
            switch (IPv4Packet.Protocol)
            {
                case 1:
                    Protocol = "\t Protocol Type : " + "ICMP";
                    break;
                case 2:
                    Protocol = "\t Protocol Type : " + "IGMP";
                    break;
                case 6:
                    Protocol = "\t Protocol Type : " + "TCP";
                    break;
                case 9:
                    Protocol = "\t Protocol Type : " + "IGRP";
                    break;
                case 17:
                    Protocol = "\t Protocol Type : " + "UDP";
                    break;
                case 47:
                    Protocol = "\t Protocol Type : " + "GRE";
                    break;
                case 50:
                    Protocol = "\t Protocol Type : " + "ESP";
                    break;
                case 51:
                    Protocol = "\t Protocol Type : " + "AH";
                    break;
                case 57:
                    Protocol = "\t Protocol Type : " + "SKIP";
                    break;
                case 88:
                    Protocol = "\t Protocol Type : " + "EIGRP";
                    break;
                case 89:
                    Protocol = "\t Protocol Type : " + "OSPF";
                    break;
                case 115:
                    Protocol = "\t Protocol Type : " + "L2TP";
                    break;                
            }               
        }

        private void SetTTL()
        {
            TTL = "\t TTL : " + IPv4Packet.TTL;
        }

        private void SetTotalLength()
        {
            TotalPacketLength = "\t Length : " + IPv4Packet.TotalPacketLength + "\t Byte";
        }        

        private void SetheaderLength()
        {
            HeaderLength = "\t Header Length : " + IPv4Packet.HeaderLength;
        }

        private void SetVersion()
        {
            Version = "\t Version : " + IPv4Packet.Version;
        }
    }
}
