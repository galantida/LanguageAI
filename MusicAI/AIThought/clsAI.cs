using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsAI
    {
        public clsMemory memory;
        public clsComprehension comprehension;

        public string debug = "";
        public string response = "";


        public clsAI()
        {
            memory = new clsMemory();
            clsMemory.load(ref memory);
            comprehension = new clsComprehension(memory);
        }

        public void send(string text)
        {
            response = "";

            comprehension.comprehend(text);

            // first sentence
            clsFragment firstClause = comprehension.page.paragraphs[0].sentences[0].fragments[0];
            if ((firstClause.interrogative) || (!firstClause.interrogative))
            {
                string firstWord = null;
                string secondWord = null;
                string thirdWord = null;

                firstWord = firstClause.segments[0].text;
                if (firstClause.segments.Count > 1) secondWord = firstClause.segments[1].text;
                if (firstClause.segments.Count > 2) thirdWord = firstClause.segments[2].text;

                if (firstClause.segments.Count > 2)
                {
                    // learn three word statment
                    long concepts = memory.totalConcepts;
                    memory.recallRelationship(firstWord, secondWord, thirdWord);
                    debug += "Learned " + (memory.totalConcepts - concepts) + " new concepts.\r\n";
                }
                else if (firstClause.segments.Count > 1)
                {
                    // interogative sentence
                    clsConcept concept = memory.recall(firstWord);

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
                }
                else if (firstClause.segments.Count > 0)
                {
                    // concept question
                    clsConcept concept = memory.recall(firstWord);
                    foreach (clsRelationship relationship in concept.objectRelationships(null))
                    {
                        response = concept.text + " " + relationship.relationshipType + " " + relationship.objectConcept.text + "\r\n";
                    }
                }

            }
        }

        public void threeWordSentence(clsSentence sentence)
        {

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
