using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsClause
    {
        clsMemory memory;

        public clsSubjectPhrase subjectPhrase;
        public clsPredicatePhrase predicatePhrase;
        public clsObjectPhrase objectPhrase;

        public clsClause(clsMemory memory)
        {
            this.memory = memory;
        }

        public void diagram(clsSentence sentence)
        {
            List<clsSegment> subjectSegments = new List<clsSegment>();

            foreach (clsFragment fragment in sentence.fragments)
            {



            }
        }

        public static string toString(clsConcept concept) 
        {

            return "";
        }



    }
}
