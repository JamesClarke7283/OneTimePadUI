using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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

        public static byte[] GenerateKeystreamRNGDevice(string devicePath, int length)
        {
            using (BinaryReader file = new BinaryReader(File.OpenRead(devicePath)))
            {
                return file.ReadBytes(length);        
            }

        }


        public static byte[] ToBytes(string txt, string[] charset) 
        {
            List<byte> byteArr = new List<byte> {};

            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(txt);

            int index;
            List<string> textArr = new List<string>(){};

            while (enumerator.MoveNext())
            {
                textArr.Add((string)enumerator.Current);
            }

            foreach (string item in textArr) {
                index = Array.IndexOf(charset, item);
                //Console.WriteLine($"{item}=>{index}");
                byteArr.Add((byte)index);
            }

            return byteArr.ToArray();
        }

        public static string ToString(byte[] byteArr, string[] charset) 
        {
            string cipherString = "";
            foreach (byte cipherByte in byteArr) 
            {
                int charInt = ((int)cipherByte);
                cipherString += charset[nfmod(charInt, charset.Length)];
            }
            return cipherString;
        }

        private static int nfmod(float a, float b) 
        {
            return (int)(a - b * Math.Floor(a / b));
        }

        private static byte[] OTP(byte[] msg, byte[] key, bool dir , string[] charset, bool hasPadding)
        {
            if (msg.Length == key.Length || (key.Length > msg.Length && hasPadding == false))
            {
                List<byte> cipherBytes = new List<byte> { };

                for (int i = 0; i < msg.Length; i++)
                {
                    int cipherInt = dir ? msg[i] + key[i] : msg[i] - key[i];
                    cipherBytes.Add((byte)(nfmod(cipherInt, charset.Length)));
                }

                return cipherBytes.ToArray();
            }
            else if (msg.Length < key.Length && hasPadding == true)
            {
                otlib.otp o = new otlib.otp();

                int addedkeys = key.Length - msg.Length;
                string addedmsg = ToString(otp.GenerateKeystream(addedkeys), charset);
                string strMsg = ToString(msg, charset);
                strMsg += addedmsg;
                return OTP(ToBytes(strMsg,charset), key, dir, charset, hasPadding);

            } else if (msg.Length > key.Length)
            {
                throw new Exception("Message is longer than the key");
            }

            return new byte[] { };
        }

        public static byte[] Encrypt(string msg, byte[] keystream, string[] codeCharset, string[] textCharset, bool hasPadding) 
        {
            Dictionary<string, string> dict = otConversionTable.GenerateConversionTable(codeCharset, textCharset);
            msg = otConversionTable.Encode(dict, msg); 
            return OTP(ToBytes(msg, codeCharset), keystream, true, codeCharset, hasPadding);
        }

        public static byte[] Decrypt(string msg, byte[] keystream, string[] codeCharset, string[] textCharset, bool hasPadding)
        {
            Dictionary<string, string> dict = otConversionTable.GenerateConversionTable(codeCharset, textCharset);
            string plainCode = ToString(OTP(ToBytes(msg, codeCharset), keystream, false, codeCharset, hasPadding),codeCharset);
            string plainText = otConversionTable.Decode(dict, plainCode);
            return ToBytes(plainText,textCharset);
        }

    }
}
