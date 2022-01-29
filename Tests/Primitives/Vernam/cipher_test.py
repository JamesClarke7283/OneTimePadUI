import unittest
from Core.Crypto.Primitives.Vernam import cipher
from Core.Crypto.Keys.Generate import key_stream
from Core.Constants import Charset


class Vernam(unittest.TestCase):
    def test_length(self):
        msg = "Hello World"
        ks = key_stream(len(msg), Charset.NUMERIC)
        cipher_array = cipher(msg, ks, True, Charset.UPPER_LOWER_NUMERIC_PUNC_SPC)
        self.assertEqual(len(cipher_array), len(msg))


if __name__ == '__main__':
    unittest.main()
