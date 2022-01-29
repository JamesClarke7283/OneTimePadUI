import unittest
from Core.Constants import Charset
from Core.Crypto.OTP import Create


class Encrypt(unittest.TestCase):
    def test_encrypt(self):
        otp = Create(Charset.NUMERIC, Charset.UPPER_LOWER_NUMERIC_PUNC_SPC, padding=False)
        msg = "Hello World"
        ks = "44179869402628953889531294457435274885446197887588"
        cipher_text = otp.encrypt(msg, ks)

        self.assertEqual(cipher_text, "0448269781199336721783")


if __name__ == '__main__':
    unittest.main()
