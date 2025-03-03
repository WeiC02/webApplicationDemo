using System.ComponentModel.DataAnnotations;

namespace webApplicationDemo.Models
{
    public class CompanyModel
    {
        public int CompanyID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public byte[] CompanyLogo { get; set; } // Stores company logo image

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property for employees
        public ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
        public string CompanyLogoBase64
        {
            get
            {
                return CompanyLogo != null ? "data:image/png;base64," + Convert.ToBase64String(CompanyLogo) : "";
            }
        }
    }
}
