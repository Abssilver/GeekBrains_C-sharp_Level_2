using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CompanyEmployee
{
    /// <summary>
    /// Логика взаимодействия для DepartmentEditWindow.xaml
    /// </summary>
    public partial class DepartmentEditWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;

        List<string> departmentsList = new List<string>();
        public DepartmentEditWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            string connectionString = settings[0]?.ConnectionString;
            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();

            #region select

            SqlCommand command =
                new SqlCommand("Select Id, Name from DepartmentTable", connection);
            adapter.SelectCommand = command;

            #endregion

            #region insert

            command = new SqlCommand(@"Insert Into DepartmentTable (Name) 
                                       Values (@Name); Set @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;

            #endregion

            #region update

            command = new SqlCommand(@"Update DepartmentTable Set Name = @Name
                                       Where Id = @Id", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            //param.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand = command;

            #endregion

            #region delete

            command = new SqlCommand("Delete From DepartmentTable Where Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            adapter.DeleteCommand = command;

            #endregion

            dt = new DataTable();
            adapter.Fill(dt);

            DataGridDepartment.DataContext = dt.DefaultView;
            for (int i = 0; i < dt.Rows.Count; i++)
                departmentsList.Add(Convert.ToString(dt.Rows[i]["Name"]));
            //cbDepartments.ItemsSource = dt.DefaultView;
        }
        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (DataGridDepartment.SelectedIndex > -1)
            {
                DataRowView deleteRow = (DataRowView)DataGridDepartment.SelectedItem;
                deleteRow.Row.Delete();
                adapter.Update(dt);
            }
        }
        private void AddRecord(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dt.NewRow();
            if (!TextBoxDepartments.Text.Equals(string.Empty) && !departmentsList.Contains(TextBoxDepartments.Text))
            {
                newRow["Name"] = TextBoxDepartments.Text;
                departmentsList.Add(TextBoxDepartments.Text);
                dt.Rows.Add(newRow);
                adapter.Update(dt);
            } 
            TextBoxDepartments.Clear();
        }
    }
}
