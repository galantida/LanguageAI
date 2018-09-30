using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utils;

namespace memoryRelative
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

        public string toJSON(bool showAsObject = true)
        {
            string result = "";

            if (!showAsObject)
            {
                result += JSONUtils.JSONFriendly(subjectConcept.firstPattern.toJSON(false) + " " + typeConcept.firstPattern.toJSON(false) + " " + objectConcept.firstPattern.toJSON(false));
            }
            else
            {
                result = "{";
                result += JSONUtils.toJSON("relationShip", subjectConcept.firstPattern.toJSON(false) + " " + typeConcept.firstPattern.toJSON(false) + " " + objectConcept.firstPattern.toJSON(false));
                result += "}";
            }

            return result;
        }
    }
}
