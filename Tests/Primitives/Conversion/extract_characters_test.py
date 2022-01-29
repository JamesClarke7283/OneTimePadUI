import unittest
from Core.Crypto.Primitives.Conversion import extract_characters
from Core.Constants import Charset


class ExtractCharacters(unittest.TestCase):
    def test_length(self):
        chrset = Charset.EMOJI
        chrstr = "".join(chrset)

        array = extract_characters(chrstr)
        self.assertEqual(len(array), len(Charset.EMOJI))


if __name__ == '__main__':
    unittest.main()
