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

            bool isSuccess = _employeeDAL.InsertEmployee(employee);

            if (isSuccess)
            {
                return RedirectToAction("Index", "Company");
                // Redirect to employee list after successful insertion
            }
            else
            {
                return View(employee); // Stay on the form if insertion fails
            }
        }


    }
}
