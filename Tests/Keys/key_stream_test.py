import unittest
from Core.Crypto.Keys.Generate import key_stream
from Core.Constants import Charset
from random import SystemRandom


class KeyStream(unittest.TestCase):
    def test_length(self):
        r = SystemRandom()
        length = r.randint(1, 1000000)

        ks = key_stream(length, Charset.NUMERIC)
        self.assertEqual(len(ks), length-1)


if __name__ == '__main__':
    unittest.main()
