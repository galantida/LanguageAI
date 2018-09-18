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
        public clsConcept typeConcept;
        public clsConcept objectConcept;

        // examples 
        // David is Great (Subject Type Object)
        // David is a Human (Object Type Subject)
        public clsRelationship(clsConcept subjectConcept, clsConcept typeConcept, clsConcept objectConcept)
        {
            this.subjectConcept = subjectConcept;
            this.typeConcept = typeConcept;
            this.objectConcept = objectConcept;
        }

        public string toJSON(bool showChildren = false)
        {
            string result = "{";
            result += "\"subjectConcept\":" + subjectConcept.firstPattern.toJSON();
            result += ",\"typeConcept\":" + typeConcept.firstPattern.toJSON();
            result += ",\"objectConcept\":" + objectConcept.firstPattern.toJSON();
            result += "}";

            return result;
        }
    }
}
