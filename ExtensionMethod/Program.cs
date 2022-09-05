using ExtensionMethod.ExtensionMethods;
using ExtensionMethod.Models;

namespace ExtensionMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string extension method
            string EvaluateRymes = "Run your mouth and my Glock will go bum!";
            EvaluateRymes.Split(' ');
            EvaluateRymes.ToLower();
            EvaluateRymes.Contains("");
            List<string> myDopeRhymes = EvaluateRymes.RhymesWith();
            Console.WriteLine("-----------------****Rhymes***-----------------");
            Console.WriteLine($"My reply: {String.Join("\n", myDopeRhymes)}");


            //int extension method
            int number = 10;
            int numberDouble = number.Double();
            Console.WriteLine("-----------------****Double the Number***-----------------");
            Console.WriteLine($"Double of {number} is equals to: {numberDouble}");


            //List extension method
            List<WordDictionary> wordDictionaries = new List<WordDictionary>();
            wordDictionaries.Add(new WordDictionary("abc", false ));
            wordDictionaries.Add(new WordDictionary("def", false ));
            wordDictionaries.Add(new WordDictionary("dummyA1", true ));
            wordDictionaries.Add(new WordDictionary("dummyB2", true ));
            wordDictionaries.Add(new WordDictionary("ghi", false ));
            wordDictionaries.Add(new WordDictionary("jkl", false ));
            wordDictionaries.Add(new WordDictionary("mno", false ));
            wordDictionaries.Add(new WordDictionary("dummyC3", true ));
            wordDictionaries.Add(new WordDictionary("pqr", false ));
            wordDictionaries.Add(new WordDictionary("stu", false ));
            wordDictionaries.Add(new WordDictionary("vwx", false ));
            wordDictionaries.Add(new WordDictionary("yz", false ));
            string data = String.Join("", wordDictionaries.Censor().Select(x => x.Word));
            Console.WriteLine("-----------------****Dictionary Words***-----------------");
            Console.WriteLine($"Dictonary of English is: {data}");
            Console.WriteLine($"Dictonary of English length is: {data.Length}");


        }
    }
}