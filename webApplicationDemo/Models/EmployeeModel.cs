using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webApplicationDemo.Models
{
    public class EmployeeModel
    {

        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string? EmployeeImage { get; set; } // Nullable
        public int CompanyID { get; set; }

    }
}
