using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsPattern
    {
        private string _text = null;
        private List<clsConcept> _concepts = new List<clsConcept>();

        public clsPattern(string text, clsConcept concept = null)
        {
            this._text = text;
            if (concept == null) concept = new clsConcept(this);
            linkConcept(concept);
        }

        public string text
        {
            get
            {
                return _text;
            }

        }

        public List<clsConcept> concepts
        {
            get
            {
                // create new list to return incase they muck with it
                List<clsConcept> concepts = new List<clsConcept>();
                foreach (clsConcept concept in this._concepts)
                {
                    concepts.Add(concept);
                }
                return concepts;
            }
        }

        public long totalConcepts
        {
            get
            {
                return _concepts.Count();
            }
        }

        public void linkConcept(clsConcept concept)
        {
            if (!_concepts.Contains(concept))
            {
                _concepts.Add(concept);
                concept.connectPattern(this);
            }
        }

        public List<clsRelationship> objectRelationships(clsConcept typeConcept = null, clsConcept objectConcept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsConcept _concept in _concepts)
            {
                List<clsRelationship> subRelationships = _concept.objectRelationships(typeConcept, objectConcept);

                foreach (clsRelationship subRelationship in subRelationships)
                {
                    relationships.Add(subRelationship);
                }
            }
            return relationships;
        }

        public List<clsRelationship> subjectRelationships(clsConcept typeConcept = null, clsConcept subjectConcept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsConcept _concept in _concepts)
            {
                List<clsRelationship> subRelationships = _concept.subjectRelationships(typeConcept, subjectConcept);

                foreach (clsRelationship subRelationship in subRelationships)
                {
                    relationships.Add(subRelationship);
                }
            }
            return relationships;
        }

        public clsConcept firstConcept
        {
            get
            {
                // should always be a concept so we should be able to delete this if
                if (_concepts.Count > 0) return _concepts[0];
                else return null;
            }
        }

        public string toJSON(bool showChildren = false)
        {
            string delimiter;
            string result = "{";
            result += "\"text\":\"" + this.text + "\"";

            if (showChildren)
            {
                delimiter = "";
                result += ",\"linkedConcepts\":[";
                foreach (clsConcept concept in this.concepts)
                {
                    result += delimiter + concept.toJSON();
                    delimiter = ",";
                }
                result += "]";
            }

            result += "}";
            return result;
        }
    }
}
