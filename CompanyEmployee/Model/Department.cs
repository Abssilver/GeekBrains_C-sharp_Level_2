using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployee.Model
{
    public class Department
    {
        private static List<string> _departments = new List<string>();
        public string Name { get; set; }
        public static List<string> DepartmentsName => _departments;
        public Department(string departmentName)
        {
            AddDepartment(departmentName);
            Name = departmentName;
        }
        public static void AddDepartment(string departmentToAdd)
        {
            if (!_departments.Contains(departmentToAdd))
                _departments.Add(departmentToAdd);
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
