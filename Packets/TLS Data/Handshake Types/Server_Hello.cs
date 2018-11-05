using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.TLS_Data.Handshake_Types
{
    public class Server_Hello
    {
        //Incoming Handshake Payload
        private readonly byte[] server_helloBuffer;

        //MainInformation
        public string Version { get; set; }
        public string Random { get; set; }
        public string SessionID { get; set; }
        private string cipherList;
        public string CompressionMethod { get; set; }
        private int RandomLength => 32;
        public int SessionLength { get; set; }
        public int ExtensionLength { get; set; }

        //TODO :
        //public string Extensions { get; set; }

        public string HexMessage { get; set; }
        public string HelloMessage => "\nversion : " + Version
            + "\nRNG Number : " + Random
            + "\nsession id : " + SessionID
            + "\nsession length : " + SessionLength
            + "\nCipher Suites : " + cipherList
            + "\nCompression Method : " + CompressionMethod
            + "\nExtension Length : " + ExtensionLength
            + "\n\nHexmessage : " + HexMessage;

        public Server_Hello(byte [] handshakePayload)
        {
            server_helloBuffer = handshakePayload;
            SetVersion();
            SetRandom();
            SetSessionLength();
            SetSessionID();
            SetTLSPayload();
            ShowCipherSuites();
            SetMethod();
            SetExtensionLength();
            //SetExtensions();
        }

        private void SetExtensionLength()
        {
            int startPos = SessionLength + RandomLength + 6;
            ExtensionLength = server_helloBuffer[startPos] << 8 | server_helloBuffer[startPos+1];
        }

        private void ShowCipherSuites()
        {
            int startPos = SessionLength + RandomLength + 3;
            cipherList = cipherList + "0x" + String.Format("{0:x6}", server_helloBuffer[startPos] << 8 | server_helloBuffer[startPos + 1]) + " ";
        }

        private void SetTLSPayload()
        {
            for (int i = 0; i < server_helloBuffer.Length; i++)
            {
                HexMessage = HexMessage + server_helloBuffer[i].ToString("X2") + " ";
            }
        }

        private void SetMethod()
        {
            int startPos = SessionLength + RandomLength + 5;
            CompressionMethod = server_helloBuffer[startPos].ToString();
        }

        private void SetSessionID()
        {
            int startPos = 35;
            for (int i = 0; i < SessionLength; i++)
            {
                SessionID = SessionID + server_helloBuffer[i + startPos].ToString("x2") + " ";
            }
        }

        private void SetSessionLength()
        {
            int startPos = 34;
            SessionLength = server_helloBuffer[startPos];
        }

        private void SetRandom()
        {

            byte timeStamp = 4;
            int startPos = timeStamp + 2;
            byte randomLength = 28;
            for (int i = 0; i < randomLength; i++)
            {
                Random = Random + server_helloBuffer[i + startPos].ToString("x2") + " ";
            }
        }

        private void SetVersion()
        {
            string result;
            result = server_helloBuffer[0] + "." + server_helloBuffer[1];
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
