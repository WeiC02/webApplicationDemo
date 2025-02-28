using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using webApplicationDemo.DAL;
using webApplicationDemo.Models;

namespace webApplicationDemo.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyDAL _companyDAL;
        private readonly EmployeeDAL _employeeDAL; // ✅ Add EmployeeDAL field

        public CompanyController(CompanyDAL companyDAL, EmployeeDAL employeeDAL)
        {
            _companyDAL = companyDAL;
            _employeeDAL = employeeDAL; // ✅ Inject EmployeeDAL
        }

        public IActionResult Index()
        {
            List<CompanyModel> companies = _companyDAL.CompanyListGet();
            return View("Company", companies);
        }
        [HttpGet]
        public JsonResult GetEmployees(int companyId)
        {
            var employees = _employeeDAL.GetEmployeesByCompany(companyId);
            return Json(employees);
        }

        public IActionResult AddEmpInCmp()
        {
            List<EmployeeModel> employees = _employeeDAL.GetAllEmployees();
            return View(employees);
        }


    }
}
