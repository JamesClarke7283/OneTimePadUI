using System;
using System.Globalization;
using System.Text;

namespace otlib
{
    public class PrettyPrint
    {
        public char separator = ' ';

        public PrettyPrint(char Separator = ' ')
        {
            this.separator = Separator;
        }
        public string Prettify(string input)
        {
            StringBuilder builder = new StringBuilder();

            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(input);

            int textElementCount = 1;
            while (enumerator.MoveNext())
            {
                builder.Append(enumerator.Current);
                if (textElementCount % 4 == 0 && textElementCount > 0)
                {
                    builder.Append(separator);
                }
                textElementCount++;
            }
            return builder.ToString();
        }

        public string UnPrettify(string key)
        {
            return key.Replace(separator.ToString(), string.Empty);

        }
    }
}
