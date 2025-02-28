using Microsoft.AspNetCore.Mvc;

namespace webApplicationDemo.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
