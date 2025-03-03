using Microsoft.AspNetCore.Mvc;
using webApplicationDemo.DAL;
using webApplicationDemo.Models;

namespace webApplicationDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDAL _employeeDAL;

        public EmployeeController(IConfiguration configuration)
        {
            _employeeDAL = new EmployeeDAL(configuration);
        }
        public IActionResult Index()
        {
            List<EmployeeModel> employees = _employeeDAL.GetAllEmployees();
            return View(employees);
        }
        [HttpPost]
        public IActionResult AddEmployee(EmployeeModel employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data.");
            }

            // Convert image file to byte[]
            if (employee.EmployeeImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    employee.EmployeeImageFile.CopyTo(ms);
                    employee.EmployeeImage = ms.ToArray(); // Store binary image
                }
            }

            bool isSuccess = _employeeDAL.InsertEmployee(employee);

            if (isSuccess)
            {
                string script = "<script>alert('Employee added successfully!'); window.location.href='/Company/Index';</script>";
                return Content(script, "text/html");
            }
            else
            {
                return View(employee);
            }
        }


    }
}
