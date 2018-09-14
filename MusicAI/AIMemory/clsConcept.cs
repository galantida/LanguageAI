using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsConcept
    {
        private List<clsRelationship> _objectRelationships = new List<clsRelationship>();
        private List<clsRelationship> _subjectRelationships = new List<clsRelationship>();
        

        // one link could have multiple indirect objects
        // example - is first person, is second person

        private string _text;
        public string text
        {
            get
            {
                return _text;
            }
        }

        public clsConcept(string text)
        {
            _text = text;
        }

        // recall a single parent relationship of this type to the specified concept
        public clsRelationship subjectRelationship(string verbString, clsConcept subjectConcept)
        {
            // get all relationships of this type
            foreach (clsRelationship relationship in this._subjectRelationships)
            {
                if ((relationship.subjectConcept == subjectConcept) && (relationship.relationshipType == verbString))
                {
                    return relationship;
                }
            }
            return null;
        }

        // recall a single child relationship of this type to the specified concept
        public clsRelationship objectRelationship(string verbString, clsConcept objectConcept)
        {
            // get all relationships of this type
            foreach (clsRelationship relationship in this._objectRelationships)
            {
                if ((relationship.objectConcept == objectConcept) && (relationship.relationshipType == verbString))
                {
                    return relationship;
                }
            }
            return null;
        }

        // make a child relationship between this subject and an object (subject is object)
        public clsRelationship connectSubject(string verbString, clsConcept subjectConcept)
        {
            clsRelationship relationship = this.subjectRelationship(verbString, subjectConcept);
            if (relationship == null)
            {
                relationship = new clsRelationship(subjectConcept, verbString, this);
                _subjectRelationships.Add(relationship);
                subjectConcept.connectObject(relationship);
            }
            return relationship;
        }

        // make a parent relationship between this object and a subject (subject is object)
        public clsRelationship connectObject(string verbString, clsConcept objectConcept)
        {
            clsRelationship relationship = this.objectRelationship(verbString, objectConcept);
            if (relationship == null)
            {
                relationship = new clsRelationship(this, verbString, objectConcept);
                _objectRelationships.Add(relationship);
                objectConcept.connectSubject(relationship);
            }
            return relationship;
        }

        // make a parent relationship where this object is the indirect one
        public void connectObject(clsRelationship relationship)
        {
            if (relationship.subjectConcept == this) {
                if (!_objectRelationships.Contains(relationship)) {
                    _objectRelationships.Add(relationship);
                }
            }
        }

        public void connectSubject(clsRelationship relationship)
        {
            if (relationship.objectConcept == this)
            {
                if (!_subjectRelationships.Contains(relationship))
                {
                    _subjectRelationships.Add(relationship);
                }
            }

        }


        // *******************************************************
        // public concept relationship queries
        // *******************************************************
        public List<clsRelationship> objectRelationships(clsConcept concept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsRelationship relationship in _objectRelationships)
            {
                if ((concept == null) || (concept == relationship.objectConcept))
                {
                    // limit scope to a single concept if passed
                    relationships.Add(relationship);
                }
            }
            return relationships;
        }

        public List<clsRelationship> objectRelationships(string verbString = null, clsConcept concept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsRelationship relationship in _objectRelationships)
            {
                if ((concept == null) || (concept == relationship.objectConcept))
                {
                    if ((verbString == null) || (verbString == relationship.relationshipType))
                    {
                        relationships.Add(relationship);
                    }
                }
            }
            return relationships;
        }

        public List<clsRelationship> subjectRelationships(string verbString = null, clsConcept subjectConcept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsRelationship relationship in _subjectRelationships)
            {
                if ((subjectConcept == null) || (subjectConcept == relationship.subjectConcept))
                {
                    if ((verbString == null) || (verbString == relationship.relationshipType))
                    {
                        relationships.Add(relationship);
                    }
                }
            }
            return relationships;
        }

        public static List<clsConcept> getObjectConcepts(List<clsRelationship> relationships)
        {
            // pull out all of the concepts
            List<clsConcept> concepts = new List<clsConcept>();
            foreach (clsRelationship relationship in relationships)
            {
                concepts.Add(relationship.objectConcept);
            }
            return concepts;
        }

        public static List<clsConcept> getSubjectConcepts(List<clsRelationship> relationships)
        {
            // pull out all of the concepts
            List<clsConcept> concepts = new List<clsConcept>();
            foreach (clsRelationship relationship in relationships)
            {
                concepts.Add(relationship.subjectConcept);
            }
            return concepts;
        }

    }
}
