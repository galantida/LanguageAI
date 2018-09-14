﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public enum Diagram { subject, predicate };

    public class clsSegment
    {
        public static char[] delimiters = new char[] { ':', '(', ')', '[', ']', '"' }; // this should include conjuctions
        public string text;
        public clsConcept concept = null;
        public Diagram diagram;

        // segment specific properties
        Dictionary<string, string> properties = new Dictionary<string, string>();

        public clsSegment(string text)
        {
            this.text = text;
        }

        // parse clauses based on segment delimiters
        public static List<clsSegment> parsefragmentString(string clauseString)
        {
            string[] segmentStrings = clauseString.Split(' '); // just use space to delimit

            // get all delimiters for sentences & clauses
            List<char> allPunctuation = clsSegment.punctuation;

            // create segment objects
            clsSegment segment;
            List<clsSegment> segments = new List<clsSegment>();
            foreach (string segmentString in segmentStrings)
            {
                string ss = segmentString.Trim();

                if (ss.Length > 0)
                {
                    // break in to segements by spaces separting punctuation from words as well.
                    List<string> unpunctuatedSegments = punctuationSpread(ss, allPunctuation);

                    foreach (string subsegment in unpunctuatedSegments)
                    {
                        // add to list of segments
                        segment = new clsSegment(subsegment);
                        segments.Add(segment);
                    }
                }
            }

            return segments;
        }


        public static List<string> punctuationSpread(string content, List<Char> wordPunctuation)
        {
            List<string> spread = new List<string>();

            string word = "";
            foreach (char c in content)
            {
                if (wordPunctuation.Contains(c))
                {
                    // before adding punctuation add any word in progress
                    if (word.Length > 0)
                    {
                        spread.Add(word);
                        word = "";
                    }

                    // add punctuation as own word
                    spread.Add(c.ToString());
                }
                else
                {
                    // add to word
                    word += c.ToString();
                }
            }

            // add any word in progress
            if (word.Length > 0) spread.Add(word);


            return spread;
        }

        public static List<char> punctuation
        {
            get
            {
                List<char> allPunctuation = clsSegment.delimiters.ToList<char>(); // get segment delimiters

                foreach (char c in clsSentence.delimiters.ToList<char>())
                {
                    allPunctuation.Add(c);
                }

                foreach (char c in clsFragment.delimiters.ToList<char>())
                {
                    allPunctuation.Add(c);
                }

                return allPunctuation; 
            }
        }

        public bool isCapitalized
        {
            get
            {
                return Char.IsUpper(text[0]);
            }
        }

        public bool isPunctuation
        {
            get
            {
                if (this.text.Length == 1)
                {
                    return clsSegment.punctuation.Contains(text[0]);
                }
                else return false;
            }
        }

        public string toJSON()
        {
            // display the unidentified text
            return JSONUtils.toJSON(this.text);
        }

        public string toString(OutputType outputType)
        {
            if (this.isPunctuation) return this.text;

            switch (outputType)
            {
                case OutputType.toString:

                    return "\"" + this.text + "\"";
                    
                case OutputType.toPartsOfSpeech:

                    // analysis
                    string analysis = "";
                    string delimiter = "";
                    List<clsRelationship> relationships = this.concept.objectRelationships("is");
                    foreach (clsRelationship relationship in relationships)
                    {
                        analysis += delimiter + relationship.objectConcept.text;
                        delimiter = ",";
                    }
                    return "( '" + this.text + "' " + analysis + " )";

                case OutputType.toDiagram:

                    if (this.isPunctuation) return this.text;

                    // analysis
                    return "( '" + this.text + "' " + this.diagram + " )";

                default:

                    return null;
            }
        }
    }
}
