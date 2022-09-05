using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod.Models
{
    public class WordDictionary
    {
        public string Word { get; set; }
        public bool Explicit { get; set; }
        public WordDictionary()
        {
        }
        public WordDictionary(string Word, bool Explicit)
        {
            this.Word = Word;
            this.Explicit = Explicit;
        }
    }
}
