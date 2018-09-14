using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsRelationship
    {
        public clsConcept subjectConcept;
        public clsConcept objectConcept;
        public string relationshipType;

        public clsRelationship(clsConcept subjectConcept, string relationshipType, clsConcept objectConcept)
        {
            this.subjectConcept = subjectConcept;
            this.relationshipType = relationshipType;
            this.objectConcept = objectConcept;
        }
    }
}
