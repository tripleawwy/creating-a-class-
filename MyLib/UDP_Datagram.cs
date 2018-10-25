using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class UDP_Datagram
    {
        public string Hi { set; get; }
        public byte[] udpBuffer;


        public UDP_Datagram(byte[] ipPayload, int protocol)
        {
            if (protocol==17)
            {
                udpBuffer = ipPayload;
                SetHi();
            }
            else
            {
                throw new Exception("scheisse");
            }
        }

        private void SetHi()
        {
            Hi = "hehehehe is udp";
        }
    }
}
