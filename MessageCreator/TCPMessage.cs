using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace MessageCreator
{
    public class TCPMessage
    {
        private readonly TCP_Segment TCP_Segment;

        private string SourcePort { get; set; }
        private string DestinationPort { get; set; }
        private string SequenceNumber { get; set; }
        private string AcknowledgmentNumber { get; set; }
        private string Flags { get; set; }
        private string TcpHeaderLength { get; set; }

        public string ImportantMessage { get; set; }
        public string TcpData { get; set; }

        public TCPMessage(IPv4_Packet ipv4packet)
        {
            TCP_Segment = ipv4packet.TCP_Segment;
            if (TCP_Segment!=null)
            {
                SetSourcePort();
                SetDestPort();
                SetSeqNum();
                SetAckNum();
                SetTcpHeaderLength();
                SetFlags();
                SetTcpData();
                SetImportantMessage();

            }
            else
            {
                ImportantMessage = "TCP_Segment not existant";
            }
        }

        private void SetTcpData()
        {
            if (TCP_Segment.tcpPayload != null && TCP_Segment.tcpPayload.Length >= 0)
            {
                TcpData = Encoding.ASCII.GetString(TCP_Segment.tcpPayload);
            }
        }

        private void SetImportantMessage()
        {
            ImportantMessage = SourcePort + DestinationPort + SequenceNumber + AcknowledgmentNumber + Flags;
        }

        private void SetFlags()
        {
            Flags = "\t Flags : ";
            if (TCP_Segment.IsReducedFlag == true)
            {
                Flags = Flags + " C";
            }
            if (TCP_Segment.IsEchoFlag == true)
            {
                Flags = Flags + " E";
            }
            if (TCP_Segment.IsUrgentFlag == true)
            {
                Flags = Flags + " U";
            }
            if (TCP_Segment.IsAckFlag == true)
            {
                Flags = Flags + " A";
            }
            if (TCP_Segment.IsPushFlag == true)
            {
                Flags = Flags + " P";
            }
            if (TCP_Segment.IsResetFlag == true)
            {
                Flags = Flags + " R";
            }
            if (TCP_Segment.IsFinishedFlag == true)
            {
                Flags = Flags + " S";
            }
            if (TCP_Segment.IsReducedFlag == true)
            {
                Flags = Flags + " F";
            }
        }

        private void SetTcpHeaderLength()
        {
            TcpHeaderLength = "\t Header Length : " + TCP_Segment.TcpHeaderLength +"\t Byte";
        }

        private void SetAckNum()
        {
            AcknowledgmentNumber = "\t Acknowledgment Number : " + TCP_Segment.AcknowledgmentNumber;
        }

        private void SetSeqNum()
        {
            SequenceNumber = "\t Sequence Number : " + TCP_Segment.SequenceNumber;
        }

        private void SetDestPort()
        {
            DestinationPort = "\t Destination Port : " + TCP_Segment.DestinationPort;
        }

        private void SetSourcePort()
        {
            SourcePort = "Source Port : " + TCP_Segment.SourcePort;
        }
    }
}
