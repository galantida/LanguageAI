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
            //clsMemory.load(ref memory);
            memory.learn("is", "is", "verb"); // teach it is or equals?


            comprehension = new clsComprehension(memory);
        }

        public void send(string text)
        {
            response = "";

            comprehension.comprehend(text);

            // first sentence
            clsSentence sentence = comprehension.page.paragraphs[0].sentences[0];

            switch (sentence.type)
            {
                case SentenceType.interogative:
                    {
                        // asking a question


                        break;
                    }
                case SentenceType.declaritve:
                    {
                        // making a statment

                        break;
                    }
                case SentenceType.imperitive:
                    {
                        // giving direction


                        break;
                    }
            }
        }

        public void declaritive(clsSentence sentence)
        {
            // learn three word statment
            //memory.learn(firstWord, secondWord, thirdWord);
            //debug += "Learned " + (memory.totalConcepts - concepts) + " new concepts.\r\n";
        }

        public void interogative(clsSentence sentence)
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
