using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsWikipedia: clsWeb
    {
        /***************************************************************
         * This wraps teh base web class for read web pages.
         * It should only include code specific to Wikipedia
         **************************************************************/

        string baseUrl = "https://en.wikipedia.org";

        // google example
        //string baseUrl = "https://google.com";
        //string url = String.Format(baseUrl + @"/search?hl=en&q={0}", searchText);

        public clsWikipedia() : base ()
        {

        }

        public void lookup(string topic)
        {
            base.request(String.Format(baseUrl + @"/wiki/{0}", topic.Replace(" ", "_")));
        }

    }
}
