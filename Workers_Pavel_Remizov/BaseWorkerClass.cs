using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_Pavel_Remizov
{
    public abstract class BaseWorkerClass
    {
        public string Name { get; }
        public string Surname { get; }
        public int Age { get; }
        protected BaseWorkerClass(string firstName, string lastName, int age)
        {
            Name = firstName;
            Surname = lastName;
            Age = age;
        }
        //Описать в базовом классе абстрактный метод для расчета среднемесячной заработной платы. 
        public abstract decimal AverageMonthlyWagesCalculation();
    }
}
