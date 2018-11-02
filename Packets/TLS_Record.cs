using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class TLS_Record
    {
        //Incoming TCP_Segment Payload
        private readonly byte[] tlsbuffer;

        //Outgoing TLS_Record Payload
        public byte[] tlsRecordPayload;

        //MainInformation
        public int RecordType { get; set; }
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public int Length { get; set; }
        public int HeaderLength => 5;

        //TLS Conditions

        public bool IsTls { get; set; }

        //not implemented yet // TODO

        public TLS_Record(byte[] tcpPayload)
        {
            if (tcpPayload != null
                        && tcpPayload.Length >= 5
                        && (tcpPayload[0] == 20 | tcpPayload[0] == 21 | tcpPayload[0] == 22 | tcpPayload[0] == 23)
                        && tcpPayload[1] == 3)
            {
                tlsbuffer = tcpPayload;
                SetRecordType();
                SetVersionMajor();
                SetVersionMinor();
                SetLength();
                SetTlSState();
            }
        }

        private void SetTlSState()
        {
            if ((RecordType == 20 | RecordType == 21 | RecordType == 22 | RecordType == 23) && VersionMajor==3)
            {
                IsTls = true;
            }
            else
            {
                IsTls = false;
            }
        }

        private void SetLength()
        {
            Length = (tlsbuffer[3] << 8) | tlsbuffer[4];
        }

        private void SetVersionMinor()
        {
            VersionMinor = tlsbuffer[2];
        }

        private void SetVersionMajor()
        {
            VersionMajor = tlsbuffer[1];
                }

        private void SetRecordType()
        {
            RecordType = tlsbuffer[0];
        }
    }
}
