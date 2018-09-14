using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageReader
{
    public class clsComprehension : clsReader
    {
        public clsMemory memory;

        public clsWikipedia wiki;

        public clsComprehension(clsMemory memory): base(memory)
        {
            this.memory = memory;
        }

        public void researchTopic(string topic)
        {
            // create wiki lookup for this topic
            this.wiki = new clsWikipedia();
            this.wiki.lookup(topic);

            // read the lookup results
            base.page.parseDocumentString(wiki.text);
        }

        public void comprehend(string documentString)
        {
            base.parseDocumentString(documentString); // parsing
            base.read(); // word mapping

        }
    }
}
