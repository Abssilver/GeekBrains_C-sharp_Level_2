using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegate_Pavel_Remizov
{
    //* Добавить в пример Lesson3 обобщенный делегат.
    //public delegate void MyDelegate(object obj);
    //public delegate void MyDelegate<T>(T obj); // solution 1
    class Source
    {
        public event Action<object> GenericAction;
        //public event MyDelegate Run;
        //public event MyDelegate<object> Run; // solution 1
        public void Start()
        {
            Console.WriteLine("Run!");
            //Run?.Invoke(this); // solution 1
            GenericAction?.Invoke(this);
        }
    }
    class ObserverOne
    {
        public void Observe(object obj)
        {
            Console.WriteLine("{0}: They’re in the trees!", obj);
        }
    }
    class ObserverTwo
    {
        public void Observe(object obj)
        {
            Console.WriteLine("{0}: Calm down! Calm down!", obj);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Source source = new Source();
            ObserverOne first = new ObserverOne();
            ObserverTwo second = new ObserverTwo();
            //MyDelegate<object> myDel = new MyDelegate<object>(first.Observe);
            source.GenericAction += first.Observe;
            source.GenericAction += second.Observe;
            //source.Run += myDel;
            //source.Run += second.Observe;
            source.Start();
            source.GenericAction -= first.Observe;
            //source.Run -= myDel;
            source.Start();
            Console.ReadKey();
        }
    }
}
