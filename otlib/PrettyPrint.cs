using System;
using System.Linq;
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

        public string GridPrettify(string key) 
        {
                key = string.Join(Environment.NewLine, key.Split()
                .Select((word, index) => new { word, index })
                .GroupBy(x => x.index / 5)
                .Select(grp => string.Join(" ", grp.Select(x => x.word))));
            return key;
        }

        public string UnPrettify(string key)
        {
            return key.Replace(separator.ToString(), string.Empty);

        }
    }
}
