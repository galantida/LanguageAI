using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using textParser;
using memoryRelative;


namespace textReader
{
    public enum PhraseType { unknown, noun, verb, gerund, infinitive, appositive, participial, prepostional, absolute };

    // A noun phrase consists of a noun and all its modifiers.
    // (starts with 'to' and the next word is a verb) An infinitive phrase is a noun phrase that begins with an infinitive. (e.g. to walk, to run)
    // (starts with a verb that ends in 'ing') A gerund phrase is simply a noun phrase that starts with a gerund. 
    // (start and/or ends with a comma) A non-essential appositive phrase restates a noun and consists of one or more words. (e.g. My favorite ice cream, chocolate, was there.)
    // (not supported) An esseantial appositive phrase restates a noun and consists of one or more words. (e.g. My favorite ice cream, chocolate, was there.)

    // A verb phrase consists of a verb and all its modifiers.
    // (starts with a verb that shows tense ending in 'ed' or 'ing' for example) A participial phrase begins with a past or present participle. 
    // (starts with a preposition) A prepositional phrase begins with a preposition and can act as a noun, an adjective or an adverb.
    // (not supported) An absolute phrase has a subject, but not an acting verb, so it cannot stand alone as a complete sentence. It modifies the whole sentence, not just a noun. (Picnic basket in hand, she set off for her date.)

    //          Direct
    //          Indirect
    //      Complements
    //      Modifiers

    // a phrase is a part of a clause
    public class clsPhrase
    {
        public List<clsPattern> patterns = new List<clsPattern>();
        public PhraseType type = PhraseType.unknown;

        private clsMemory memory;

        public clsPhrase(clsMemory memory)
        {
            this.memory = memory;
        }

        public clsPattern firstPattern
        {
            get
            {
                if (patterns.Count > 0) return patterns[0];
                else return null;
            }
        }





    }
}
