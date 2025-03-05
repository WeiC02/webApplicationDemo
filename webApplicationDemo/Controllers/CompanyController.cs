using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using webApplicationDemo.DAL;
using webApplicationDemo.Models;
using Microsoft.AspNetCore.Http; // Required for Session

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

        //public IActionResult Company()
        //{
        //    List<CompanyModel> companies = _companyDAL.CompanyListGet();//get company name logo data to let users select from dropdown in the first page
        //    return View(companies);
        //}

        public IActionResult Company()
        {
            List<CompanyModel> companies = _companyDAL.CompanyListGet(); // Get company list

            // Retrieve the last selected company from session
            string lastSelectedCompany = HttpContext.Session.GetString("SelectedCompany");

            // If session is empty and there are companies, set the first company as default
            if (string.IsNullOrEmpty(lastSelectedCompany) && companies.Count > 0)
            {
                lastSelectedCompany = companies[0].CompanyID.ToString();
                HttpContext.Session.SetString("SelectedCompany", lastSelectedCompany); // Store default selection in session
            }

            // Pass it to the view using ViewBag
            ViewBag.SelectedCompany = lastSelectedCompany;

            return View(companies);
        }


        [HttpPost]
        public IActionResult SaveSelectedCompany(string companyId)
        {
            // Store the selected company ID in session
            HttpContext.Session.SetString("SelectedCompany", companyId);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UploadCompanyLogo(IFormFile companyLogoFile, int companyId)
        {
            if (companyLogoFile == null || companyId <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid file or company ID." });
            }

            try
            {
                byte[] logoBytes;
                using (var ms = new MemoryStream())
                {
                    companyLogoFile.CopyTo(ms);
                    logoBytes = ms.ToArray();
                }

                bool isUpdated = _companyDAL.UpdateCompanyLogo(companyId, logoBytes);

                if (isUpdated)
                {
                    return Json(new { success = true, message = "Company logo updated successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update company logo." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error: " + ex.Message });
            }
        }



        [HttpGet]
        public JsonResult GetEmployees(int companyId)
        {
            var employees = _employeeDAL.GetEmployeesByCompany(companyId);
            return Json(employees);
        }

        public IActionResult AddEmployeeView(int companyId)
        {
            // Fetch company name from the database
            string companyName = _companyDAL.GetCompanyName(companyId);

            // Pass company name and ID to the view
            ViewBag.CompanyId = companyId;//pass data from a controller to a view without using a model
            ViewBag.CompanyName = companyName;

            return View("AddEmpInCmp");
        }

    }
}
