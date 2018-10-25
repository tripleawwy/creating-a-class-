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
        public string version { get; private set; }
        public int headerLength { get; private set; }
        public int totalpacketLength { get; private set; }
        public int ttl { get; private set; }
        public int protocol { get; private set; }
        public string sourceIpAdress { get; private set; }
        public string destIpAdress { get; private set; }

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
            setVersion();
            setheaderLength();
            setTotalLength();
            setTTL();
            setproto();
            setSource();
            setDest();
            getIpPayload();
        }

        private void getIpPayload()
        {
            ipPayload = new byte[totalpacketLength - headerLength];
            for (int i = headerLength; i < totalpacketLength; i++)
            {
                ipPayload[i - headerLength] = ipv4Buffer[i];
            }
        }

        private void setDest()
        {
            destIpAdress = ipv4Buffer[16] + "." + ipv4Buffer[17] + "." + ipv4Buffer[18] + "." + ipv4Buffer[19];
        }

        private void setSource()
        {
            sourceIpAdress = ipv4Buffer[12] + "." + ipv4Buffer[13] + "." + ipv4Buffer[14] + "." + ipv4Buffer[15];
        }

        private void setproto()
        {
            protocol = ipv4Buffer[9];
        }

        private void setTTL()
        {
            ttl = ipv4Buffer[8];
        }

        private void setTotalLength()
        {
            totalpacketLength = (ipv4Buffer[2] << 8) | (ipv4Buffer[3]);
        }

        private void setheaderLength()
        {
            headerLength = (ipv4Buffer[0] & 15) * 4;
        }

        private void setVersion()
        {
            version = "IPv" + (ipv4Buffer[0] >> 4);
        }
    }
}
