using System;
using System.Collections.Generic;

/*
Построить  три  класса  (базовый  и  2  потомка),  
описывающих  работников  с  почасовой  оплатой  
(один  из  потомков)  и  фиксированной оплатой (второй потомок):
Описать в базовом классе абстрактный метод для расчета среднемесячной заработной платы. 
Для «повременщиков» формула для расчета такова: 
«среднемесячная заработная плата = 20.8 * 8 * почасовая ставка»; 
для  работников  с  фиксированной  оплатой: «среднемесячная заработная плата = фиксированная месячная оплата»;
Создать на базе абстрактного класса массив сотрудников и заполнить его;
* Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort();
* Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
*/
namespace Workers_Pavel_Remizov
{
    class Program
    {
        static BaseWorkerClass[] _workers = new BaseWorkerClass[30];
        static Random _random = new Random();
        static void Main(string[] args)
        {
            CreateWorkersArray();
            IComparer<BaseWorkerClass> ageComparer = new AgeComparer();
            Array.Sort(_workers, ageComparer);
            PrintWorkers();
            IComparer<BaseWorkerClass> wagesComparer = new WagesComparer();
            Array.Sort(_workers, wagesComparer);
            PrintWorkers();
            IComparer<BaseWorkerClass> nameComparer = new NameComparer();
            Array.Sort(_workers, nameComparer);
            PrintWorkers();

            Console.WriteLine(new string('*', 20));
            WorkerEnumerable workersEnumerable = new WorkerEnumerable(30);
            foreach (BaseWorkerClass worker in workersEnumerable)
                Console.WriteLine("{0, 10} {1, 10} {2, 4} {3, 10}",
                                  worker.Name, worker.Surname, worker.Age, worker.AverageMonthlyWagesCalculation());
            Console.ReadKey();
        }
        static void CreateWorkersArray()
        {
            for (int i = 0; i < _workers.Length / 2; i++)
                _workers[i] = new FixedWageWorker($"Muffin_{i}", $"Lover_{i}",
                                                  _random.Next(18, 30), _random.Next(30_000, 40_000));
            for (int i = _workers.Length / 2; i < _workers.Length; i++)
            {
                _workers[i] = new HourlyWorker($"Bold_{i}", $"Orange_{i}",
                                               _random.Next(18, 30), _random.Next(100, 200));
            }
        }
        static void PrintWorkers()
        {
            Console.WriteLine(new string('*', 10));
            foreach (var worker in _workers)
                Console.WriteLine("{0, 10} {1, 10} {2, 4} {3, 10}",
                    worker.Name, worker.Surname, worker.Age, worker.AverageMonthlyWagesCalculation());
        }
    }
    public class AgeComparer : IComparer<BaseWorkerClass>
    {
        public int Compare(BaseWorkerClass x, BaseWorkerClass y) => x.Age.CompareTo(y.Age);
    }
    public class WagesComparer : IComparer<BaseWorkerClass>
    {
        public int Compare(BaseWorkerClass x, BaseWorkerClass y) => 
            x.AverageMonthlyWagesCalculation().CompareTo(y.AverageMonthlyWagesCalculation());
    }
    public class NameComparer : IComparer<BaseWorkerClass>
    {
        public int Compare(BaseWorkerClass x, BaseWorkerClass y) => x.Name.CompareTo(y.Name);
    }
}
