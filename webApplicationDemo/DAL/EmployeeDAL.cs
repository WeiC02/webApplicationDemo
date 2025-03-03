using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Xml;
using webApplicationDemo.Models;


namespace webApplicationDemo.DAL
{
    public class EmployeeDAL : Controller
    {
        private readonly string _connectionString;
        public EmployeeDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection") ?? "";

        }


        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT EmployeeID, FullName, Email, Phone, HireDate, EmployeeImage, CompanyID FROM Employee";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new EmployeeModel
                                {
                                    EmployeeID = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Phone = reader.GetString(3),
                                    HireDate = reader.GetDateTime(4),
                                    EmployeeImage = reader.IsDBNull(5) ? null : (byte[])reader["EmployeeImage"],
                                    CompanyID = reader.GetInt32(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("[GetAllEmployees] - " + ex.Message);
            }

            return employees;
        }

        public List<EmployeeModel> GetEmployeesByCompany(int companyId)
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT EmployeeID, FullName, Email, Phone, HireDate, EmployeeImage, CompanyID FROM Employee WHERE CompanyID = @CompanyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyID", companyId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new EmployeeModel
                                {
                                    EmployeeID = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Phone = reader.GetString(3),
                                    HireDate = reader.GetDateTime(4),
                                    EmployeeImage = reader.IsDBNull(5) ? null : (byte[])reader["EmployeeImage"], // Corrected for VARBINARY
                                    CompanyID = reader.GetInt32(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("[GetEmployeesByCompany] - " + ex.Message);
            }

            return employees;
        }


        public bool InsertEmployee(EmployeeModel employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Employee (FullName, Email, Phone, HireDate, CompanyID, EmployeeImage) " +
                               "VALUES (@FullName, @Email, @Phone, @HireDate, @CompanyID, @EmployeeImage)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", employee.FullName);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);
                    command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                    command.Parameters.AddWithValue("@CompanyID", employee.CompanyID);
                    command.Parameters.Add("@EmployeeImage", SqlDbType.VarBinary).Value = (object?)employee.EmployeeImage ?? DBNull.Value;

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public EmployeeModel? GetEmployeeById(int employeeId)
        {
            EmployeeModel? employee = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT EmployeeID, FullName, Email, Phone, HireDate, EmployeeImage, CompanyID FROM Employee WHERE EmployeeID = @EmployeeID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", employeeId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employee = new EmployeeModel
                                {
                                    EmployeeID = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Phone = reader.GetString(3),
                                    HireDate = reader.GetDateTime(4),
                                    EmployeeImage = reader.IsDBNull(5) ? null : (byte[])reader["EmployeeImage"],
                                    CompanyID = reader.GetInt32(6)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("[GetEmployeeById] - " + ex.Message);
            }

            return employee;
        }

        public bool UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Employee SET FullName = @FullName, Email = @Email, Phone = @Phone, HireDate = @HireDate, EmployeeImage = @EmployeeImage WHERE EmployeeID = @EmployeeID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", employee.FullName);
                        command.Parameters.AddWithValue("@Email", employee.Email);
                        command.Parameters.AddWithValue("@Phone", employee.Phone);
                        command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                        command.Parameters.Add("@EmployeeImage", SqlDbType.VarBinary).Value = (object?)employee.EmployeeImage ?? DBNull.Value;
                        command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("[UpdateEmployee] - " + ex.Message);
            }
        }



    }
}
