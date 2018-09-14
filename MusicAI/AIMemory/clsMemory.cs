using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{

    public class clsMemory
    {

        // bill is human - set is link (bill get all links of human)
        // bill is not stupid - set is link (bill get all links of stupid)
        // bill is friendly - set link (bill looses links of friendly)
        // bill can run - link to parent category (or List)

        // searching "bill is" "is friendly"

        // lists can be (and list) or (or lists)

        // concept verb concept
        private List<clsConcept> _concepts = new List<clsConcept>();

        /*********************************************************************
        * private calls learning and linking basic concepts
        *********************************************************************/

        // learn a single concept
        private clsConcept learn(string conceptString)
        {
            clsConcept response = _recall(conceptString); // check does not already exist for safty
            if (response == null)
            {
                response = new clsConcept(conceptString);
                _concepts.Add(response);
            }
            return response;
        }

        // link two concepts
        private clsRelationship learn(clsConcept concept, string verbString, clsConcept indirectConcept)
        {
            return concept.connectObject(verbString, indirectConcept);
        }

        /*********************************************************************
        * private calls for reading memory
        *********************************************************************/

        // get a specific concept
        private clsConcept _recall(string conceptString)
        {
            foreach (clsConcept concept in _concepts)
            {
                if (concept.text == conceptString) return concept;
            }
            return null;
        }

        // get a list of concepts
        private List<clsConcept> _recall(List<string> conceptStrings)
        {
            List<clsConcept> response = new List<clsConcept>();
            foreach (clsConcept concept in _concepts)
            {
                if (conceptStrings.Contains(concept.text)) response.Add(concept);
            }
            return response;
        }

        /*********************************************************************
         * public calls for reading and writing memory and recording the experience
         *********************************************************************/

        // lookup an existing or create a new concept
        public clsConcept recall(string conceptString)
        {
            clsConcept response = _recall(conceptString);
            if (response == null)
            {
                response = learn(conceptString);
            }
            return response;
        }

        // lookup exiting or create new concepts
        public List<clsConcept> recall(List<string> conceptStrings)
        {
            // get all existing concepts that match
            List<clsConcept> concepts = _recall(conceptStrings);

            // loop through conceptstrings
            foreach (string conceptString in conceptStrings)
            {
                // loop through the matching concepts
                bool found = false;
                foreach (clsConcept concept in concepts)
                {
                    if (conceptString == concept.text)
                    {
                        found = true;
                        break;
                    }
                }

                // if this concept does not exist then create it
                if (found == false)
                {
                    concepts.Add(learn(conceptString));
                }
            }

            return concepts;
        }

        

        // *******************************
        // recall compound concepts
        // *******************************
        public clsRelationship recallRelationship(clsConcept concept, string verbString, clsConcept childConcept)
        {
            return concept.connectObject(verbString, childConcept);
        }

        // auto string conversions
        public clsRelationship recallRelationship(string conceptString, string verbString, string childConceptString)
        {
            clsConcept concept = this.recall(conceptString);
            return concept.connectObject(verbString, this.recall(childConceptString));
        }

        // auto string conversions
        public clsRelationship recall(string conceptString, string verbString, clsConcept childConcept)
        {
            clsConcept concept = this.recall(conceptString);
            return concept.connectObject(verbString, childConcept);
        }

        // auto string conversions
        public clsRelationship recall(clsConcept concept, string verbString, string chilrdConceptString)
        {
            return concept.connectObject(verbString, this.recall(chilrdConceptString));
        }

        // *******************************
        // encounter compound concept lists
        // *******************************
        public List<clsRelationship> recallRelationShips(List<clsConcept> concepts, List<string> verbStrings, List<clsConcept> childConcepts)
        {
            List<clsRelationship> response = new List<clsRelationship>();

            foreach (clsConcept concept in concepts)
            {
                foreach (clsConcept childConcept in childConcepts)
                {
                    foreach (string verbString in verbStrings)
                    {
                        response.Add(learn(concept, verbString, childConcept));
                    }
                }
            }
            return response;
        }

        public List<clsRelationship> recallRelationships(List<string> conceptStrings, List<string> verbStrings, List<clsConcept> childConcepts)
        {
            return recallRelationShips(this.recall(conceptStrings), verbStrings, childConcepts);
        }

        public List<clsRelationship> recallRelationships(List<clsConcept> concepts, List<string> verbStrings, List<string> childConceptStrings)
        {
            return recallRelationShips(concepts, verbStrings, this.recall(childConceptStrings));
        }

        public List<clsRelationship> recallRelationships(List<string> conceptStrings, List<string> verbStrings, List<string> childConceptStrings)
        {
            return recallRelationShips(this.recall(conceptStrings), verbStrings, this.recall(childConceptStrings));
        }

        public List<clsRelationship> recallRelationships(List<string> conceptStrings, string verbString, List<clsConcept> childConcepts)
        {
            return recallRelationShips(this.recall(conceptStrings), new List<string>(new string[] { verbString }), childConcepts);
        }

        public List<clsRelationship> recallRelationships(List<clsConcept> concepts, string verbString, List<string> childConceptStrings)
        {
            return recallRelationShips(concepts, new List<string>(new string[] { verbString }), this.recall(childConceptStrings));
        }

        public List<clsRelationship> recallRelationships(List<string> conceptStrings, string verbString, List<string> childConceptStrings)
        {
            return recallRelationShips(this.recall(conceptStrings), new List<string>(new string[] { verbString }), this.recall(childConceptStrings));
        }

        public List<clsRelationship> recallRelationships(List<string> conceptStrings, string verbString, string childConceptString)
        {
            return recallRelationShips(this.recall(conceptStrings), new List<string>(new string[] { verbString }), this.recall(new List<string>(new string[] { childConceptString })));
        }

        public List<clsRelationship> recallRelationships(string conceptString, string verbString, string childConceptString)
        {
            return recallRelationShips(this.recall(new List<string>(new string[] { conceptString })), new List<string>(new string[] { verbString }), this.recall(new List<string>(new string[] { childConceptString })));
        }


        /*********************************************************************
        * public calls for general access
        *********************************************************************/
        public long totalConcepts
        {
            get
            {
                return _concepts.Count();
            }
        }
        public string diagnostic
        {
            get
            {
                string result = "\r\n";
                result += "Memory Diagnostics.\r\n";

                // concepts
                result += _concepts.Count() + " concepts.\r\n";

                // relation ships
                long relationshipCount = 0;
                foreach (clsConcept concept in _concepts)
                {
                    relationshipCount += concept.objectRelationships(null).Count;
                }
                result += relationshipCount + " relationships.\r\n";

                return result;
            }
        }


        public static void load(ref clsMemory memory)
        {
            // simple tense verbs do not use helping verbs except for will or shall
            // no helping verbs

            memory.recallRelationships(new List<string>(new string[] { "noun", "verb", "pronoun", "proper noun", "ajective", "adverb", "preposition", "conjunction", "determiner" }), "is", "Part of Speech");

            // perfect tense (have has or had helping verb) will is used to show future e.g. will have
            memory.recallRelationships(new List<string>(new string[] { "being", "be", "does", "do", "has", "have", "having" }), "is", new List<string>(new string[] { "verb", "helping", "present tense" })); // 'be, do, has' present modifyable future with will
            memory.recallRelationships(new List<string>(new string[] { "been", "did", "had" }), "is", new List<string>(new string[] { "verb", "helping", "past tense" }));

            // progressive tense ( is, are, was, were helping verbs with ing added to main verb) will is used to show future e.g. will be 
            // states of be
            memory.recallRelationships(new List<string>(new string[] { "is", "am" }), "is", new List<string>(new string[] { "verb", "helping", "present tense" }));
            memory.recallRelationships(new List<string>(new string[] { "are" }), "is", new List<string>(new string[] { "verb", "helping", "present tense", "plural" }));
            memory.recallRelationships(new List<string>(new string[] { "was" }), "is", new List<string>(new string[] { "verb", "helping", "past tense" }));
            memory.recallRelationships(new List<string>(new string[] { "were" }), "is", new List<string>(new string[] { "verb", "helping", "past tense", "plural" }));

            // modal auxiliar
            memory.recallRelationships(new List<string>(new string[] { "must", "ought to", "should" }), "is", new List<string>(new string[] { "verb", "helping" }));
            memory.recallRelationships(new List<string>(new string[] { "can" }), "is", new List<string>(new string[] { "verb", "helping", "present tense" }));
            memory.recallRelationships(new List<string>(new string[] { "could", "may", "might", "shall", "will" }), "is", new List<string>(new string[] { "verb", "helping", "future tense" }));


            // prepositions - A preposition is a word such as after, in, to, on, and with. Prepositions are usually used in front of nouns or pronouns and they show the relationship between the noun or pronoun and other words in a sentence
            memory.recallRelationships(new List<string>(new string[] { "to", "of", "in", "for", "on", "with", "at", "by", "from", "up", "about", "into", "over", "after" }), "is", "preposition");

            // conjuctions
            memory.recallRelationships(new List<string>(new string[] { "and", "but", "if", "or" }), "is", "conjunction");

            // determiners - Determiners are words placed in front of a noun to make it clear what the noun refers to.
            memory.recallRelationships(new List<string>(new string[] { "a", "an", "the", "their" }), "is", new List<string>(new string[] { "determiner", "adjective" }));

            // genders pronouns (incomplete)
            memory.recallRelationship("male", "is", "gender");
            memory.recallRelationship("female", "is", "gender");
            memory.recallRelationships(new List<string>(new string[] { "he", "him" }), "is", "male");
            memory.recallRelationships(new List<string>(new string[] { "she", "her" }), "is", "female");
            memory.recallRelationships(new List<string>(new string[] { "they", "them", "we", "us" }), "is", "group");
            memory.recallRelationships(new List<string>(new string[] { "it" }), "is", "thing");

            // unknown
            memory.recallRelationships(new List<string>(new string[] { "they", "them" }), "is", new List<string>(new string[] { "pronoun", "plural", "third person" }));

            // personal pronouns
            memory.recallRelationships(new List<string>(new string[] { "i" }), "is", new List<string>(new string[] { "pronoun", "personal", "singular", "first person", "subject" }));
            memory.recallRelationships(new List<string>(new string[] { "me" }), "is", new List<string>(new string[] { "pronoun", "personal", "singular", "first person", "object" }));
            memory.recallRelationships(new List<string>(new string[] { "we" }), "is", new List<string>(new string[] { "pronoun", "personal", "plural", "first person", "subject" }));
            memory.recallRelationships(new List<string>(new string[] { "us" }), "is", new List<string>(new string[] { "pronoun", "personal", "plural", "first person", "object" }));

            memory.recallRelationships(new List<string>(new string[] { "you" }), "is", new List<string>(new string[] { "pronoun", "personal", "singular", "second person" }));

            memory.recallRelationships(new List<string>(new string[] { "she", "he" }), "is", new List<string>(new string[] { "pronoun", "personal", "singular", "third person", "subject" }));
            memory.recallRelationships(new List<string>(new string[] { "her", "him" }), "is", new List<string>(new string[] { "pronoun", "personal", "singular", "third person", "object" }));
            memory.recallRelationships(new List<string>(new string[] { "it" }), "is", new List<string>(new string[] { "pronoun", "personal", "singular", "third person" }));

            // relative pronouns
            memory.recallRelationships(new List<string>(new string[] { "that", "which", "who", "whom", "whose", "whichever", "whoever", "whomever" }), "is", new List<string>(new string[] { "pronoun", "relative" }));

            // demonstrative
            memory.recallRelationships(new List<string>(new string[] { "this", "that" }), "is", new List<string>(new string[] { "pronoun", "demonstrative", "singular" })); // near
            memory.recallRelationships(new List<string>(new string[] { "these", "those" }), "is", new List<string>(new string[] { "pronoun", "demonstrative", "plural" })); // far

            // indefinite
            memory.recallRelationships(new List<string>(new string[] { "anybody", "anyone", "anything", "each", "either", "every", "everyone", "everything", "neither", "nobody", "no one", "nothing", "one", "sombody", "someone", "something" }), "is", new List<string>(new string[] { "pronoun", "indefinite", "singular" }));
            memory.recallRelationships(new List<string>(new string[] { "both", "few", "many", "several", "everybody" }), "is", new List<string>(new string[] { "pronoun", "indefinite", "plural" }));
            memory.recallRelationships(new List<string>(new string[] { "all", "any", "most", "none", "some" }), "is", new List<string>(new string[] { "pronoun", "indefinite" }));

            // reflexive
            memory.recallRelationships(new List<string>(new string[] { "myself", "yourself", "himself", "herself", "itself" }), "is", new List<string>(new string[] { "pronoun", "reflexive", "singular" }));
            memory.recallRelationships(new List<string>(new string[] { "ourselves", "yourselves", "themselves" }), "is", new List<string>(new string[] { "pronoun", "reflexive", "plural" }));

            // interogative
            memory.recallRelationships(new List<string>(new string[] { "what", "who", "which", "whom", "whose" }), "is", new List<string>(new string[] { "pronoun", "interrogative" }));

            // posessive - used before nouns
            memory.recallRelationships(new List<string>(new string[] { "my", "your", "his", "her", "its" }), "is", new List<string>(new string[] { "pronoun", "possessive", "singular" }));
            memory.recallRelationships(new List<string>(new string[] { "our", "your", "their" }), "is", new List<string>(new string[] { "pronoun", "possessive", "plural" }));

            // posessive - used after nouns
            memory.recallRelationships(new List<string>(new string[] { "mine", "yours", "his", "hers" }), "is", new List<string>(new string[] { "pronoun", "possessive", "singular" }));
            memory.recallRelationships(new List<string>(new string[] { "ours", "yours", "theirs" }), "is", new List<string>(new string[] { "pronoun", "possessive", "plural" }));

            // reciprolcol
            memory.recallRelationships(new List<string>(new string[] { "each other", "one another" }), "is", new List<string>(new string[] { "pronoun", "reciprolcal", "plural" }));


            memory.recallRelationships(new List<string>(new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }), "is", "month");

            // words to ignor during the count
            //private List<string> prepositions = new List<string>(new string[] { "to", "of", "in", "for", "on", "with", "at", "by", "from", "up", "about", "into", "over", "after" });
            //private List<string> others = new List<string>(new string[] { "the", "and", "a", "that", "I", "it", "not", "he", "as", "you", "this", "but", "his", "they", "her", "she", "or", "an", "will", "my", "would", "there", "their" });


        }
    }
}
