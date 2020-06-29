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
        private Employee _originEmployeeLink;
        private string _department = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> DepartmentData { get; set; } = new ObservableCollection<string>();
        public string EmployeeDepartment
        { 
            get => _department;
            set
            {
                if (!this._department.Equals(value))
                {
                    _department = value;
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
            _originEmployeeLink = employeeToEdit;
            Department.DepartmentsName.ForEach(data => DepartmentData.Add(data));
            EmployeeData = employeeToEdit.Clone() as Employee;
            EmployeeDepartment = EmployeeData.Department;
            //сbDepartment.SelectedValue = EmployeeDepartment;
            //cbDepartment.ItemsSource = DepartmentData;
        }
        private void ApplyChanges(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = true;
            _originEmployeeLink.FullCopy(_editableObject);
            //string department = _editableObject.Department;
            this.Close();
        }
        private void RejectChanges(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = false;
            this.Close();
        }
    }
}
