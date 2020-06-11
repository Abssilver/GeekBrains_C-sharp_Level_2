using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleLinq exapmle = new ExampleLinq();
            exapmle.SamplingFromSeveralSources();
            Console.ReadKey();
        }
    }
    public struct User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age
        {
            get => _age;
            set => _age = value;
        }
        private int _age;
        public User(string firstName, string lastName) : this()
        {
            FirstName = firstName;
            LastName = lastName;

        }
    }
    internal class ExampleLinq
    {
        private readonly User[] _users;
        private readonly int[] _numbers;
        public ExampleLinq()
        {
            _users = new[]
            {
                new User("Roman", "Muratov") {Age = 18},
                new User("Ivan", "Petrov"){Age = 22},
                new User("Igor", "Ivanov"){Age = 25},
                new User("Lera", "Muratova"){Age = 17},
                new User("Sveta", "Petrova"){Age = 27},
                new User("Lena", "Ivanova"){Age = 33},
                new User("Lera", "Muratova"){Age = 17},
                new User("Sveta", "Petrova"){Age = 27},
                new User("Lena", "Ivanova"){Age = 33}
            };
            _numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        }

        public void Filtration()
        {
            IEnumerable<int> evens = from i in _numbers
                                     where i % 2 == 0 && i > 10
                                     select i;

            IEnumerable<int> evens1 = _numbers.Where(i => i % 2 == 0 && i > 10);

            foreach (int i in evens)
                Console.WriteLine(i);
        }

        public void SelectingComplexObjects()
        {
            var selectedUsers = from user in _users
                                where user.Age > 25
                                select user;

            var selectedUsers1 = _users.Where(u => u.Age > 25).ToList();

            foreach (User user in selectedUsers)
                Console.WriteLine("{0} - {1}", user.FirstName, user.Age);
        }

        public void Projection()
        {
            var names = _users.Select(u => u.FirstName).ToList();

            foreach (string user in names)
                Console.WriteLine(user);
        }
        public void ExampleLet()
        {
            var people = from u in _users
                         let age = u.Age <= 18 ? u.Age + (18 - u.Age) : u.Age
                         select new User(u.FirstName, u.LastName)
                         {
                             Age = age
                         };

            foreach (var user in people)
                Console.WriteLine("{0} - {1}", user.FirstName, user.Age);
        }

        public void SamplingFromSeveralSources()
        {
            var people = from user in _users
                         from number in _numbers
                         select new { Name = user.LastName, Number = number };

            foreach (var p in people)
                Console.WriteLine("{0} - {1}", p.Name, p.Number);
        }

        public void Sorting()
        {
            var sortedUsers = from u in _users
                                  // ascending (сортировка по возрастанию) и descending (сортировка по убыванию)
                              orderby u.Age
                              select u;

            // ThenByDescending() (для сортировки по убыванию)
            var result = _users.OrderBy(u => u.FirstName).ThenBy(u => u.Age).ThenBy(u => u.FirstName.Length);


            foreach (User u in sortedUsers)
                Console.WriteLine("{0} - {1}", u.FirstName, u.Age);
        }

        public void WorkingWithSets()
        {
            string[] peopleFromAstrakhan = { "Igor", "Roman", "Ivan" };
            string[] peopleFromMoscow = { "Roman", "Vitalik", "Denis" };

            // Разность множеств
            var result = peopleFromAstrakhan.Except(peopleFromMoscow);
            // Пересечение множеств
            var result1 = peopleFromAstrakhan.Intersect(peopleFromMoscow);
            // Объединение множеств
            var result2 = peopleFromAstrakhan.Union(peopleFromMoscow);
            // Удаление дубликатов
            var result3 = peopleFromAstrakhan.Concat(peopleFromMoscow).Distinct();
        }

        public void ExampleAverage()
        {
            int min1 = _numbers.Min();
            // Минимальный возраст
            int min2 = _users.Min(n => n.Age);

            int max1 = _numbers.Max();
            // Максимальный возраст
            int max2 = _users.Max(n => n.Age);

            double avr1 = _numbers.Average();
            // Средний возраст
            double avr2 = _users.Average(n => n.Age);
        }

        public void ExampleSkipAndTake()
        {
            // Три первых элемента
            var result = _numbers.Take(3);
            // Все элементы, кроме первых трех                     
            var result1 = _numbers.Skip(3);

            foreach (var t in _users.TakeWhile(x => x.FirstName.StartsWith("I")))
                Console.WriteLine(t);

            foreach (var t in _users.SkipWhile(x => x.FirstName.StartsWith("I")))
                Console.WriteLine(t);
        }

        public void ExampleGrouping()
        {
            var groups = from user in _users
                         group user by user.LastName;

            // foreach (IGrouping<string, User> g in groups)
            // {
            //    Console.WriteLine(g.Key);
            //    foreach (var t in g)
            //        Console.WriteLine(t.FirstName);
            //    Console.WriteLine();
            // }

            var groups1 = _users.GroupBy(p => p.LastName)
                        .Select(g => new { LastName = g.Key, Count = g.Count() });

            var groups2 = _users.GroupBy(p => p.LastName)
                        .Select(g => new
                        {
                            LastName = g.Key,
                            Count = g.Count(),
                            Name = g.Select(p => p)
                        });

            foreach (var g in groups2)
            {
                Console.WriteLine(g.LastName);
                Console.WriteLine(g.Count);
                foreach (var t in g.Name)
                    Console.WriteLine(t.FirstName);
                Console.WriteLine();
            }
        }

        public void ExampleAllAndAny()
        {
            bool result = _users.All(u => u.Age > 20);
            Console.WriteLine(result
                ? "У всех пользователей возраст больше 20"
                : "Есть пользователи с возрастом меньше 20");

            bool result1 = _users.Any(u => u.Age < 20);
            Console.WriteLine(result1
                ? "Есть пользователи с возрастом меньше 20"
                : "У всех пользователей возраст больше 20");
        }
    }
}


