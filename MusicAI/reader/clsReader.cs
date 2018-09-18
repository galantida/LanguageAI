using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsReader : clsParser
    {
        private clsMemory memory;

        public clsReader(clsMemory memory)
        {
            this.memory = memory;
        }

        public void read()
        {
            clsConcept isConcept = memory.recallConcept("is");
            clsConcept verbConcept = memory.recallConcept("verb");


            foreach (clsParagraph paragraph in base.page.paragraphs)
            {
                foreach (clsSentence sentence in paragraph.sentences)
                {
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
                            if (!foundVerb) {
                                foreach (clsConcept concept in segment.pattern.concepts)
                                {
                                    if (concept.subjectRelationship(isConcept, verbConcept) != null) foundVerb = true;
                                }
                                
                            }
                            if (!foundVerb) segment.diagram = Diagram.subject;
                            else segment.diagram = Diagram.predicate;
                        }

                        // identify multi word pronouns
                        identifyProperNouns(fragment);
                    }
                }
            }
        }

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

        public static string toString(clsConcept concept)
        {

            return "";
        }
    }
}
