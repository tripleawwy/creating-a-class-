using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class IPv4_Packet
    {
        //Incoming Ethernet_Frame Payload
        readonly byte[] ipv4Buffer;

        //Outgoing IP_Packet Payload
        public byte[] ipPayload;

        //MainInformation
        public string Version { get; private set; }
        public int HeaderLength { get; private set; }
        public int TotalpacketLength { get; private set; }
        public int TTL { get; private set; }
        public int Protocol { get; private set; }
        public string SourceIpAdress { get; private set; }
        public string DestIpAdress { get; private set; }
        public bool IsTcP { get; private set; }
        public TCP_Segment TCP_Segment;
        public UDP_Datagram UDP_Datagram;


        //Not implemented yet  //TODO
        //private int typeOfService;
        //private int identification;
        //private int checksum;
        //private int ipFlags;
        //private int offset;
        //private int ipOption;


        public IPv4_Packet(byte[] ethernetPayload)
        {
            ipv4Buffer = ethernetPayload;
            SetVersion();
            SetheaderLength();
            SetTotalLength();
            SetTTL();
            Setproto();
            SetSource();
            SetDest();
            GetIpPayload();
            if (Protocol == 6)
            {
                TCP_Segment = new TCP_Segment(ipPayload, Protocol);
            }
            if (Protocol == 17)
            {
                UDP_Datagram = new UDP_Datagram(ipPayload, Protocol);
            }
        }


        public void GetIpPayload()
        {
            ipPayload = new byte[TotalpacketLength - HeaderLength];
            for (int i = HeaderLength; i < TotalpacketLength; i++)
            {
                ipPayload[i - HeaderLength] = ipv4Buffer[i];
            }
        }

        private void SetDest()
        {
            DestIpAdress = ipv4Buffer[16] + "." + ipv4Buffer[17] + "." + ipv4Buffer[18] + "." + ipv4Buffer[19];
        }

        private void SetSource()
        {
            SourceIpAdress = ipv4Buffer[12] + "." + ipv4Buffer[13] + "." + ipv4Buffer[14] + "." + ipv4Buffer[15];
        }

        private void Setproto()
        {
            Protocol = ipv4Buffer[9];
        }

        private void SetTTL()
        {
            TTL = ipv4Buffer[8];
        }

        private void SetTotalLength()
        {
            TotalpacketLength = (ipv4Buffer[2] << 8) | (ipv4Buffer[3]);
        }

        private void SetheaderLength()
        {
            HeaderLength = (ipv4Buffer[0] & 15) * 4;
        }

        private void SetVersion()
        {
            Version = "IPv" + (ipv4Buffer[0] >> 4);
        }
    }
}
