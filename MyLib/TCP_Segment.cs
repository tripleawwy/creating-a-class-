using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class TCP_Segment
    {
        byte[] internTCPBuffer;


        //MainInformations
        public int sourcePort { get; private set; }
        public int destinationPort { get; private set; }
        public int sequenceNumber { get; private set; }
        public int acknowledgmentNumber { get; private set; }

        //HeaderLength instead of offset
        private int offset;
        public int tcpHeaderLength { get; private set; }

        //TCP Flags
        public bool isReducedFlag { get; private set; }
        public bool isEchoFlag { get; private set; }
        public bool isUrgentFlag { get; private set; }
        public bool isAckFlag { get; private set; }
        public bool isPushFlag { get; private set; }
        public bool isResetFlag { get; private set; }
        public bool isSynFlag { get; private set; }
        public bool isFinishedFlag { get; private set; }

        //not implemented yet // TODO
        private int reserved;
        private int window;
        private int checksum;
        private int urgentPointer;
        private int tcpOptions;



        public TCP_Segment(byte[] tcpBuffer)
        {
            internTCPBuffer = tcpBuffer;
            setSourcePort();
            setDestPort();
            setSeqNum();
            setAckNum();
            setHeaderLength();
            setAckFlag();

        }

        private void setAckFlag()
        {
            if (((internTCPBuffer[13] >> 4) & 1) == 0)
            {
                isAckFlag = false;
            }
            else
            {
                isAckFlag = true;
            }
        }

        private void setHeaderLength()
        {
            offset = (internTCPBuffer[12] >> 4);
            tcpHeaderLength = offset * 4;
        }

        private void setSeqNum()
        {
            sequenceNumber = (internTCPBuffer[4] << 24)
                               | (internTCPBuffer[5] << 16)
                               | (internTCPBuffer[6] << 8)
                               | (internTCPBuffer[7]);
        }

        private void setAckNum()
        {
            acknowledgmentNumber = (internTCPBuffer[8] << 24)
                   | (internTCPBuffer[9] << 16)
                   | (internTCPBuffer[10] << 8)
                   | (internTCPBuffer[11]);
        }

        private void setSourcePort()
        {
            //int help = internesArschloch[0];
            //int output = help << 8;
            //help = internesArschloch[1];
            //SourcePort = output | help;

            sourcePort = (internTCPBuffer[0] << 8) | internTCPBuffer[1];
        }

        public string getSourcePort()
        {
            return sourcePort.ToString();
        }

        private void setDestPort()
        {
            destinationPort = (internTCPBuffer[2] << 8) | internTCPBuffer[3];
        }




    }
}