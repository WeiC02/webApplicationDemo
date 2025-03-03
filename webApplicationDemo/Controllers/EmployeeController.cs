using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public IActionResult AddEmployee(EmployeeModel employee, string EmployeePhoto)
        {
            if (employee == null)
            {
                return Json(new { success = false, message = "Invalid employee data." });
            }

            // Convert Base64 string to byte[] if a webcam photo was taken
            if (!string.IsNullOrEmpty(EmployeePhoto))
            {
                string base64Data = EmployeePhoto.Split(',')[1]; // Remove data URL prefix
                employee.EmployeeImage = Convert.FromBase64String(base64Data);
            }
            // If an image file was uploaded instead
            else if (employee.EmployeeImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    employee.EmployeeImageFile.CopyTo(ms);
                    employee.EmployeeImage = ms.ToArray();
                }
            }

            bool isSuccess = _employeeDAL.InsertEmployee(employee);

            if (isSuccess)
            {
                return Json(new { success = true, redirectUrl = "/Company/Index" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to add employee." });
            }
        }

        public IActionResult Edit(int employeeId)
        {
            var employee = _employeeDAL.GetEmployeeById(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            return View("~/Views/Company/EditEmpInCmp.cshtml", employee);
        }



        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeModel employee, string EmployeePhoto)
        {
            if (!ModelState.IsValid)
            {
                return View("EditEmpInCmp", employee);
            }
            // Convert Base64 string to byte[] if a webcam photo was taken
            if (!string.IsNullOrEmpty(EmployeePhoto))
            {
                string base64Data = EmployeePhoto.Split(',')[1]; // Remove data URL prefix
                employee.EmployeeImage = Convert.FromBase64String(base64Data);
            }
            // If an image file was uploaded instead
            else if (employee.EmployeeImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    employee.EmployeeImageFile.CopyTo(ms);
                    employee.EmployeeImage = ms.ToArray();
                }
            }

            bool isUpdated = _employeeDAL.UpdateEmployee(employee);

            if (isUpdated)
            {
                return Json(new { success = true, redirectUrl = "/Company/Index" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to update employee." });
            }
        }




    }
}
