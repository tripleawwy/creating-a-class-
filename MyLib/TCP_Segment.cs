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
        private byte[] tcpBuffer;

        //Outgoing TCP_Segment Payload
        public byte[] tcpPayload;

        //MainInformation
        public int SourcePort { get; private set; }
        public int DestinationPort { get; private set; }
        public int SequenceNumber { get; private set; }
        public int AcknowledgmentNumber { get; private set; }

        //HeaderLength instead of offset
        private int offset;
        public int TcpHeaderLength { get; private set; }

        //TCP Flags
        public bool IsReducedFlag { get; private set; }
        public bool IsEchoFlag { get; private set; }
        public bool IsUrgentFlag { get; private set; }
        public bool IsAckFlag { get; private set; }
        public bool IsPushFlag { get; private set; }
        public bool IsResetFlag { get; private set; }
        public bool IsSynFlag { get; private set; }
        public bool IsFinishedFlag { get; private set; }

        //not implemented yet // TODO
        //private int reserved;
        //private int window;
        //private int checksum;
        //private int urgentPointer;
        //private int tcpOptions;


             


        public TCP_Segment(byte[] ipPayload, int protocol)
        {           
            if (protocol == 6) // 6 = TCP
            {
                if (ipPayload.Length >= 20)
                {
                    tcpBuffer = ipPayload;
                    SetSourcePort();
                    SetDestPort();
                    SetSeqNum();
                    SetAckNum();
                    SetHeaderLength();
                    SetRedFlag();
                    SetEchoFlag();
                    SetUrgeFlag();
                    SetAckFlag();
                    SetPushFlag();
                    SetResetFlag();
                    SetSynFlag();
                    SetFinFlag();
                    GetTcpPayload();
                }
                else
                {
                    throw new Exception("from class TCP_Segment: ungültiger ipPayload");
                }

            }
            else
            {
                throw new Exception("from class TCP_Segment: is nicht tcp");
            }
        }

        private void GetTcpPayload()
        {
            tcpPayload = new byte[tcpBuffer.Length - TcpHeaderLength];
            for (int i = TcpHeaderLength; i < tcpBuffer.Length; i++)
            {
                tcpPayload[i - TcpHeaderLength] = tcpBuffer[i];
            }
        }

        private void SetFinFlag()
        {
            if ((tcpBuffer[13] & 1) == 0)
            {
                IsFinishedFlag = false;
            }
            else
            {
                IsFinishedFlag = true;
            }
        }

        private void SetSynFlag()
        {
            if (((tcpBuffer[13] >> 1) & 1) == 0)
            {
                IsSynFlag = false;
            }
            else
            {
                IsSynFlag = true;
            }
        }

        private void SetResetFlag()
        {
            if (((tcpBuffer[13] >> 2) & 1) == 0)
            {
                IsResetFlag = false;
            }
            else
            {
                IsResetFlag = true;
            }
        }

        private void SetPushFlag()
        {
            if (((tcpBuffer[13] >> 3) & 1) == 0)
            {
                IsPushFlag = false;
            }
            else
            {
                IsPushFlag = true;
            }
        }

        private void SetAckFlag()
        {
            if (((tcpBuffer[13] >> 4) & 1) == 0)
            {
                IsAckFlag = false;
            }
            else
            {
                IsAckFlag = true;
            }
        }

        private void SetUrgeFlag()
        {
            if (((tcpBuffer[13] >> 5) & 1) == 0)
            {
                IsUrgentFlag = false;
            }
            else
            {
                IsUrgentFlag = true;
            }
        }

        private void SetEchoFlag()
        {
            if (((tcpBuffer[13] >> 6) & 1) == 0)
            {
                IsEchoFlag = false;
            }
            else
            {
                IsEchoFlag = true;
            }
        }

        private void SetRedFlag()
        {
            if (((tcpBuffer[13] >> 7) & 1) == 0)
            {
                IsReducedFlag = false;
            }
            else
            {
                IsReducedFlag = true;
            }
        }

        private void SetHeaderLength()
        {
            offset = ((tcpBuffer[12] >> 4) & 15);
            TcpHeaderLength = offset * 4;
        }

        private void SetSeqNum()
        {
            SequenceNumber = (tcpBuffer[4] << 24)
                               | (tcpBuffer[5] << 16)
                               | (tcpBuffer[6] << 8)
                               | (tcpBuffer[7]);
        }

        private void SetAckNum()
        {
            AcknowledgmentNumber = (tcpBuffer[8] << 24)
                   | (tcpBuffer[9] << 16)
                   | (tcpBuffer[10] << 8)
                   | (tcpBuffer[11]);
        }

        private void SetSourcePort()
        {
            //int help = internesArschloch[0];
            //int output = help << 8;
            //help = internesArschloch[1];
            //SourcePort = output | help;

            SourcePort = (tcpBuffer[0] << 8) | tcpBuffer[1];
        }

        public string GetSourcePort()
        {
            return SourcePort.ToString();
        }

        private void SetDestPort()
        {
            DestinationPort = (tcpBuffer[2] << 8) | tcpBuffer[3];
        }




    }
}