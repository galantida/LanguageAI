using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsRelationship
    {
        public clsConcept parentConcept;
        public clsConcept childConcept;
        public string relationshipType;

        public clsRelationship(clsConcept parentConcept, string relationshipType, clsConcept childConcept)
        {
            this.parentConcept = parentConcept;
            this.relationshipType = relationshipType;
            this.childConcept = childConcept;
        }
    }
}
