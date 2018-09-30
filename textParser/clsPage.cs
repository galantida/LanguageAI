using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textParser
{
    //public enum OutputType { toString, toPartsOfSpeech, toDiagram };

    public class clsPage
    {
        public List<clsParagraph> paragraphs;
        

        public clsPage(string documentString)
        {
            parseDocumentString(documentString);
        }

        public void parseDocumentString(string documentString)
        {
            // only single page parsing right now assume page
            string pageString = documentString;

            // parse page based on paragraph delimiters
            paragraphs = clsParagraph.parsePageString(pageString);
        }

        public string toString()
        {
            string result = "";
            string delimiter = "";
            foreach (clsParagraph paragraph in this.paragraphs)
            {
                result += delimiter + paragraph.toString();
                delimiter = "\r\n\r\n";
            }
            return result;
        }

        public string toJSON()
        {
            string result = "";
            string delimiter = "";
            result += "{";
            result += ",\"paragraphs\":[";
            foreach (clsParagraph paragraph in this.paragraphs)
            {
                result += delimiter + paragraph.toJSON();
                delimiter = ",";
            }
            result += "]}";
            return result;
        }
    }
}
