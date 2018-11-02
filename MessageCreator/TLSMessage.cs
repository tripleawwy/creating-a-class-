using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace MessageCreator
{
    public class TLSMessage
    {
        private TLS_Record TLS_Record;
        private string RecordType { get; set; }
        private string Version { get; set; }
        private string Length { get; set; }

        public string ImportantTlsMessage;

        public TLSMessage(TLS_Record tlsRecord)
        {
            if (tlsRecord.IsTls == true)
            {
                TLS_Record = tlsRecord;
                SetRecordType();
                SetVersion();
                SetLength();
                SetImportantMessage();
            }
        }

        private void SetImportantMessage()
        {
            ImportantTlsMessage = RecordType + Version + Length;
        }

        private void SetLength()
        {
            Length = "\t Length : " + TLS_Record.Length.ToString();
        }

        private void SetVersion()
        {
            Version = "\t Version : " + TLS_Record.VersionMajor.ToString() + "." + TLS_Record.VersionMinor.ToString();
        }

        private void SetRecordType()
        {
            RecordType = "Record Type : " + TLS_Record.RecordType.ToString();
        }
    }
}
