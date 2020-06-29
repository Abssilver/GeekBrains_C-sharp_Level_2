using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyEmployee.Model;

namespace CompanyEmployee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> _employeeData = new ObservableCollection<Employee>();
        ObservableCollection<string> _departmentData = new ObservableCollection<string>();
        public ObservableCollection<Employee> EmployeeData => _employeeData;
        public ObservableCollection<string> DepartmentData => _departmentData;
        private List<string> _departmentPull = new List<string>
        {
            "Маркетинг", "Сбыт", "Инновации",
            "Закупки", "Финансы", "Связи с общественностью",
            "Кадры", "Администрация" 
        };
        private List<string> _namePull = new List<string>
        {
            "Василий Лазарев",
            "Петр Жуков",
            "Николай Орехов",
            "Анастасия Журавлева",
            "Дмитрий Панов",
            "Алексей Зуев",
            "Екатерина Боброва",
            "Мария Лапина",
            "Степан Рыбаков"
        };

        public MainWindow()
        {
            InitializeComponent();
            FillLists();
        }
        void FillLists()
        {
            Random rnd = new Random();
            for (int i = 0; i < _namePull.Count; i++)
            {
                var _nameData = _namePull[i].Split(' ');
                _employeeData.Add(
                    new Employee(_nameData[0], _nameData[1], 
                                 rnd.Next(18, 45), 
                                 rnd.Next(20, 100) * 100, 
                                 _departmentPull[rnd.Next(0, _departmentPull.Count)]));
            }

            foreach (var department in _departmentPull)
            {
                Department.AddDepartment(department);
                _departmentData.Add(department);
            }
            //lvEmployee.ItemsSource = _employeeData;
        }
        private void AddDepartment(object sender, RoutedEventArgs e)
        {
            if (!TextBoxDepartments.Text.Equals(string.Empty) &&
                !Department.DepartmentsName.Exists(x=> TextBoxDepartments.Text.Equals(x)))
            {
                Department.AddDepartment(TextBoxDepartments.Text);
                _departmentData.Add(TextBoxDepartments.Text);
            }
            TextBoxDepartments.Clear();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (lvEmployee.SelectedIndex >= 0)
            {
                EmployeeEditWindow editWindow = new EmployeeEditWindow(lvEmployee.SelectedItem as Employee);
                editWindow.ShowDialog();
                //_employeeData.RemoveAt(lvEmployee.SelectedIndex);
            }
        }

        private void AddEmployee(object sender, RoutedEventArgs e) =>
            _employeeData.Add(new Employee("Имя", "Фамилия", 00, 0000, Department.DepartmentsName[0]));

        private void RemoveEmployee(object sender, RoutedEventArgs e)
        {
            if (lvEmployee.SelectedIndex >= 0)
                _employeeData.RemoveAt(lvEmployee.SelectedIndex);
        }
    }
}

