using CompanyEmployee.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SqlClient;
using System.Configuration;

namespace CompanyEmployee
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        private Dictionary<string, int> departmentsDictionary = new Dictionary<string, int>();

        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;
        public DataRow EditableObject { get; set; }
        public EmployeeEditWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbName.Text = EditableObject["Name"].ToString();
            tbSurname.Text = EditableObject["Surname"].ToString();
            tbAge.Text = EditableObject["Age"].ToString();
            tbSalary.Text = EditableObject["Salary"].ToString();
            //cbDepartment.SelectedItem = EditableObject["Department"].ToString();

            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            string connectionString = settings[0]?.ConnectionString;
            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();
            SqlCommand command =
                new SqlCommand("Select Id, Name from DepartmentTable", connection);
            adapter.SelectCommand = command;
            dt = new DataTable();
            adapter.Fill(dt);
            //SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

            for (int i = 0; i < dt.Rows.Count; i++)
                departmentsDictionary.Add(Convert.ToString(dt.Rows[i]["Name"]), Convert.ToInt32(dt.Rows[i]["Id"]));

            cbDepartment.ItemsSource = departmentsDictionary.Keys;
            //cbDepartment.DisplayMemberPath = "Name";
            cbDepartment.SelectedValue = EditableObject["DepartmentName"].ToString();
        }
        public EmployeeEditWindow(DataRow employeeToEdit) : this()
        {
            EditableObject = employeeToEdit;
        }
        private void ApplyChanges(object sender, RoutedEventArgs e)
        {
            EditableObject["Name"] = tbName.Text;
            EditableObject["Surname"] = tbSurname.Text;
            EditableObject["Age"] = Convert.ToByte(tbAge.Text);
            EditableObject["Salary"] = Convert.ToDecimal(tbSalary.Text);
            EditableObject["DepartmentName"] = cbDepartment.SelectedValue.ToString();
            EditableObject["Department"] = departmentsDictionary[cbDepartment.SelectedValue.ToString()];
            this.DialogResult = true;
            this.Close();
        }
        private void RejectChanges(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
