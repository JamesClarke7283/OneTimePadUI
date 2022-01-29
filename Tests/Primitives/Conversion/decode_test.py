import unittest
from Core.Constants import Charset
from Core.Crypto.Primitives.Conversion import Table


class Decode(unittest.TestCase):
    def test_decoded(self):
        table = Table(Charset.NUMERIC, Charset.UPPER_LOWER_NUMERIC_PUNC_SPC)
        msg = "Hello World"
        encode_msg = "6031383841937541443830"
        decoded_msg = table.decode(encode_msg)
        self.assertEqual(decoded_msg, msg)


if __name__ == '__main__':
    unittest.main()
