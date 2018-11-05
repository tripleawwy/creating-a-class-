using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.TLS_Data.Handshake_Types
{
    public class Certificate_Request
    {
        //Incoming Handshake Payload
        private readonly byte[] cert_requestBuffer;

        //MainInformation
        public int CertTypesLength { get; set; }
        public string CertID { get; set; }
        public int CertAuthoritiesLength { get; set; }
        public int CertAuthoritiyLength { get; set; }
        public string CertAuthorityName { get; set; }

        public string HexMessage { get; set; }
        public string CertRequestMessage => "\nCertificate Types Length : " + CertTypesLength
            + "\nCertificate ID : " + CertID
            + "\n\nHexmessage : " + HexMessage;

        public Certificate_Request(byte[] handshakePayload)
        {
            cert_requestBuffer = handshakePayload;
            SetHexMessage();
            SetCertLength();
            SetCertID();
            //SetCertAuthorities_Length();
            //SetCA_Length();
            //SetCA_Name();
        }

        private void SetCertLength()
        {
            CertTypesLength = cert_requestBuffer[0];
        }

        private void SetCertID()
        {
            CertID = cert_requestBuffer[1].ToString("x");
        }

        private void SetCertAuthorities_Length()
        {
            throw new NotImplementedException();
        }

        private void SetCA_Length()
        {
            throw new NotImplementedException();
        }

        private void SetCA_Name()
        {
            throw new NotImplementedException();
        }

        private void SetHexMessage()
        {
            for (int i = 0; i < cert_requestBuffer.Length; i++)
            {
                HexMessage = HexMessage + cert_requestBuffer[i].ToString("X2") + " ";
            }
        }
    }
}
