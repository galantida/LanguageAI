using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utils;

namespace memoryRelative
{
    public class clsConcept
    {
        private List<clsRelationship> _objectRelationships = new List<clsRelationship>(); // where this concept is the subject
        private List<clsRelationship> _subjectRelationships = new List<clsRelationship>(); // where this concept is the object
        private List<clsPattern> _patterns = new List<clsPattern>();


        // one link could have multiple indirect objects
        // example - is first person, is second person
        public clsConcept(clsPattern pattern)
        {
            this._patterns.Add(pattern);
        }

        public long totalRelationships
        {
            get
            {
                return _objectRelationships.Count() + _subjectRelationships.Count();
            }
        }

        public long totalPatterns
        {
            get
            {
                return _patterns.Count();
            }
        }


        // ***************************************************************
        #region Linking and Learning

        // assign a pattern to make this concept searchable
        public void connectPattern(clsPattern pattern)
        {
            if (!_patterns.Contains(pattern))
            {
                _patterns.Add(pattern);
                pattern.linkConcept(this);
            }
        }


        // make a child relationship between this subject and an object (subject is object)
        public clsRelationship connectSubject(clsConcept typeConcept, clsConcept subjectConcept)
        {
            clsRelationship relationship = this.subjectRelationship(typeConcept, subjectConcept);
            if (relationship == null)
            {
                relationship = new clsRelationship(subjectConcept, typeConcept, this);
                _subjectRelationships.Add(relationship);
                subjectConcept.connectObject(relationship);
            }
            return relationship;
        }

        // make a parent relationship between this object and a subject (subject is object)
        public clsRelationship connectObject(clsConcept typeConcept, clsConcept objectConcept)
        {
            clsRelationship relationship = this.objectRelationship(typeConcept, objectConcept);
            if (relationship == null)
            {
                relationship = new clsRelationship(this, typeConcept, objectConcept);
                _objectRelationships.Add(relationship);
                objectConcept.connectSubject(relationship);
            }
            return relationship;
        }

        // make a parent relationship where this object is the indirect one
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

        public void connectObject(clsRelationship relationship)
        {
            if (relationship.subjectConcept == this)
            {
                if (!_objectRelationships.Contains(relationship))
                {
                    _objectRelationships.Add(relationship);
                }
            }
        }

       

        #endregion


        #region Lookup and searches


        // recall a single parent relationship of this type to the specified concept
        public clsRelationship subjectRelationship(clsConcept typeConcept, clsConcept subjectConcept)
        {
            // get all relationships of this type
            foreach (clsRelationship relationship in this._subjectRelationships)
            {
                if ((relationship.subjectConcept == subjectConcept) && (relationship.typeConcept == typeConcept))
                {
                    return relationship;
                }
            }
            return null;
        }

        // recall a single child relationship of this type to the specified concept
        public clsRelationship objectRelationship(clsConcept typeConcept, clsConcept objectConcept)
        {
            // get all relationships of this type
            foreach (clsRelationship relationship in this._objectRelationships)
            {
                if ((relationship.objectConcept == objectConcept) && (relationship.typeConcept == typeConcept))
                {
                    return relationship;
                }
            }
            return null;
        }

        // *******************************************************
        // public concept relationship queries
        // *******************************************************
        public List<clsRelationship> objectRelationships(clsConcept typeConcept = null, clsConcept concept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsRelationship relationship in _objectRelationships)
            {
                if ((concept == null) || (concept == relationship.objectConcept))
                {
                    if ((typeConcept == null) || (typeConcept == relationship.typeConcept))
                    {
                        relationships.Add(relationship);
                    }
                }
            }
            return relationships;
        }

        public List<clsRelationship> subjectRelationships(clsConcept typeConcept = null, clsConcept subjectConcept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsRelationship relationship in _subjectRelationships)
            {
                if ((subjectConcept == null) || (subjectConcept == relationship.subjectConcept))
                {
                    if ((typeConcept == null) || (typeConcept == relationship.typeConcept))
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

        public clsPattern firstPattern
        {
            get
            {
                // concepts should always have patterns as well. We should be able to delete this if after a while
                if (_patterns.Count > 0) return _patterns[0];
                else return null;
            }
        }

        #endregion 

        public string toJSON()
        {
            string delimiter;
            string result = "{";
            result += "\"firstPattern\":" + JSONUtils.toJSON(this.firstPattern.toJSON(false));
            result += ",\"subjectRelationships\":[";
            delimiter = "";
            foreach (clsRelationship relationship in _subjectRelationships)
            {
                result += delimiter + JSONUtils.toJSON(relationship.toJSON(false));
                delimiter = ",";
            }
            result += "]";

            result += ",\"objectRelationships\":[";
            delimiter = "";
            foreach (clsRelationship relationship in _objectRelationships)
            {
                result += delimiter + JSONUtils.toJSON(relationship.toJSON(false));
                delimiter = ",";
            }
            result += "]";

            result += "}";

            return result;
        }

    }
}
