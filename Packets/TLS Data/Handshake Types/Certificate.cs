using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.TLS_Data.Handshake_Types
{
    public class Certificate
    {
        //Incoming Handshake Payload
        private readonly byte[] certBuffer;

        //MainInformation
        public int CertChainLength { get; set; }
        public int CertLength { get; set; }
        public string Cert { get; set; }
        //public string CertOptions { get; set; }

        //TODO : CertOptions


        public string HexMessage { get; set; }
        public string CertMessage => "\nChain Length : " + CertChainLength
            + "\nCert Length : " + CertLength
            //+ "\n Certificate : " + Cert
            //+ "\n Options : " + CertOptions
            + "\n\nHexmessage : " + HexMessage;

        public Certificate(byte[] handshakePayload)
        {
            certBuffer = handshakePayload;
            SetHexMessage();
            SetChainLength();
            SetCertLength();
            //SetCert();
            //SetCertoptions();
        }

        private void SetHexMessage()
        {
            for (int i = 0; i < certBuffer.Length; i++)
            {
                HexMessage = HexMessage + certBuffer[i].ToString("X2") + " ";
            }
        }

        private void SetChainLength()
        {
            CertChainLength = certBuffer[0] << 16 | certBuffer[1] << 8 | certBuffer[2];
        }

        private void SetCertLength()
        {
            CertLength = certBuffer[3] << 16 | certBuffer[4] << 8 | certBuffer[5];
        }

        //private void SetCert()
        //{
        //    throw new NotImplementedException();
        //}

        //private void SetCertoptions()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
