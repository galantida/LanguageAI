﻿using System;
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
            int end = 0;

            if (paragraphString.Length > 0) {
                while (end != -1) // don't add empty strings
                {
                    end = paragraphString.IndexOfAny(delimiters, start); // find next delimiter

                    // get the content upto and including the delimiter or just the rest
                    int length = 0; 
                    if (end != -1) length = (end + 1) - start;
                    else length = paragraphString.Length - start;
                        
                    string content = paragraphString.Substring(start, length); 

                    // add to results
                    if ((length < minimumLength) && (sentenceStrings.Count > 0))
                    {
                        // too small add it to last string
                        sentenceStrings[sentenceStrings.Count - 1] += content; 
                    }
                    else
                    {
                        // add a new sentence string
                        sentenceStrings.Add(content); 
                    }

                    // set next start
                    start = end + 1; 
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
                switch (outputType)
                {
                    case OutputType.toDiagram:
                        {
                            result += delimiter + "Fragment[" + fragment.toString(outputType) + "]";
                            delimiter = " ";
                            break;
                        }
                    default:
                        {
                            result += delimiter + fragment.toString(outputType);
                            delimiter = " ";
                            break;
                        }
                }
                
                
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
