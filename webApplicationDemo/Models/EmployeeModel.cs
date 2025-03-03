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
        public byte[]? EmployeeImage { get; set; } // Store image in DB
        public int CompanyID { get; set; }
        [NotMapped] // Prevent EF from mapping this field to the database
        public IFormFile? EmployeeImageFile { get; set; }

    }
}
