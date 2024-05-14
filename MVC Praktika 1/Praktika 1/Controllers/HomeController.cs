using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Praktika_1.Controllers
{
    public class HomeController : Controller
    {
        IServiceService _serviceService;

        public HomeController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IActionResult Index()
        {
            List<Service> services = _serviceService.GetAllService();
            return View(services);
        }
    }
}
