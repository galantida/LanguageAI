using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using memoryRelative;
using textReader;


namespace artificialIntelligence
{
    public class clsAI
    {
        public clsMemory memory;
        public clsReader reader;

        public string debug = "";
        public string response = "";


        public clsAI()
        {
            memory = new clsMemory();
            reader = new clsReader(memory);

            // we need to is to start it all off
            memory.learn("is", "is", "verb");
        }

        public void hear(string text)
        {
            response = "";
            reader.read(text);

            // first clause
            clsClause clause = reader.clauses[0];

            switch (clause.type)
            {
                case ClauseType.interogative:
                    {
                        // asking a question


                        break;
                    }
                case ClauseType.declaritive:
                    {
                        // making a statment
                        foreach (clsPhrase subjectPhrase in clause.phrases) // subject loop
                        {
                            if (subjectPhrase.type == PhraseType.noun)
                            {
                                foreach (clsPhrase verbPhrase in clause.phrases) // subject loop
                                {
                                    if (verbPhrase.type == PhraseType.verb)
                                    {
                                        foreach (clsPhrase absolutePhrase in clause.phrases) // subject loop
                                        {
                                            if (absolutePhrase.type == PhraseType.absolute) // absolute is just a place holder
                                            {
                                                memory.learn(subjectPhrase.firstPattern.firstConcept, verbPhrase.firstPattern.firstConcept, absolutePhrase.firstPattern.firstConcept);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                case ClauseType.imperitive:
                    {
                        // giving direction


                        break;
                    }
            }
        }

        public void declaritive(clsClause clause)
        {
            // learn three word statment
            //memory.learn(firstWord, secondWord, thirdWord);
            //debug += "Learned " + (memory.totalConcepts - concepts) + " new concepts.\r\n";
        }

        public void interogative(clsClause sentence)
        {
            /*
            // interogative sentence (expecting response) use the question mark?
            clsConcept concept = memory.recallCreateConcept(firstWord);

            List<clsRelationship> relationships;
            if (concept.objectRelationship("is", memory.recall("verb")) == null)
            {
                // question Noun verb (David is?);
                relationships = concept.objectRelationships(secondWord);

                string delimiter = "";
                foreach (clsRelationship relationship in relationships)
                {
                    response += delimiter + relationship.objectConcept.text;
                    delimiter = ",";
                }
                response = firstWord + " " + secondWord + " " + response;
            }
            else
            {
                // question verb noun ( is David?)
                concept = memory.recall(secondWord);

                string delimiter = "";
                relationships = concept.subjectRelationships(firstWord);
                foreach (clsRelationship relationship in relationships)
                {
                    response += delimiter + relationship.subjectConcept.text;
                    delimiter = ",";
                }
                response += " " + firstWord + " " + secondWord + " ";
            }
            */
        }

        

        public string receve
        {
            get
            {
                return response;
            }
        }

        public string diagnostic
        {
            get
            {
                return memory.diagnostic;
            }
        }
    }
}
