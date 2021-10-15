﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace otlib
{
    public class otp
    {
        public string charset;
       

        public otp(string charset) 
        {
            this.charset = charset;
        }

        public byte[] GenerateKeystream(int length) 
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            byte[] keystream = new byte[length];

            provider.GetBytes(keystream);

            return keystream;
        }

        public byte[] ToBytes(string txt) 
        {
            List<byte> byteArr = new List<byte> {};
            foreach(char c in txt) 
            {
                int index = Array.IndexOf(charset.ToCharArray(), c);
                byteArr.Add((byte)index);
            }
            return byteArr.ToArray();
        }

        public string ToString(byte[] byteArr) 
        {
            string cipherString = "";
            foreach (byte cipherByte in byteArr) 
            {
                int charInt = ((int)cipherByte);
                cipherString += charset.ToCharArray()[nfmod(charInt, charset.Length)];
            }
            return cipherString;
        }

        private int nfmod(float a, float b) 
        {
            return (int)(a - b * Math.Floor(a / b));
        }

        private byte[] OTP(byte[] msg, byte[] key, bool dir)
        {
            if (msg.Length == key.Length) 
            {
                List<byte> cipherBytes = new List<byte> { };

                for (int i = 0; i < msg.Length; i++)
                {
                    int cipherInt = dir ? msg[i] + key[i] : msg[i] - key[i];
                    cipherBytes.Add((byte)(nfmod(cipherInt, charset.Length)));
                }

                return cipherBytes.ToArray();
            }
            else 
            {
               throw new Exception("Key needs to be same length as Message");
            }
        }

        public byte[] Encrypt(string msg, byte[] keystream) 
        {
            return OTP(ToBytes(msg), keystream, true);
        }

        public byte[] Decrypt(string msg, byte[] keystream)
        {
            return OTP(ToBytes(msg), keystream, false);
        }

    }
}
