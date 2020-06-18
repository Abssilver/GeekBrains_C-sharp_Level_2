using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployee.Model
{
    public class Employee : ICloneable
    {
        private static int _id;
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public string Department 
        { 
            get => _department.Name; 
            set => _department.Name = value; 
        }
        Department _department;

        public Employee() 
        {
        }
        public Employee(string name, string surname, int age, double salary, string department)
        {
            Id = _id++;
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            _department = new Department(department);
        }
        public object Clone()
        {
            return new Employee()
            {
                Id = this.Id,
                Name = this.Name,
                Surname = this.Surname,
                Age = this.Age,
                Salary = this.Salary,
                Department = this.Department
            };
        }
    }
}
