using ExtensionMethod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod.ExtensionMethods
{
    public static class RhymesExtension
    {
        public static List<string> RhymesWith(this string matchRymes)
        {
            return new List<string> {"Your life ain't a thing but sham!", "Your girl earns money on a web cam", "I'll put your light out like a reactor SCRAM" };
        }
        public static int Double(this int DoubleMe)
        {
            return DoubleMe * 2;
        }
        public static List<WordDictionary> Censor(this List<WordDictionary> DictionaryToCensor)
        {
            List<WordDictionary> censoredDictionary = DictionaryToCensor.Where(x=>x.Explicit==false).ToList();
            return censoredDictionary;
        }



    }
}
