using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class TCP_Segment
    {
        //Incoming IP_Packet Payload
        byte[] tcpBuffer;

        //Outgoing TCP_Segment Payload
        public byte[] tcpPayload;

        //MainInformation
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
        //private int reserved;
        //private int window;
        //private int checksum;
        //private int urgentPointer;
        //private int tcpOptions;





        public TCP_Segment(byte[] ipPayload, int protocolType)
        {
            if (protocolType == 6) // 6 = TCP
            {
                tcpBuffer = ipPayload;
                setSourcePort();
                setDestPort();
                setSeqNum();
                setAckNum();
                setHeaderLength();
                setRedFlag();
                setEchoFlag();
                setUrgeFlag();
                setAckFlag();
                setPushFlag();
                setResetFlag();
                setSynFlag();
                setFinFlag();
                getTcpPayload();
            }
            else
            {
               
            }
        }

        private void getTcpPayload()
        {
            tcpPayload = new byte[tcpBuffer.Length - tcpHeaderLength];
            for (int i = tcpHeaderLength; i < tcpBuffer.Length; i++)
            {
                tcpPayload[i - tcpHeaderLength] = tcpBuffer[i];
            }
        }

        private void setFinFlag()
        {
            if ((tcpBuffer[13] & 1) == 0)
            {
                isFinishedFlag = false;
            }
            else
            {
                isFinishedFlag = true;
            }
        }

        private void setSynFlag()
        {
            if (((tcpBuffer[13] >> 1) & 1) == 0)
            {
                isSynFlag = false;
            }
            else
            {
                isSynFlag = true;
            }
        }

        private void setResetFlag()
        {
            if (((tcpBuffer[13] >> 2) & 1) == 0)
            {
                isResetFlag = false;
            }
            else
            {
                isResetFlag = true;
            }
        }

        private void setPushFlag()
        {
            if (((tcpBuffer[13] >> 3) & 1) == 0)
            {
                isPushFlag = false;
            }
            else
            {
                isPushFlag = true;
            }
        }

        private void setAckFlag()
        {
            if (((tcpBuffer[13] >> 4) & 1) == 0)
            {
                isAckFlag = false;
            }
            else
            {
                isAckFlag = true;
            }
        }

        private void setUrgeFlag()
        {
            if (((tcpBuffer[13] >> 5) & 1) == 0)
            {
                isUrgentFlag = false;
            }
            else
            {
                isUrgentFlag = true;
            }
        }

        private void setEchoFlag()
        {
            if (((tcpBuffer[13] >> 6) & 1) == 0)
            {
                isEchoFlag = false;
            }
            else
            {
                isEchoFlag = true;
            }
        }

        private void setRedFlag()
        {
            if (((tcpBuffer[13] >> 7) & 1) == 0)
            {
                isReducedFlag = false;
            }
            else
            {
                isReducedFlag = true;
            }
        }

        private void setHeaderLength()
        {
            offset = ((tcpBuffer[12] >> 4) & 15);
            tcpHeaderLength = offset * 4;
        }

        private void setSeqNum()
        {
            sequenceNumber = (tcpBuffer[4] << 24)
                               | (tcpBuffer[5] << 16)
                               | (tcpBuffer[6] << 8)
                               | (tcpBuffer[7]);
        }

        private void setAckNum()
        {
            acknowledgmentNumber = (tcpBuffer[8] << 24)
                   | (tcpBuffer[9] << 16)
                   | (tcpBuffer[10] << 8)
                   | (tcpBuffer[11]);
        }

        private void setSourcePort()
        {
            //int help = internesArschloch[0];
            //int output = help << 8;
            //help = internesArschloch[1];
            //SourcePort = output | help;

            sourcePort = (tcpBuffer[0] << 8) | tcpBuffer[1];
        }

        public string getSourcePort()
        {
            return sourcePort.ToString();
        }

        private void setDestPort()
        {
            destinationPort = (tcpBuffer[2] << 8) | tcpBuffer[3];
        }




    }
}