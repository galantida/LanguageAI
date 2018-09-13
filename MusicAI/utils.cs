using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public static class JSONUtils
    {
        public static string JSONFriendly(string originalString)
        {
            return originalString.Replace("\"", "\\\"");
        }

        public static string toJSON(string value)
        {
            return "\"" + JSONFriendly(value) + "\"";
        }

        public static string toJSON(string name, string value)
        {
            return toJSON(name) + ":" + toJSON(value);
        }
    }
}
