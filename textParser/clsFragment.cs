﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utils;

namespace textParser
{
    public class clsFragment
    {
        public List<clsSegment> segments = null; // words, punctuation, datetimes etc...
        public static char[] delimiters = new char[] { ',', ';' };

        public clsFragment(string fragmentString)
        {
            segments = clsSegment.parsefragmentString(fragmentString);
        }


        // Parse sentence string into fragment strings based on delimiters
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

        public bool isCapitalized
        {
            get
            {
                return segments[0].isCapitalized;
            }
        }

        public bool endsInPunctuation
        {
            get
            {
                return segments[segments.Count-1].isPunctuation;
            }
        }

        public clsSegment lastSegment
        {
            get
            {
                return this.segments[segments.Count - 1];
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

        public string toString()
        {
            string result = "";
            string delimiter = "";
            foreach (clsSegment segment in this.segments)
            {
                result += delimiter + segment.toString();
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
