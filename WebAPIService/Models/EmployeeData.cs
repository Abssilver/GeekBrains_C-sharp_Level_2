using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPIService.Models
{
    public class EmployeeData
    {
        private SqlConnection sqlConnection;

        public EmployeeData()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Lesson7",
                IntegratedSecurity = true,
                Pooling = true
            };

            sqlConnection = new SqlConnection(connectionStringBuilder.ConnectionString);
        }

        public List<Employee> GetEmployeeData()
        {
            sqlConnection.Open();

            List<Employee> employees = new List<Employee>();

            string sqlQuery = string.Format("Select {0}, {1}, {2}, {3}, {4}, {5} as {6}, {7} from {8} inner join {9} on {10} = {11}",
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
                            "DepartmentTable.Id");

            using (SqlCommand command = new SqlCommand(sqlQuery, sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(
                            new Employee()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Surname = reader["Surname"].ToString(),
                                Age = Convert.ToByte(reader["Age"]),
                                Salary = Convert.ToDouble(reader["Salary"]),
                                Department = reader["Department"].ToString()
                            });
                    }
                }

            }
            sqlConnection.Close();
            return employees;
        }

        public Employee GetEmployeeById(int Id)
        {
            sqlConnection.Open();

            string sqlQuery = $@"SELECT * FROM EmployeeTable WHERE Id={Id}";

            Employee employeeToReturn = new Employee();

            using (SqlCommand command = new SqlCommand(sqlQuery, sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeToReturn = new Employee()
                        {
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            Age = Convert.ToByte(reader["Age"]),
                            Salary = Convert.ToDouble(reader["Salary"]),
                            Department = reader["Department"].ToString()
                        };
                    }
                }
            }
            sqlConnection.Close();
            return employeeToReturn;
        }
        public bool AddEmployee(Employee toAdd)
        {
            using (sqlConnection)
            {
                try
                {
                    string sqlQuery = $@"Insert Into EmployeeTable (Name, Surname, Age, Salary, Department)
                                         Values (N'{toAdd.Name}', 
                                                 N'{toAdd.Surname}', 
                                                  '{toAdd.Age}', 
                                                  '{toAdd.Salary}', 
                                                 N'{toAdd.Department}')";
                    using (SqlCommand command = new SqlCommand(sqlQuery, sqlConnection))
                        command.ExecuteNonQuery();
                }
                catch
                {

                    return false;
                }
                return true;
            }
        }
    }
}
