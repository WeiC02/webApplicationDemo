using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webApplicationDemo.Models
{
    public class EmployeeModel
    {

            public int EmployeeID { get; set; }

            [Required]
            [StringLength(100)]
            public string FullName { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(150)]
            public string Email { get; set; }

            [StringLength(20)]
            public string Phone { get; set; }

            [DataType(DataType.Date)]
            public DateTime HireDate { get; set; }

            public byte[] EmployeeImage { get; set; }

    }
}
