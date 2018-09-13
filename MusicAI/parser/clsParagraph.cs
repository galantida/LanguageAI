using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsParagraph
    {
        public List<clsSentence> sentences = null;
        public static char[] delimiters = new char[] { '\t', '\v', '\n', '\r', '\f' };

        public clsParagraph(string paragraphString)
        {
            sentences = clsSentence.parseParagraphString(paragraphString);
        }

        public static List<clsParagraph> parsePageString(string pageString)
        {
            List<clsParagraph> paragraphs = new List<clsParagraph>();

            int exit = 600;

            string[] paragraphStrings = pageString.Split(delimiters); // we don't care about the delimiters
            foreach (string paragraphString in paragraphStrings)
            {
                clsParagraph paragraph = new clsParagraph(paragraphString.Trim(delimiters));
                if (paragraph.sentences.Count > 0) paragraphs.Add(paragraph);
                exit--;
                if (exit <1) break;
            }

            return paragraphs;
        }

        public string toString(OutputType outputType)
        {
            string result = "\t";
            string delimiter = "";
            foreach (clsSentence sentence in this.sentences)
            {
                result += delimiter + sentence.toString(outputType);
                delimiter = "  ";
            }
            return result;
        }

        public string toJSON()
        {
            string result = "";
            string delimiter = "";
            result += "{\"sentences\":[";
            foreach (clsSentence sentence in this.sentences)
            {
                result += delimiter + sentence.toJSON();
                delimiter = ",";
            }
            result += "]}";
            return result;
        }
    }
}
