﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployee.Model
{
    public class Employee
    {
        private static int _id;
        public int Id { get; }
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
    public Employee(string name, string surname, int age, int salary, string department)
        {
            Id = ++_id;
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            _department = new Department(department);
        }
        public override string ToString()
        {
            return $"{Id,3}\t{Name,15}\t{Surname,15}\t{Age,4}\t{Salary,10}\t{_department}";
        }
    }
}
