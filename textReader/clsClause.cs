using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using memoryRelative;
using textParser;

namespace textReader
{
    public enum ClauseType { interogative, declaritive, imperitive }

    // A clause is a single thought with a subject and predicate
    public class clsClause
    {
        // phrase are simple list of concepts representing subjects, verbs and, predicate
        public List<clsPhrase> phrases = new List<clsPhrase>();
        private clsMemory memory;
        public ClauseType type = ClauseType.declaritive;

        public clsClause(clsMemory memory)
        {
            this.memory = memory;
        }

        // build segments up into noun, verb and indirect object phrases
        public void readSegments(List<clsSegment> segments) {
        
            // get the is and verb concepts
            clsConcept isConcept = memory.recallConcept("is");
            clsConcept verbConcept = memory.recallConcept("verb");

            List<clsPhrase> Topics = new List<clsPhrase>();

            // loop fragements and segments since the commas are not nessisarily the end of a phrase
            clsPhrase phrase;
            clsPattern pattern;


            PhraseType currentPhraseType = PhraseType.noun;


            // starting subject phrase
            phrase = new clsPhrase(memory);
            phrase.type = PhraseType.noun;
            currentPhraseType = PhraseType.noun;
            this.phrases.Add(phrase);

            foreach (clsSegment segment in segments)
            {
                // look up pattern for this segment
                pattern = memory.recallCreatePattern(segment.text);

                if (pattern.text == "are")
                {
                    int x = 2;
                }

                if (currentPhraseType == PhraseType.noun)
                {
                    // if pattern still fits the expected phrase keep adding
                    if (pattern.objectRelationships(isConcept, verbConcept).Count == 0) phrase.patterns.Add(pattern); // keep adding to subject phrase until we hit a verb
                    else
                    {
                        // starting new verb phrase
                        phrase = new clsPhrase(memory);
                        phrase.type = PhraseType.verb;
                        currentPhraseType = PhraseType.verb;
                        this.phrases.Add(phrase);
                    }
                }

                // building verb phrase
                if (currentPhraseType == PhraseType.verb)
                {
                    if (pattern.objectRelationships(isConcept, verbConcept).Count > 0) phrase.patterns.Add(pattern); // keep adding to verb phrase until we hit word that is not a verb
                    else
                    {
                        // starting object phrase
                        phrase = new clsPhrase(memory);
                        phrase.type = PhraseType.absolute;
                        currentPhraseType = PhraseType.absolute;
                        this.phrases.Add(phrase);
                    }
                }

                // building object phrase
                if (currentPhraseType == PhraseType.absolute) phrase.patterns.Add(pattern);

            }

            // add last phase to phrases
            this.phrases.Add(phrase); // keep adding to object phrase
        }


        /*
        public void identifyProperNouns(clsFragment fragment)
        {
            // thias has problems combining pronouns in titles that have lower case words like "Rock and Roll".
            // maybe adding a quote search as well will help
            clsConcept isConcept = memory.recallConcept("is");
            clsConcept properNounConcept = memory.recallConcept("proper noun");

            for (int s = 0; s < fragment.segments.Count; s++)
            {
                if (fragment.segments[s].isCapitalized)
                {
                    // merge multiple capitalized words into a single proper noun
                    int currentSegment = s;
                    while (fragment.segments[s].isCapitalized && (s < fragment.segments.Count() - 1))
                    {
                        if (fragment.segments[s + 1].isCapitalized)
                        {
                            fragment.segments[currentSegment].text += " " + fragment.segments[s + 1].text;
                            fragment.segments.RemoveAt(s + 1);
                        }
                        else s++;
                    }

                    fragment.segments[currentSegment].pattern.firstConcept.connectObject(isConcept, properNounConcept);
                }
            }
        }

        public void diagram(clsSentence sentence)
        {
            List<clsSegment> subjectSegments = new List<clsSegment>();

            foreach (clsFragment fragment in sentence.fragments)
            {
                bool foundVerb = false;
                foreach (clsSegment segment in fragment.segments)
                {
                    //set segment pattern
                    segment.pattern = memory.recallCreatePattern(segment.text);

                    // get concept relationships
                    segment.partsOfSpeech = "";
                    clsConcept segmentConcept = segment.pattern.firstConcept;
                    List<clsRelationship> relationships = segmentConcept.subjectRelationships(isConcept);
                    foreach (clsRelationship relationship in relationships)
                    {
                        segment.partsOfSpeech += relationship.objectConcept.firstPattern.text;
                    }


                    // diagram code
                    // map subject and predicate
                    if (!foundVerb)
                    {
                        foreach (clsConcept concept in segment.pattern.concepts)
                        {
                            if (concept.subjectRelationship(isConcept, verbConcept) != null) foundVerb = true;
                        }

                    }
                    if (!foundVerb) segment.diagram = Diagram.subject;
                    else segment.diagram = Diagram.predicate;


                }
        }
        */

        public static string toString(clsConcept concept) 
        {

            return "";
        }



    }
}
