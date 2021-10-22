using System;
using NUnit.Framework;
using otlib;

namespace ot.UnitTests
{
    public class EncryptionTests
    {
        [TestCase("hello world!", "mrozK3XLGvIM")]
        [TestCase("hello world!", "2ehIqhqS681QKpmWkjnbvXe3xJtzec")]
        public void TestEncryptionValid(string inputText, string key)
        {
            otp oneTimePad = new();
            byte[] keyAsBytes = otp.ToBytes(key);
            int keyLength = key.Length;

            byte[] actualOutput = oneTimePad.Encrypt(inputText, keyAsBytes);
            int actualLength = actualOutput.Length;

            Assert.AreEqual(keyLength, actualLength);
        }
        [TestCase("hello world!", "mrozK")]
        public void TestEncryptionInvalid(string inputText, string key)
        {
            otp oneTimePad = new();
            byte[] keyAsBytes = otp.ToBytes(key);

            Assert.Throws<Exception>(() => oneTimePad.Encrypt(inputText, keyAsBytes));
        }
    }
}