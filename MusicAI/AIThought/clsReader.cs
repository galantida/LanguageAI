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

        public List<clsConcept> read()
        {
            List<clsConcept> concepts = new List<clsConcept>();

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
                            concepts.Add(segment.concept); // add to concapt list

                            // map subject and predicate
                            if ((!foundVerb) && (segment.concept.childConcepts("is", memory.recall("verb")).Count > 0)) foundVerb = true;
                            if (!foundVerb) segment.diagram = Diagram.subject;
                            else segment.diagram = Diagram.predicate;
                        }
                    }
                }
            }

            return concepts;
        }

        public static string toString(clsConcept concept) 
        {

            return "";
        }



    }
}
