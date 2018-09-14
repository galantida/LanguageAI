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
            foreach (clsParagraph paragraph in base.page.paragraphs)
            {
                foreach (clsSentence sentence in paragraph.sentences)
                {
                    foreach (clsFragment fragment in sentence.fragments)
                    {
                        bool foundVerb = false;
                        foreach (clsSegment segment in fragment.segments)
                        {
                            segment.concept = memory.recall(segment.text); //set segment concept
                            
                            // map subject and predicate
                            if ((!foundVerb) && (segment.concept.subjectRelationship("is", memory.recall("verb")) != null)) foundVerb = true;
                            if (!foundVerb) segment.diagram = Diagram.subject;
                            else segment.diagram = Diagram.predicate;
                        }
                        identifyProperNouns(fragment);
                    }
                }
            }
        }

        public void identifyProperNouns(clsFragment fragment)
        {
            // thias has problems combining pronouns in titles that have lower case words like "Rock and Roll".
            // maybe adding a quote search as well will help

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
                    fragment.segments[currentSegment].concept.connectObject("is", memory.recall("proper noun"));
                }
            }
        }

        public static string toString(clsConcept concept)
        {

            return "";
        }
    }
}
