using System;
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

        public string UnPrettify(string key)
        {
            return key.Replace(separator.ToString(), string.Empty);

        }
    }
}
