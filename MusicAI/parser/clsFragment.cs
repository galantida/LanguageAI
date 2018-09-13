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
            List<string> fragmentStrings = new List<string>();


            int minimumLength = 3;
            int start = 0;
            int end = 0;

            if (sentenceString.Length > 0)
            {
                while (end != -1) // don't add empty strings
                {
                    end = sentenceString.IndexOfAny(delimiters, start); // find next delimiter

                    // get the content upto and including the delimiter or just the rest
                    int length = 0;
                    if (end != -1) length = (end + 1) - start; // get length
                    else length = sentenceString.Length - start; // get length

                    string content = sentenceString.Substring(start, length);

                    // add to results
                    if ((length < minimumLength) && (fragmentStrings.Count > 0))
                    {
                        // too small to be a stand alone fragment add it to last string
                        fragmentStrings[fragmentStrings.Count - 1] += content;
                    }
                    else
                    {
                        // add a new fragment string
                        fragmentStrings.Add(content); 
                    }

                    start = end + 1; // set next start
                }
            }

            // filter and move string to to clause objects
            List<clsFragment> fragments = new List<clsFragment>();
            foreach (string fragmentString in fragmentStrings)
            {
                clsFragment clause = new clsFragment(fragmentString); // trim spaces only
                if (clause.segments.Count > 0) fragments.Add(clause);

            }
            return fragments;
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
