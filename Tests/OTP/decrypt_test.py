import unittest
from Core.Constants import Charset
from Core.Crypto.OTP import Create


class Decrypt(unittest.TestCase):
    def test_decrypt(self):
        otp = Create(Charset.NUMERIC, Charset.UPPER_LOWER_NUMERIC_PUNC_SPC, padding=False)
        cipher_text = "0448269781199336721783"
        ks = "44179869402628953889531294457435274885446197887588"
        msg = otp.decrypt(cipher_text, ks)

        self.assertEqual(msg, "Hello World")


if __name__ == '__main__':
    unittest.main()
