using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace otlib
{
    public class otp
    {
        public static byte[] GenerateKeystream(int length) 
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            byte[] keystream = new byte[length];

            provider.GetBytes(keystream);

            return keystream;
        }

        public static byte[] ToBytes(string txt, string charset) 
        {
            List<byte> byteArr = new List<byte> {};
            foreach(char c in txt) 
            {
                int index = Array.IndexOf(charset.ToCharArray(), c);
                byteArr.Add((byte)index);
            }
            return byteArr.ToArray();
        }

        public static string ToString(byte[] byteArr, string charset) 
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

        private static byte[] OTP(byte[] msg, byte[] key, bool dir , string charset)
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
                otlib.otp o = new otlib.otp();

                int addedkeys = key.Length - msg.Length;
                string addedmsg = ToString(otp.GenerateKeystream(addedkeys), Settings.charset);
                string strMsg = ToString(msg,Settings.charset);
                strMsg += addedmsg;
                return OTP(ToBytes(strMsg,Settings.charset), key, dir, Settings.charset);

            } else if (msg.Length > key.Length)
            {
                throw new Exception("Message is longer than the key");
            }

            return new byte[] { };
        }

        public static byte[] Encrypt(string msg, byte[] keystream, string charset) 
        {
            return OTP(ToBytes(msg, charset), keystream, true, charset);
        }

        public static byte[] Decrypt(string msg, byte[] keystream, string charset)
        {
            return OTP(ToBytes(msg, charset), keystream, false, charset);
        }

    }
}
