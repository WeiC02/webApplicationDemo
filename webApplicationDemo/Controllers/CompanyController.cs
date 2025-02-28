using Microsoft.AspNetCore.Mvc;

namespace webApplicationDemo.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Company()
        {
            return View();
        }
    }
}
