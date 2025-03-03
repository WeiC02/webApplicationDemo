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

        public bool UpdateCompanyLogo(int companyId, byte[] logoBytes)
        {
            string query = "UPDATE Company SET CompanyLogo = @Logo WHERE CompanyID = @CompanyId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CompanyId", companyId);
                    cmd.Parameters.AddWithValue("@Logo", (object)logoBytes ?? DBNull.Value);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


        public string GetCompanyName(int companyId)
        {
            string companyName = "";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Name FROM Company WHERE companyID = @CompanyID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CompanyID", companyId);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        companyName = result.ToString();
                    }
                }
            }
            return companyName;
        }

    }
}
