using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsSentence
    {
        public List<clsFragment> fragments = null;

        public static char[] delimiters = new char[] { '.', '!', '?' };

        public clsSentence(string sentenceString)
        {

            fragments = clsFragment.parseSentenceString(sentenceString);

            // diagram sentence (subject/verb/ (verb, noun, adjective)

        }

        // parse paragraph based on sentence delimiters
        public static List<clsSentence> parseParagraphString(string paragraphString)
        {
            List<string> sentenceStrings = new List<string>();

            int minimumLength = 3;
            int start = 0;
            int end = paragraphString.Length;

            if (paragraphString.Length > 0) {
                while (end > 0) // don't add empty strings
                {
                    end = paragraphString.IndexOfAny(delimiters, start) + 1; // find next delimiter

                    int length = end - start; // get length

                    string content = "";
                    if (end == 0) content = paragraphString.Substring(start); // get the rest of the content
                    else content = paragraphString.Substring(start, length); // get the content upto and including the delimiter

                    if ((length < minimumLength) && (sentenceStrings.Count > 0))
                    {
                        // too small add it to last string
                        sentenceStrings[sentenceStrings.Count - 1] += content;
                    }
                    else
                    {
                        if (end == 0) sentenceStrings.Add(content); // add a new sentence string
                        else sentenceStrings.Add(content); // add a new sentence string
                    }

                    // set next start
                    start = end; 
                }
            }


            // create list of sentence objects from string
            List<clsSentence> sentences = new List<clsSentence>();
            foreach (string sentenceString in sentenceStrings)
            {
                // add to collection
                clsSentence sentence = new clsSentence(sentenceString);
                if (sentence.fragments.Count > 0) sentences.Add(sentence);
            }

            return sentences;
        }
       

        public bool interrogative
        {
            get
            {
                return this.fragments[this.fragments.Count()-1].interrogative;
            }
        }
        

        public string toString(OutputType outputType)
        {
            string result = "";
            string delimiter = "";
            foreach (clsFragment fragment in this.fragments)
            {
                result += delimiter + fragment.toString(outputType);
                delimiter = " ";
            }
            result += "  ";

            return result;
        }

        public string toJSON()
        {
            string result = "";
            string delimiter = "";
            result += "{\"clauses\":[";
            foreach (clsFragment fragment in this.fragments)
            {
                result += delimiter + fragment.toJson();
                delimiter = ",";
            }
            result += "]}";
            return result;
        }
    }
}
