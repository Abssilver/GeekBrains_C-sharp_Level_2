using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using WebAPIClient.Models;

namespace WebAPIClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("https://localhost:44314"); //при запуске необходимо установить свое значение
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private async void GetData(object sender, RoutedEventArgs e)
        {
            List<Employee> employeeData = await GetDataListAsync<Employee>(client.BaseAddress + "get_employee");
            if (employeeData != null)
                DataGridEmployee.ItemsSource = employeeData;
        }
        
        private async void GetEmployeeById(object sender, RoutedEventArgs e)
        {
            List<Employee> employeeData = new List<Employee>();
            if (DataGridEmployee.SelectedIndex > -1)
            {
                Employee employee = await GetEmployeeAsync(
                    client.BaseAddress + "get_employee/" + (DataGridEmployee.SelectedIndex + 1));
                if (employee != null)
                {
                    employeeData.Add(employee);
                    DataGridEmployee.ItemsSource = employeeData;
                }
            }
            //else
            //{
            //    employeeData = (List<Employee>)await GetEmployeeDataAsync(client.BaseAddress + "get_data");
            //}
            //DataGridEmployee.ItemsSource = employeeData;
        }
        private async void ShowDepartments(object sender, RoutedEventArgs e)
        {
            List<Department> departmentData = await GetDataListAsync<Department>(client.BaseAddress + "get_departments");
            if (departmentData != null)
            {
                DepartmentWindow departmentWindow = new DepartmentWindow(departmentData);
                departmentWindow.ShowDialog();
            }
        }

        static async Task<List<T>> GetDataListAsync<T>(string path)
        {
            List<T> data = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsAsync<List<T>>();
                }
            }
            catch (Exception)
            {
            }
            return data;
        }
        /*
        static async Task<List<Employee>> GetEmployeeDataAsync(string path)
        {
            List<Employee> employees = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    employees = await response.Content.ReadAsAsync<List<Employee>>();
                }
            }
            catch (Exception)
            {
            }
            return employees;
        }
        */
        /*
        private void Post(object sender, RoutedEventArgs e)
        {
            var obj = @"{
                      'Name': 'Дмитрий',
                      'Surname': 'Грязев',
                      'Age': 25,
                      'Salary': 50000,
                      'Department': 2
                       }";
            var content = new StringContent(obj, Encoding.UTF8, "application/json");
            var r = client.PostAsync(client.BaseAddress + "add_employee", content).Result;
            textBox.Text = r.ToString();
        }
        */
        static async Task<Employee> GetEmployeeAsync(string path)
        {
            Employee employee = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                    employee = await response.Content.ReadAsAsync<Employee>();
            }
            catch (Exception)
            {
            }
            return employee;
        }
    }
}
