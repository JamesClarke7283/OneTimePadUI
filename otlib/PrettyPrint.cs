using System;
using System.Linq;

namespace otlib
{
    public class PrettyPrint
    {
        public char separator = ' ';

        public PrettyPrint(char Separator = ' ')
        {
            this.separator = Separator;
        }
        public string Prettify(string key)
        {

            for (int i = 4; i <= key.Length; i += 4)
            {
                key = key.Insert(i, separator.ToString());
                i++;
            }
            return key;
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
