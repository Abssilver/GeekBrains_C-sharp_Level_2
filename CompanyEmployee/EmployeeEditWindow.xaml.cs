using CompanyEmployee.Model;
using System;
using System.Collections.Generic;
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
    public partial class EmployeeEditWindow : Window
    {
        public Employee editableObject { get; set; }
        public EmployeeEditWindow()
        {
            InitializeComponent();
        }
        public EmployeeEditWindow(Employee employeeToEdit) : this()
        {
            editableObject = employeeToEdit.Clone() as Employee;
        }

    }
}
