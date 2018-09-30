using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memoryRelative
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
        private List<clsPattern> _patterns = new List<clsPattern>();

        /*********************************************************************
        * calls for learning and recalling patterns
        *********************************************************************/
        // learn a single concept
        public clsPattern learnPattern(string patternString)
        {
            clsPattern pattern = this.recallPattern(patternString);
            if (pattern == null)
            {
                pattern = new clsPattern(patternString);
                _patterns.Add(pattern);
            }
            return pattern;
        }

        public clsPattern recallPattern(string patternString)
        {
            foreach (clsPattern pattern in this._patterns)
            {
                if (pattern.text == patternString) return pattern;
            }
            return null;
        }

        public clsPattern recallCreatePattern(string patternString)
        {
            foreach (clsPattern pattern in this._patterns)
            {
                if (pattern.text == patternString) return pattern;
            }

            // learn pattern
            return learnPattern(patternString);
        }


        /*********************************************************************
        * calls for general learning
        *********************************************************************/
        public void learn(clsConcept subjectConcept, clsConcept typeConcept, clsConcept objectConcept)
        {
            clsRelationship relationship = subjectConcept.objectRelationship(typeConcept, objectConcept);
            if (relationship == null)
            {
                subjectConcept.connectObject(typeConcept, objectConcept);
            }
        }

        public void learn(List<clsConcept> subjectConcepts, List<clsConcept> typeConcepts, List<clsConcept> objectConcepts)
        {
            foreach (clsConcept subjectConcept in subjectConcepts)
            {
                foreach (clsConcept typeConcept in typeConcepts)
                {
                    foreach (clsConcept objectConcept in objectConcepts)
                    {
                        this.learn(subjectConcept, typeConcept, objectConcept);
                    }
                }
            }
        }

        public void learn(string subjectPatternString, string typePatternString, string objectPatternString)
        {
            clsPattern subjectPattern = this.recallCreatePattern(subjectPatternString);
            clsPattern typePattern = this.recallCreatePattern(typePatternString);
            clsPattern objectPattern = this.recallCreatePattern(objectPatternString);
            this.learn(subjectPattern.firstConcept, typePattern.firstConcept, objectPattern.firstConcept);
        }

        public void learn(List<string> subjectPatternStrings, List<string> typePatternStrings, List<string> objectPatternStrings)
        {
            foreach (string subjectPatternString in subjectPatternStrings)
            {
                foreach (string typePatternString in typePatternStrings)
                {
                    foreach (string objectPatternString in objectPatternStrings)
                    {
                        this.learn(subjectPatternString, typePatternString, objectPatternString);
                    }
                }
            }
        }


        /*********************************************************************
        * calls for general recolection
        *********************************************************************/
        public List<clsRelationship> recallObjectRelationship(clsPattern subjectPattern, clsPattern typePattern)
        {
            return subjectPattern.firstConcept.objectRelationships(typePattern.firstConcept);
        }

        public List<clsRelationship> recallSubjectRelationship(clsPattern typePattern, clsPattern objectPattern)
        {
            return objectPattern.firstConcept.subjectRelationships(typePattern.firstConcept);
        }

        // just returing the first one for now. Maybe later we can add context so it return oine relevant to the current conversasion
        public clsConcept recallConcept(string patternString)
        {
            foreach (clsPattern pattern in this._patterns)
            {
                if (pattern.text == patternString)
                {
                    if (pattern.concepts.Count > 0) return pattern.concepts[0];
                }
            }
            return null;
        }



        /*********************************************************************
        * public calls for general access
        *********************************************************************/
        public long totalPatterns
        {
            get
            {
                return _patterns.Count();
            }
        }
        public string diagnostic
        {
            get
            {
                string result = "\r\n";
                result += "Memory Diagnostics.\r\n";
                result += _patterns.Count() + " patterns.\r\n"; // patterns

                result = this.toJSON();
                return result;
            }
        }

        public string toJSON()
        {
            string delimiter = "";
            string result = "{\"memoryDump\":";

            delimiter = "";
            result += "[";
            foreach (clsPattern pattern in _patterns)
            {
                result += delimiter + pattern.toJSON(true);
                delimiter = ",";
            }
            result += "]";

            result += "}";

            return result;
        }


        /*
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
        */
    }
}
