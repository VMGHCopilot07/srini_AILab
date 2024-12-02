using Microsoft.AspNetCore.Mvc;

namespace SonarQube.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
