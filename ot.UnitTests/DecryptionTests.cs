using System;
using NUnit.Framework;
using otlib;

namespace ot.UnitTests
{
    public class DecryptionTests
    {
        [TestCase("Raf71MyEQYy6efE", "helloHworldH3uj", "u64WNFCajxVzlvf")]
        public void TestDecryptionValid(string inputText, string exceptedOutput, string key)
        {
            otp oneTimePad = new();
            byte[] keyAsBytes = otp.ToBytes(key);

            string actualOutput = otp.ToString(oneTimePad.Decrypt(inputText, keyAsBytes));

            Assert.AreEqual(exceptedOutput, actualOutput);
        }
        [TestCase("testing", "")]
        [TestCase("testing", "a")]
        public void TestDecryptionInvalid(string inputText, string key)
        {
            otp oneTimePad = new();
            byte[] keyAsBytes = otp.ToBytes(key);

            Assert.Throws<Exception>(() => oneTimePad.Decrypt(inputText, keyAsBytes));
        }
    }
}