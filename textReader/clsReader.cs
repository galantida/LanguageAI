using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using memoryRelative;
using textParser;

namespace textReader
{
    // A noun is the name of a person, place, thing, or idea.
    // A pronoun is a word used in place of a noun.
    // A verb expresses action or being.
    // An adjective modifies or describes a noun or pronoun.
    // An adverb modifies or describes a verb, an adjective, or another adverb.
    // A preposition is a word placed before a noun or pronoun to form a phrase modifying another word in the sentence.
    // A conjunction joins words, phrases, or clauses.
    // An interjection is a word used to express emotion.

    public enum Diagram { subject, predicate };


    // the purpose of teh reader is to take the break down from the parser and build it back up as diagramed sentences linked to meaningful concepts
    // reader is aware of the parts of speech gender and tence. 
    // It links words that it can to their parts of speech
    // Multi word titles and names are combined
    // 
    public class clsReader
    {
        private clsMemory memory;
        public clsParser parser;
        public List<clsClause> clauses;

        public clsReader(clsMemory memory)
        {
            this.memory = memory;
        }

        public void read(string documentString)
        {
            parser = new clsParser();
            parser.parseDocumentString(documentString);

            // clear current reader clauses
            clauses = new List<clsClause>();

            List<clsSegment> segments;
            foreach (clsParagraph paragraph in parser.page.paragraphs)
            {
                foreach (clsSentence sentence in paragraph.sentences)
                {
                    // combine fragment in to phrases
                    segments = new List<clsSegment>(); // for now we only read single clause sentences
                    foreach (clsFragment fragment in sentence.fragments)
                    {
                        // fragments are separate by ",  and ;" which might not nessisarily be what we need
                        foreach (clsSegment segment in fragment.segments)
                        {
                            // link to concepts
                            segments.Add(segment);
                        }
                    }

                    // add to Reader Clauses
                    clsClause clause = new clsClause(memory);
                    clause.readSegments(segments);
                    clauses.Add(clause);
                }
            }


        }


       

        /*public static string toPartsOfSpeech()
        {
            if (this.isPunctuation) return this.text;

            switch (outputType)
            {
                case OutputType.toString:

                    return "\"" + this.text + "\"";

                case OutputType.toPartsOfSpeech:

                    if (this.isPunctuation) return this.text;
                    return "( '" + this.text + "' " + this.partsOfSpeech + " )";

                case OutputType.toDiagram:

                    if (this.isPunctuation) return this.text;
                    return "( '" + this.text + "' " + this.diagram + " )";

                default:

                    return null;
            }
            return "";
        }
        */
    }
}
