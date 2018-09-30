using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textParser
{
    // ths parser is used to break large blocks of text into groups of words, sentences, paragraphs and pages. 
    // It separates the punctuation from the words.
    public class clsParser
    {
        public clsPage page = null;

        public clsParser()
        {
        }

        // single page parsing
        public void parseDocumentString(string documentString)
        {
            // parse new page
            page = new clsPage(documentString);
        }

        public string toString()
        {
            return page.toString();
        }

        public string toJSON()
        {
            return page.toJSON();
        }
    }
}
