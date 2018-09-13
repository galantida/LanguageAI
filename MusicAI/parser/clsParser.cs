using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    // A noun is the name of a person, place, thing, or idea.
    // A pronoun is a word used in place of a noun.
    // A verb expresses action or being.
    // An adjective modifies or describes a noun or pronoun.
    // An adverb modifies or describes a verb, an adjective, or another adverb.
    // A preposition is a word placed before a noun or pronoun to form a phrase modifying another word in the sentence.
    // A conjunction joins words, phrases, or clauses.
    // An interjection is a word used to express emotion.

    public class clsParser
    {
        public clsPage page = null;

        public clsParser()
        {
        }

        // single page parsing
        public void parseDocumentString(string documentString)
        {
            // parse new page
            page = new clsPage(documentString);
        }

        public string toString(OutputType outputType)
        {
            return page.toString(outputType);
        }

        public string toJSON()
        {
            return page.toJSON();
        }
    }
}
