using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployee.Model
{
    public class Department : INotifyPropertyChanged
    {
        private static Dictionary<int, string> _departments = new Dictionary<int, string>();
        private static int _id;
        private string _name;

        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; }
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
        public static List<string> DepartmentsName => _departments.Values.ToList();
        public Department(string departmentName)
        {
            AddDepartment(departmentName);
            _name = departmentName;
        }
        public static void AddDepartment(string departmentToAdd)
        {
            if (!_departments.ContainsValue(departmentToAdd) && !departmentToAdd.Equals(string.Empty))
            {
                _id++;
                _departments.Add(_id, departmentToAdd);
            }
        }
    }
}
