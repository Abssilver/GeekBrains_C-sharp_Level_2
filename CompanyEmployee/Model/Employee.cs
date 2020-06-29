using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployee.Model
{
    public class Employee : ICloneable, INotifyPropertyChanged
    {
        private static int _id;
        private string _name;
        private string _surname;
        private int _age;
        private double _salary;
        public int Id { get; private set; }
        public string Name 
        {
            get => _name;
            set
            {
                if (this._name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
                }
            }
        }
        public string Surname 
        {
            get => _surname;
            set
            {
                if (this._surname != value)
                {
                    _surname = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Surname)));
                }
            }
        }
        public int Age
        {
            get => _age;
            set
            {
                if (this._age != value)
                {
                    _age = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Age)));
                }
            }
        }
        public double Salary
        {
            get => _salary;
            set
            {
                if (this._salary != value)
                {
                    _salary = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Salary)));
                }
            }
        }
        public string Department 
        {
            get => _department?.Name;
            set
            {
                if (this._department.Name != value)
                {
                    _department.Name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Department)));
                }
            }
        }
        Department _department;

        public event PropertyChangedEventHandler PropertyChanged;

        public Employee() 
        {
        }
        public Employee(string name, string surname, int age, double salary, string department)
        {
            Id = _id++;
            _name = name;
            _surname = surname;
            _age = age;
            _salary = salary;
            _department = new Department(department);
        }
        public void FullCopy(Employee newData)
        {
            this.Id = newData.Id;
            this.Name = newData.Name;
            this.Surname = newData.Surname;
            this.Age = newData.Age;
            this.Salary = newData.Salary;
            this.Department = newData.Department;
        }
        public object Clone()
        {
            return new Employee()
            {
                Id = this.Id,
                _name = this.Name,
                _surname = this.Surname,
                _age = this.Age,
                _salary = this.Salary,
                _department = new Department(this.Department)
            };
        }
    }
}
