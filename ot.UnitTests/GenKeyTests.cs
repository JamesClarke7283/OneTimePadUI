using System;
using NUnit.Framework;
using otlib;

namespace ot.UnitTests
{
    public class GenKeyTests
    {
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(500)]
        public void TestKeyGenValid(int length)
        {
            byte[] key = otp.GenerateKeystream(length);

            int genLength = key.Length;
            Assert.AreEqual(length, genLength);
        }
        [TestCase(-10)]
        [TestCase(0)]
        public void TestKeyGenInvalid(int length)
        {
            Assert.Throws<Exception>(() => otp.GenerateKeystream(length));
        }
    }
}