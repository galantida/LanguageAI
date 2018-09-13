using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsConcept
    {
        private List<clsRelationship> _childRelationships = new List<clsRelationship>();
        private List<clsRelationship> _parentRelationships = new List<clsRelationship>();
        

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

        // make a child relationship between this concept and one that is indirect to this one
        public clsRelationship connectChild(string verbString, clsConcept childConcept)
        {
            // when both parameters are passed only one item should be returned
            List<clsRelationship> relationships = childConcept.parentRelationships(verbString, this);

            // is there already a relationship
            clsRelationship relationship = null;
            if (relationships.Count > 0)
            {
                relationship = relationships[0];
            }
            else 
            {
                // create a relation ship
                relationship = new clsRelationship(this, verbString, childConcept);
                this._childRelationships.Add((relationship)); // add to our children
                childConcept.connectParent(relationship); // tell the child concept to link us as a parent
            }
            return relationship;
        }

        // make a parent relationship where this object is the indirect one
        public void connectParent(clsRelationship relationship)
        {
            if (relationship.childConcept == this) this._parentRelationships.Add(relationship);
        }


        // recall a single child relationship of this type to the specified concept
        public clsRelationship childRelationship(string verbString, clsConcept childConcept)
        {
            // get all relationships of this type
            foreach (clsRelationship relationship in this._childRelationships)
            {
                if ((relationship.childConcept == childConcept) && (relationship.relationshipType == verbString))
                {
                    return relationship;
                }
            }
            return null;
        }

        // recall a single parent relationship of this type to the specified concept
        public clsRelationship parentRelationship(string verbString, clsConcept concept)
        {
            // get all relationships of this type
            foreach (clsRelationship relationship in this._parentRelationships)
            {
                if ((relationship.childConcept == concept) && (relationship.relationshipType == verbString))
                {
                    return relationship;
                }
            }
            return null;
        }


        // *******************************************************
        // public concept relationship queries
        // *******************************************************
        public List<clsRelationship> childRelationships(clsConcept concept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsRelationship relationship in _childRelationships)
            {
                if ((concept == null) || (concept == relationship.childConcept))
                {
                    // limit scope to a single concept if passed
                    relationships.Add(relationship);
                }
            }
            return relationships;
        }

        public List<clsRelationship> childRelationships(string verbString = null, clsConcept concept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsRelationship relationship in _childRelationships)
            {
                if ((concept == null) || (concept == relationship.childConcept))
                {
                    if ((verbString == null) || (verbString == relationship.relationshipType))
                    {
                        relationships.Add(relationship);
                    }
                }
            }
            return relationships;
        }

        public List<clsConcept> childConcepts(string verbString = null, clsConcept childConcept = null)
        {
            // get filtered list of relationships
            List<clsRelationship> relationships = childRelationships(verbString, childConcept);

            // pull out all of the concepts
            List<clsConcept> concepts = new List<clsConcept>();
            foreach (clsRelationship relationship in relationships)
            {
                concepts.Add(relationship.childConcept);
            }
            return concepts;
        }

        public List<clsRelationship> parentRelationships(string verbString = null, clsConcept parentConcept = null)
        {
            List<clsRelationship> relationships = new List<clsRelationship>();
            foreach (clsRelationship relationship in _parentRelationships)
            {
                if ((parentConcept == null) || (parentConcept == relationship.parentConcept))
                {
                    if ((verbString == null) || (verbString == relationship.relationshipType))
                    {
                        relationships.Add(relationship);
                    }
                }
            }
            return relationships;
        }

        public List<clsConcept> parentConcepts(string verbString = null, clsConcept parentConcept = null)
        {
            // get filtered list of relationships
            List<clsRelationship> relationships = parentRelationships(verbString, parentConcept);

            // pull out all of the concepts
            List<clsConcept> concepts = new List<clsConcept>();
            foreach (clsRelationship relationship in relationships)
            {
                concepts.Add(relationship.parentConcept);
            }
            return concepts;
        }

    }
}
