﻿using Microsoft.AspNetCore.Mvc;
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
                                    EmployeeImage = reader.IsDBNull(5) ? null : reader.GetString(5),
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
                                    EmployeeImage = reader.IsDBNull(5) ? null : reader.GetString(5),
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
                string query = "INSERT INTO Employee (FullName, Email, Phone, HireDate, CompanyID) VALUES (@FullName, @Email, @Phone, @HireDate, @CompanyID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", employee.FullName);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);
                    command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                    command.Parameters.AddWithValue("@CompanyID", employee.CompanyID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


    }
}
