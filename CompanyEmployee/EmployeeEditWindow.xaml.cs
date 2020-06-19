using CompanyEmployee.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompanyEmployee
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window, INotifyPropertyChanged
    {
        private Employee _editableObject = new Employee();
        public ObservableCollection<string> DepartmentData { get; set; } = new ObservableCollection<string>();
        public string EmployeeDepartment
        { 
            get => _editableObject.Department;
            set
            {
                if (!this._editableObject.Department.Equals(value))
                {
                    _editableObject.Department = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.EmployeeDepartment)));
                }
            }
        }
        public Employee EmployeeData
        { 
            get => _editableObject;
            set
            {
                if (!this._editableObject.Equals(value))
                {
                    _editableObject = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.EmployeeData)));
                }
            }
        }
        public EmployeeEditWindow()
        {
            InitializeComponent();
        }
        public EmployeeEditWindow(Employee employeeToEdit) : this()
        {
            Department.DepartmentsName.ForEach(data => DepartmentData.Add(data));
            EmployeeData = employeeToEdit.Clone() as Employee;
            //сbDepartment.SelectedValue = EmployeeDepartment;
            //cbDepartment.ItemsSource = DepartmentData;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
