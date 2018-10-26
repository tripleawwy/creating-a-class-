using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class ICMP_Segment
    {
        public byte[] icmpBuffer;
        public byte[] icmpPayload;

        public int Checksum { get; private set; }
        public int Type { get; private set; }
        public int Code { get; private set; }
        public int ICMPHeaderLength => 20;


        public ICMP_Segment(byte[] ipPayload, int protocol)
        {
            if (protocol == 1)
            {
                if (ipPayload.Length >= 20)
                {
                    icmpBuffer = ipPayload;
                    SetChecksum();
                    SetType();
                    SetCode();
                    GetIcmpPayload();

                }
                else
                {
                    throw new Exception("from class ICMP_Message : invalid ipPayload");
                }
            }
            else
            {
                throw new Exception("from class ICMP_Message : invalid Protocoltype (not ICMP)");
            }

        }

        private void SetCode()
        {
            Code = icmpBuffer[0];
        }

        private void SetType()
        {
            Type = icmpBuffer[1];
        }

        private void GetIcmpPayload()
        {
            icmpPayload = new byte[icmpBuffer.Length - ICMPHeaderLength];
            for (int i = ICMPHeaderLength; i < icmpBuffer.Length; i++)
            {
                icmpPayload[i - ICMPHeaderLength] = icmpBuffer[i];
            }
        }

        private void SetChecksum()
        {
            Checksum = (icmpBuffer[2] << 8) | icmpBuffer[3];
        }        
    }
}
