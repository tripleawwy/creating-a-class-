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
        private string Flag { get; set; }
        private string TcpHeaderLength { get; set; }

        public string ImportantMessage { get; set; }

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
                SetFlag();
                SetImportantMessage();
            }
            else
            {
                ImportantMessage = "TCP_Segment not existant";
            }
        }

        private void SetImportantMessage()
        {
            ImportantMessage = SourcePort + DestinationPort + SequenceNumber + AcknowledgmentNumber + Flag;
        }

        private void SetFlag()
        {
            if (TCP_Segment.IsReducedFlag == true)
            {
                Flag = "\t Flag : " + "C";
            }
            else if (TCP_Segment.IsEchoFlag == true)
            {
                Flag = "\t Flag : " + "E";
            }
            else if (TCP_Segment.IsUrgentFlag == true)
            {
                Flag = "\t Flag : " + "U";
            }
            else if (TCP_Segment.IsAckFlag == true)
            {
                Flag = "\t Flag : " + "A";
            }
            else if (TCP_Segment.IsPushFlag == true)
            {
                Flag = "\t Flag : " + "P";
            }
            else if (TCP_Segment.IsResetFlag == true)
            {
                Flag = "\t Flag : " + "R";
            }
            else if (TCP_Segment.IsFinishedFlag == true)
            {
                Flag = "\t Flag : " + "S";
            }
            else if (TCP_Segment.IsReducedFlag == true)
            {
                Flag = "\t Flag : " + "F";
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
