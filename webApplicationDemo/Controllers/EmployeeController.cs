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

    }
}
