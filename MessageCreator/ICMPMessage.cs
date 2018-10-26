using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace MessageCreator
{
    public class ICMPMessage
    {
        private readonly ICMP_Segment ICMP_Segment;

        private string Checksum { get;  set; }
        private string Type { get;  set; }
        private string Code { get;  set; }
        private string ICMPHeaderLength => "Header Length : 20 Byte";

        public string ImportantMessage { get; set; }



        public ICMPMessage(IPv4_Packet ipv4packet)
        {
            ICMP_Segment = ipv4packet.ICMP_Segment;
            if (ICMP_Segment != null)
            {                
                SetChecksum();
                SetType();
                SetCode();
                SetImportantMessage();
            }
            else
            {
                ImportantMessage = "ICMP_Segment not existant";
            }
        }

        private void SetImportantMessage()
        {
            ImportantMessage = Type + Code;
        }

        private void SetCode()
        {
            Code = "\t Code : " + ICMP_Segment.Code;
        }

        private void SetType()
        {
            Type = "\t Code : " + ICMP_Segment.Type;
        }

        private void SetChecksum()
        {
            Checksum = "\t Code : " + ICMP_Segment.Checksum;
        }
    }
}
