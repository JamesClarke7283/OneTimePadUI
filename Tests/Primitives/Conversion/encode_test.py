import unittest
from Core.Constants import Charset
from Core.Crypto.Primitives.Conversion import Table

table = Table(Charset.NUMERIC, Charset.UPPER_LOWER_NUMERIC_PUNC_SPC)


class Encode(unittest.TestCase):
    def test_length(self):
        global table

        msg = "Hello World"
        encoded_msg = table.encode(msg)
        self.assertGreater(len(encoded_msg), len(msg))

    def test_encode(self):
        global table

        msg = "Hello World"
        encoded_msg = table.encode(msg)
        self.assertEqual(encoded_msg, "6031383841937541443830")


if __name__ == '__main__':
    unittest.main()
