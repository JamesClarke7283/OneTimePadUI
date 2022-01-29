import unittest
from Core.Crypto.Keys.Generate import key_stream_device
from Core.Constants import Charset
from random import SystemRandom


class KeyStreamDevice(unittest.TestCase):
    def test_length(self):
        r = SystemRandom()
        length = r.randint(1, 100000)

        ks = key_stream_device(length, "/dev/urandom", Charset.NUMERIC)
        self.assertEqual(len(ks), length)


if __name__ == '__main__':
    unittest.main()
