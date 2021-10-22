using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace otlib
{
    public class otp
    {
        public static string charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public otp(string charset= "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789") 
        {
            otp.charset = charset;
        }

        public static byte[] GenerateKeystream(int length) 
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            byte[] keystream = new byte[length];

            provider.GetBytes(keystream);

            return keystream;
        }

        public static byte[] ToBytes(string txt) 
        {
            List<byte> byteArr = new List<byte> {};
            foreach(char c in txt) 
            {
                int index = Array.IndexOf(charset.ToCharArray(), c);
                byteArr.Add((byte)index);
            }
            return byteArr.ToArray();
        }

        public static string ToString(byte[] byteArr) 
        {
            string cipherString = "";
            foreach (byte cipherByte in byteArr) 
            {
                int charInt = ((int)cipherByte);
                cipherString += charset.ToCharArray()[nfmod(charInt, charset.Length)];
            }
            return cipherString;
        }

        private static int nfmod(float a, float b) 
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
            else if (msg.Length < key.Length)
            {
                int addedkeys = key.Length - msg.Length;
                string addedmsg = otp.ToString(otp.GenerateKeystream(addedkeys));
                string strMsg = otp.ToString(msg);
                strMsg += addedmsg;
                return OTP(ToBytes(strMsg), key, dir);

            } else if (msg.Length > key.Length)
            {
                throw new Exception("Message is longer than the key");
            }

            return new byte[] { };
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
