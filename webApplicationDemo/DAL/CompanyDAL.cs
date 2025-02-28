using webApplicationDemo.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace webApplicationDemo.DAL
{
    public class CompanyDAL
    {
        private readonly string _connectionString;

        public CompanyDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection") ?? "";
        }

        public List<CompanyModel> CompanyListGet()
        {
            List<CompanyModel> companyList = new List<CompanyModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT CompanyID, Name, Address, CompanyLogo FROM Company"; // Direct SQL Query

                    companyList = connection.Query<CompanyModel>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<CompanyModel>(); // Return empty list to avoid crashes
            }

            return companyList;
        }
    }
}
