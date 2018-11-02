using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.TLS_Data.Handshake_Types
{
    public class Client_Hello
    {
        //Incoming Handshake Payload
        private readonly byte[] helloBuffer;

        //MainInformation
        public string Version { get; set; }
        public int Random { get; set; }
        public int SessionLength { get; set; }
        public uint SessionID { get; set; }
        private int cipherLength;
        public uint[] CipherSuites { get; set; }
        public string CompressionMethod { get; set; }
        //public string Extensions { get; set; }
        private string cipherList;

        public string HelloMessage => "version : " + Version + "\t Cipher Suites : " + ShowCipherSuites();
        

        public Client_Hello(byte[] handshakePayload)
        {
            helloBuffer = handshakePayload;
            SetVersion();
            SetRandom();
            SetSessionLength();
            SetSessionID();
            SetCipherSuites();
            SetMethod();
            ShowCipherSuites();
            //SetExtensions();
        }

        public string ShowCipherSuites()
        {
            for (int i = 0; i < CipherSuites.Length; i++)
            {
                 cipherList = cipherList + CipherSuites[i].ToString("X6") + "\t";                
            }
            return cipherList;
        }

        private void SetMethod()
        {
            CompressionMethod = helloBuffer[11 + cipherLength + 2].ToString();
        }

        private void SetCipherSuites()
        {
            //TODO : INCORRECT

            cipherLength = BitConverter.ToInt16(helloBuffer, (6 + SessionLength));
            CipherSuites = new uint[cipherLength / 2];
            int j = (6 + SessionLength + 2);
            for (int i = 0; i < cipherLength / 2; i++)
            {
                CipherSuites[i] = BitConverter.ToUInt16(helloBuffer, j);
                j = j + 2;
            }

        }

        private void SetSessionID()
        {
            SessionID = BitConverter.ToUInt32(helloBuffer, 7);
        }

        private void SetSessionLength()
        {
            SessionLength = helloBuffer[6];
        }

        private void SetRandom()
        {
            Random = BitConverter.ToInt32(helloBuffer, 2);
        }

        private void SetVersion()
        {
            string result;
            result = helloBuffer[0] + "." + helloBuffer[1];
            switch (result)
            {
                case "3.0":
                    Version = "SSL";
                    break;
                case "3.1":
                    Version = "1.0";
                    break;
                case "3.2":
                    Version = "1.1";
                    break;
                case "3.3":
                    Version = "1.2";
                    break;
                case "3.4":
                    Version = "1.3";
                    break;
            }
        }

        //private void SetExtensions()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
