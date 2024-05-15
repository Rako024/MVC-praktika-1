using Microsoft.AspNetCore.Mvc;

namespace MVC_Praktika_2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
