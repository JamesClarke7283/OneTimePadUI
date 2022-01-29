import unittest
from Core.Constants import Charset
from Core.Crypto.Primitives.Conversion import Table


class Generate(unittest.TestCase):
    def test_length(self):
        table = Table(Charset.NUMERIC, Charset.UPPER_LOWER_NUMERIC_PUNC_SPC)
        self.assertEqual(len(table.conversion_table.keys()), len(Charset.UPPER_LOWER_NUMERIC_PUNC_SPC))


if __name__ == '__main__':
    unittest.main()
