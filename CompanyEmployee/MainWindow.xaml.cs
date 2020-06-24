using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (DataGridEmployee.SelectedIndex > -1)
            {
                DataRowView deleteRow = (DataRowView)DataGridEmployee.SelectedItem;
                deleteRow.Row.Delete();
                adapter.Update(dt);
            }
        }
        private void AddRecord(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dt.NewRow();
            EmployeeEditWindow edit = new EmployeeEditWindow(newRow);
            edit.ShowDialog();
            if (edit.DialogResult.Value)
            {
                dt.Rows.Add(edit.EditableObject);
                adapter.Update(dt);
            }
        }
        private void UpdateRecord(object sender, RoutedEventArgs e)
        {
            DataRowView editRow = (DataRowView)DataGridEmployee.SelectedItem;
            editRow.BeginEdit();
            EmployeeEditWindow editWindow = new EmployeeEditWindow(editRow.Row);
            editWindow.ShowDialog();
            
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                editRow.EndEdit();
                adapter.Update(dt);
            }
            else
            {
                editRow.CancelEdit();
            }
        }

        private void EditDepartments(object sender, RoutedEventArgs e)
        {
            DepartmentEditWindow editWindow = new DepartmentEditWindow();
            editWindow.ShowDialog();
            adapter.Update(dt);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Lesson7",
                IntegratedSecurity = true,
                Pooling = true
            };
            */
            //connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            string connectionString = settings[0]?.ConnectionString;
            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();

            #region select
            //"Select EmployeeTable.Id, EmployeeTable.Name, Surname, Age, Salary, DepartmentTable.Name From EmployeeTable"
            SqlCommand command = 
                new SqlCommand(string.Format("Select {0}, {1}, {2}, {3}, {4}, {5} as {6}, {7} from {8} inner join {9} on {10} = {11}",
                                            "EmployeeTable.Id", 
                                            "EmployeeTable.Name", 
                                            "Surname",
                                            "Age", 
                                            "Salary",
                                            "DepartmentTable.Name",
                                            "DepartmentName",
                                            "Department",
                                            "EmployeeTable",
                                            "DepartmentTable",
                                            "EmployeeTable.Department",
                                            "DepartmentTable.Id"), connection);
            adapter.SelectCommand = command;

            #endregion

            #region insert

            command = new SqlCommand(@"Insert Into EmployeeTable (Name, Surname, Age, Salary, Department)
                                    Values (@Name, @Surname, @Age, @Salary, @Department); Set @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Surname", SqlDbType.NVarChar, -1, "Surname");
            command.Parameters.Add("@Age", SqlDbType.TinyInt, 1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Decimal, 5, "Salary");
            command.Parameters.Add("@Department", SqlDbType.Int, -1, "Department");
            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;

            #endregion

            #region update

            command = new SqlCommand(@"Update EmployeeTable Set Name = @Name, Surname = @Surname,
                                     Age = @Age, Salary = @Salary, Department = @Department
                                     Where Id = @Id", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Surname", SqlDbType.NVarChar, -1, "Surname");
            command.Parameters.Add("@Age", SqlDbType.TinyInt, 1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Decimal, 5, "Salary");
            command.Parameters.Add("@Department", SqlDbType.Int, -1, "Department");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            //param.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand = command;

            #endregion

            #region delete

            command = new SqlCommand("Delete From EmployeeTable Where Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            adapter.DeleteCommand = command;

            #endregion

            dt = new DataTable();
            adapter.Fill(dt);
            DataGridEmployee.DataContext = dt.DefaultView;
            //cbDepartments.ItemsSource = dt.DefaultView;
        }
    }
}

