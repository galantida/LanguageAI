using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textParser
{
    public static class parsingUtils
    {
        public static List<string> breakApart(string originalString, char[] delimiters, int minlength = 0)
        {
            List<string> results = new List<string>();

            string lastString = "";
            while (originalString.Length > 0)
            {
                int end = originalString.IndexOfAny(delimiters); // find next delimiter
                if (end >= 0)
                {
                    // found a delimiter

                    // add this content
                    string content = originalString.Substring(0, end); // get the content
                    string delimiter = originalString.Substring(end, 1); // get delimiter

                    if ((end < minlength) && (results.Count > 0)) // add to last one if there is one
                    {
                        // too small add it to last sting
                        results[results.Count - 1] += content + delimiter;
                    }
                    else
                    {
                        // make a new string and add it
                        results.Add(content + " " + delimiter);
                    }

                    // cut off the text and delimiter in the old string
                    originalString = originalString.Substring(end + 1);
                }
                else
                {
                    // no delimiters just add remainder
                    results.Add(originalString);
                    originalString = "";
                }
            }

            return results;
        }

        public static bool startsWith(string content, List<string> prefixes)
        {
            foreach (string prefix in prefixes)
            {
                if (content.StartsWith(prefix)) return true;
            }
            return false;
        }

        public static bool endsWith(string content, List<string> suffixes)
        {
            foreach (string suffix in suffixes)
            {
                if (content.EndsWith(suffix)) return true;
            }
            return false;
        }
    }
}
