using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPIService.Models
{
    public class DepartmentData
    {
        private SqlConnection sqlConnection;

        public DepartmentData()
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

        public List<Department> GetDepartmentData()
        {
            sqlConnection.Open();

            List<Department> departments = new List<Department>();

            string sqlQuery = "Select Id, Name from DepartmentTable";

            using (SqlCommand command = new SqlCommand(sqlQuery, sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(
                            new Department()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                            });
                    }
                }
            }
            sqlConnection.Close();
            return departments;
        }
    }
}