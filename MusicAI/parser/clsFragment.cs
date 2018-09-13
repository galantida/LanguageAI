using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsFragment
    {
        public List<clsSegment> segments = null; // words, punctuation, datetimes etc...
        public static char[] delimiters = new char[] { ',', ';' };

        public clsFragment(string clauseString)
        {
            segments = clsSegment.parseClauseString(clauseString);

            identifyProperNouns();
        }


        // parse paragraph based on sentence delimiters
        public static List<clsFragment> parseSentenceString(string sentenceString)
        {
            List<string> clauseStrings = new List<string>();


            int minimumLength = 3;
            int start = 0;
            int end = sentenceString.Length;

            if (sentenceString.Length > 0)
            {
                while (end > 0) // don't add empty strings
                {
                    end = sentenceString.IndexOfAny(delimiters, start) + 1; // find next delimiter

                    int length = end - start; // get length

                    string content = "";
                    if (end == 0) content = sentenceString.Substring(start); // get the rest of the content
                    else content = sentenceString.Substring(start, length); // get the content upto and including the delimiter

                    if ((length < minimumLength) && (clauseStrings.Count > 0))
                    {
                        // too small add it to last string
                        clauseStrings[clauseStrings.Count - 1] += content;
                    }
                    else
                    {
                        if (end == 0) clauseStrings.Add(content); // add a new sentence string
                        else clauseStrings.Add(content); // add a new sentence string
                    }

                    start = end; // set next start
                }
            }

            // filter and move string to to clause objects
            List<clsFragment> clauses = new List<clsFragment>();
            foreach (string clauseString in clauseStrings)
            {
                clsFragment clause = new clsFragment(clauseString); // trim spaces only
                if (clause.segments.Count > 0) clauses.Add(clause);

            }
            return clauses;
        }

        public bool isSentence
        {
            
            get
            {
                // does it begin with a capital and end with punctuation
                if (segments[0].isCapitalized)
                {
                    // does it end with sentence or clause punctuation
                    char segmentChar = segments[segments.Count-1].text[0];
                    if (clsSentence.delimiters.Contains(segmentChar) || clsFragment.delimiters.Contains(segmentChar)) {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool interrogative
        {
            get
            {
                // does it end with sentence or clause punctuation
                char segmentChar = this.segments[this.segments.Count - 1].text[0];
                if (segmentChar == '?') return true;
                else return false;
            }
        }

        public void identify_old()
        {
            // find inital groups like U.S.A
            // find dates
            // match names first last , titles, 

            //identifyVocabWords();
            //identifyProperNouns();

        }

        public void identifyProperNouns()
        {
            // thias has problems combining pronouns in titles that have lower case words like "Rock and Roll".
            // maybe adding a quote search as well will help

            for (int s = 0; s < this.segments.Count; s++)
            {
                // check that it has not already been identified
                if (segments[s].concept == null)
                {
                    if (segments[s].isCapitalized)
                    {
                        // merge multiple capitalized words into a single proper noun
                        int currentSegment = s;
                        while (segments[s].isCapitalized && (s < segments.Count() - 1))
                        {
                            if (segments[s + 1].isCapitalized)
                            {
                                segments[currentSegment].text += " " + segments[s + 1].text;
                                segments.RemoveAt(s + 1);
                            }
                            else s++;
                        }
                    }
                }
            }
        }

        public string toString(OutputType outputType)
        {
            string result = "";
            string delimiter = "";
            foreach (clsSegment segment in this.segments)
            {
                result += delimiter + segment.toString(outputType);
                delimiter = " ";
            }
            return result;
        }

        public string toJson()
        {
            string result = "";
            string delimiter = "";
            result += "{";
            result += JSONUtils.toJSON("segments") + ":[";
            foreach (clsSegment segment in this.segments)
            {
                result += delimiter + segment.toJSON();
                delimiter = ",";
            }
            result += "]}";
            return result;
        }
    }
}
