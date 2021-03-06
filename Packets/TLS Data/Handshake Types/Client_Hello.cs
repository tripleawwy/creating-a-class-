﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.TLS_Data.Handshake_Types
{
    public class Client_Hello
    {
        //Incoming Handshake Payload
        private readonly byte[] client_helloBuffer;

        //MainInformation
        public string Version { get; set; }
        public string Random { get; set; }
        public string SessionID { get; set; }
        private string cipherList;
        public string CompressionMethod { get; set; }
        private int RandomLength => 32;
        public int SessionLength { get; set; }
        public int CipherSuiteLength { get; set; }
        public int CipherSuiteCount => CipherSuiteLength / 2;

        //TODO :
        //public string Extensions { get; set; }


        public string HexMessage { get; set; }
        public string HelloMessage => "\nversion : " + Version
            + "\nRNG Number : " + Random 
            + "\nsession id : " + SessionID 
            + "\nsession length : " + SessionLength 
            + "\nCipherSuiteCount : " + CipherSuiteCount 
            + "\nCipherSuites : "+ cipherList                        
            + "\nCompressionMethod : " + CompressionMethod
            + "\n\nHexmessage : " + HexMessage;
      

        public Client_Hello(byte[] handshakePayload)
        {
            client_helloBuffer = handshakePayload;
            SetVersion();
            SetRandom();
            SetSessionLength();
            SetSessionID();
            SetTLSPayload();
            SetCipherSuiteLength();
            ShowCipherSuites();
            SetMethod();
            //SetExtensions();
        }

        private void ShowCipherSuites()
        {
            int i = 0;
            int startPos = SessionLength + RandomLength + 5;
            do
            {
                cipherList = cipherList + "0x" + String.Format("{0:x6}", client_helloBuffer[startPos] << 8 | client_helloBuffer[startPos + 1]) + " ";
                startPos = startPos + 2;
                i++;
            } while (i<CipherSuiteCount);

        }

        private void SetTLSPayload()
        {
            for (int i = 0; i < client_helloBuffer.Length; i++)
            {
                HexMessage = HexMessage + client_helloBuffer[i].ToString("X2") + " ";                
            }
        }


        private void SetMethod()
        {
            int startPos = SessionLength + RandomLength + CipherSuiteLength + 6;
            CompressionMethod = client_helloBuffer[startPos].ToString();
        }

        private void SetCipherSuiteLength()
        {
            int startPos = SessionLength + RandomLength + 3;
            CipherSuiteLength = client_helloBuffer[startPos] << 8 | client_helloBuffer[startPos + 1];
        }

        private void SetSessionID()
        {
            int startPos = 35;
            for (int i = 0; i < SessionLength; i++)
            {
                SessionID = SessionID + client_helloBuffer[i + startPos].ToString("x2") + " ";
            }
        }

        private void SetSessionLength()
        {
            int startPos = 34;
            SessionLength = client_helloBuffer[startPos];
        }

        private void SetRandom()
        {
            
            byte timeStamp = 4;
            int startPos = timeStamp + 2;
            byte randomLength = 28;
            for (int i = 0; i < randomLength; i++)
            {
                Random = Random + client_helloBuffer[i+startPos].ToString("x2") + " ";
            }
        }

        private void SetVersion()
        {
            string result;
            result = client_helloBuffer[0] + "." + client_helloBuffer[1];
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
