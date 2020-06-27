using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
* Дан фрагмент программы:
Dictionary<string, int> dict = new Dictionary<string, int>()
  {
    {"four",4 },
    {"two",2 },
    { "one",1 },
    {"three",3 },
  };
     var d = dict.OrderBy(delegate(KeyValuePair<string,int> pair) { return pair.Value; });
     foreach (var pair in d)
    {
      Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
    }

а. Свернуть обращение к OrderBy с использованием лямбда-выражения =>.
b. * Развернуть обращение к OrderBy с использованием делегата .
*/
namespace Lesson_4_Task_3_Pavel_Remizov
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four", 4 },
                { "two", 2 },
                { "one", 1 },
                { "three", 3 },
            };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            Console.ReadKey();

            dict = new Dictionary<string, int>()
            {
                { "five", 5 },
                { "seven", 7 },
                { "eight", 8 },
                { "six", 6 },
            };

            var collapse = dict.OrderBy(x => x.Value);
            foreach (var item in collapse)
                Console.WriteLine("{0} - {1}", item.Key, item.Value);
            Console.ReadKey();

            dict = new Dictionary<string, int>()
            {
                { "ten", 10 },
                { "nine", 9 },
                { "twelve", 12 },
                { "eleven", 11 },
            };
            Func<KeyValuePair<string, int>, int> function = GetValue;
            //Sorting<string, int> sortingDelegate = GetValue;
            //IComparer<KeyValuePair<string, int>> comparer = new SortingDictionary();
            var expand = dict.OrderBy(function);
            foreach (var pair in expand)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            Console.ReadKey();
        }
        //delegate TValue Sorting <TKey, TValue>(KeyValuePair<TKey, TValue> pair);
        static TValue GetValue<TKey, TValue>(KeyValuePair<TKey, TValue> pair) => pair.Value;
        /*
        static IOrderedEnumerable<KeyValuePair<string, int>> Collapse(Dictionary<string, int> dict) =>
            dict.OrderBy(x => x.Value);
        */
    }
    /*
    class SortingDictionary : IComparer<KeyValuePair<string, int>>
    {
        public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y) => x.Value.CompareTo(y.Value);

    }
    */
}
