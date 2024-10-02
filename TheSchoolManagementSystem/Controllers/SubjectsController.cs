using Microsoft.AspNetCore.Mvc;

namespace TheSchoolManagementSystem.Controllers
{
    public class SubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
