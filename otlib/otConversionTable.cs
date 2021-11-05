using System;
using System.Collections.Generic;
using System.Linq;

namespace otlib
{
    public class otConversionTable
    {
        public static Dictionary<string, char> GenerateConversionTable(string chr1, string chr2)
        {
            Dictionary<string, char> dict = new Dictionary<string, char>();

            int maxencodelen = (int)Math.Ceiling(chr2.Length / (decimal)chr1.Length);
            int spaceReserved = chr1.Length * maxencodelen;
            int spaceLeft = chr1.Length * chr2.Length;

            for (int i = 0, j = 0, k = 0; i < chr2.Length; i++)
            {
                if (spaceLeft > spaceReserved)
                {
                    dict.Add(chr1[i].ToString(), chr2[i]);
                    spaceLeft = (chr1.Length - i) * chr1.Length;
                }
                else
                {
                    if (k == 0)
                    {
                        k = i;
                    }
                    dict.Add(string.Concat(chr1[k].ToString(), chr1[j].ToString()), chr2[i]);
                    j++;

                    if (j >= chr1.Length)
                    {
                        j = 0;
                        k++;
                    }
                }
            }
            return dict;
        }

        public static string Encode(Dictionary<string, char> d, string msg)
        {
            string trans = "";
            Dictionary<char, string> dict2 = d.ToDictionary(x => x.Value, x => x.Key);
            foreach (char i in msg)
            {
                trans += dict2[i];
            }
            return trans;
        }

        public static string Decode(Dictionary<string, char> d, string code)
        {

            string trans = "";
            string twostring = "";
            for (int i = 0; i < code.Length; i++)
            {
                if (i < code.Length - 1 && d.ContainsKey(code[i].ToString()) == false)
                {
                    twostring = string.Concat(code[i], code[i + 1]);
                }

                if (d.ContainsKey(code[i].ToString()))
                {
                    trans += d[code[i].ToString()];
                }
                else if (d.ContainsKey(twostring))
                {
                    trans += d[twostring];
                    i += 1;
                }

            }

            return trans;
        }
    }
}
