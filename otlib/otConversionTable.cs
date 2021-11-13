using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace otlib
{
    public class otConversionTable
    {
        public static Dictionary<string, string> GenerateConversionTable(string[] chr1, string[] chr2)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            int maxencodelen = (int)Math.Ceiling(chr2.Length / (decimal)chr1.Length);
            int spaceReserved = chr1.Length * maxencodelen;
            int spaceLeft = chr1.Length * chr1.Length;

            if (spaceReserved > spaceLeft)
            {
                throw new ArgumentOutOfRangeException($"You reserved {spaceReserved} space, maximum of {spaceLeft} is allowed\nTry increasing the amount of characters in the codeCharset");
            }

            bool hasInitilised = false;

            for (int chr2Index = 0, j = 0, k = 0; chr2Index < chr2.Length; chr2Index++)
            {
                if ((spaceLeft - chr1.Length) > spaceReserved)
                {
                    dict.Add(chr1[chr2Index].ToString(), chr2[chr2Index]);
                    spaceLeft = (chr1.Length - chr2Index) * chr1.Length;
                }
                else
                {
                    // decides whether to move to the next diget space, from 09 to 10 for example

                    if (hasInitilised == false)
                    {
                        k = chr2Index;
                        hasInitilised = true;
                    }

                    dict.Add(string.Concat(chr1[k].ToString(), chr1[j].ToString()), chr2[chr2Index]);
                    spaceLeft--;
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

        public static string Encode(Dictionary<string, string> d, string msg)
        {
            string trans = "";
            Dictionary<string, string> dict2 = d.ToDictionary(x => x.Value, x => x.Key);

            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(msg);
            int textElementCount = 0;

            while (enumerator.MoveNext())
            {
                trans += dict2[(string)enumerator.Current];          

                textElementCount++;
            }
            /*
            foreach (char i in msg)
            {
                trans += dict2[i.ToString()];
            }
            */
            return trans;
        }

        public static string Decode(Dictionary<string, string> d, string code)
        {

            string trans = "";
            string twostring = "";

            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(code);

            bool isNext = true;
            bool hasRun = false;

            List<string> codeLst = new List<string>() { };

            while (enumerator.MoveNext())
            {
                codeLst.Add((string)enumerator.Current);
            }

            for (int i = 0; i < codeLst.Count; i++)
            {
                if (i < codeLst.Count - 1 && d.ContainsKey(codeLst[i]) == false)
                {
                    twostring = string.Concat(codeLst[i], codeLst[i + 1]);
                }

                if (d.ContainsKey(codeLst[i]))
                {
                    trans += d[codeLst[i]];
                }
                else if (d.ContainsKey(twostring))
                {
                    trans += d[twostring];
                    i += 1;
                }

            }

            return trans;
            /*
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
            */           

            //return trans;
        }
    }
}
