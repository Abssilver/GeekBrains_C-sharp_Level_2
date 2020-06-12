using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
Дана коллекция List<T>. Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:

для целых чисел;
* для обобщенной коллекции;
** используя Linq.

 */
namespace Lesson_4_Task_2_Pavel_Remizov
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<int> intCollection = new List<int>();
            for (int i = 0; i < 20; i++)
                intCollection.Add(random.Next(0, 10));
            foreach (var element in intCollection)
                Console.Write($"{element} ");
            Console.WriteLine("\n***** integers *****");

            IntCollection(intCollection);
            Console.ReadKey();

            Console.WriteLine("\n***** GENERICS *****");
            Item sword = new Item(0, "Sword", 5);
            Item bow = new Item(1, "Bow", 3);
            Item knife = new Item(2, "Knife", 3);
            Item staff = new Item(3, "Staff", 7);

            List<Item> items = new List<Item>()
            {
                sword, bow, knife, knife, bow, staff, staff, bow, knife, sword, sword, sword
            };
            foreach (var element in items)
                Console.WriteLine($"id = {element.id, -3} name = {element.name, -7} power = {element.power, -3}");
            GenericCollection(items);
            Console.ReadKey();
            Console.WriteLine("\n***** LINQ *****");

            LINQ(items);
            Console.ReadKey();
        }
        static void IntCollection(IEnumerable<int> collection)
        {
            SortedDictionary<int, int> frequency = new SortedDictionary<int, int>();
            foreach (var item in collection)
            {
                if (frequency.ContainsKey(item))
                    frequency[item]++;
                else
                    frequency.Add(item, 1);
            }
            foreach (var number in frequency)
                Console.WriteLine($"{number.Key} is presented {number.Value} time(s).");
            //Console.WriteLine(new string('*', 10));
            //GenericCollection(intCollection);
        }
        static void GenericCollection<T>(IEnumerable<T> collection)
        {
            Dictionary<T, int> frequency = new Dictionary<T, int>();
            foreach (var item in collection)
            {
                if (frequency.ContainsKey(item))
                    frequency[item]++;
                else
                    frequency.Add(item, 1);
            }
            /*
            foreach (var item in frequency)
            {
                FieldInfo[] fields = item.Key.GetType().GetFields();
                string output = string.Empty;
                if (item.Key.GetType().GetFields().Length > 0)
                    foreach (var field in fields)
                    output = string.Join(" ", output, field);
                Console.WriteLine($"{output} is presented {item.Value} time(s).");
            }
            */
            foreach (var item in frequency)
                Console.WriteLine($"{item.Key} is presented {item.Value} time(s).");
        }
        static void LINQ<T>(IEnumerable<T> collection)
        {
            var frequencyCollection = collection.Distinct().Select(x => new { x, frequency = collection.Where(y => y.Equals(x)).Count() });
            foreach (var element in frequencyCollection)
                Console.WriteLine($"{element.x} is presented {element.frequency} time(s).");
        }
    }
    class Item
    {
        public int id;
        public string name;
        public double power;
        public Item(int id, string name, double power)
        {
            this.id = id;
            this.name = name;
            this.power = power;
        }
    }
}
