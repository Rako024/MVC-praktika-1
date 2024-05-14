using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Praktika_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IActionResult Index()
        {
            List<Service> services = _serviceService.GetAllService();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _serviceService.Create(service);
            }catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View();
            }catch(NotFoundImageFileException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _serviceService.Delete(id);
            }catch(NotFoundServiceException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            Service service = _serviceService.GetService(x => x.Id == id);
            if (service == null)
            {
                ModelState.AddModelError("", "Service Not found!!!");
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        [HttpPost]
        public IActionResult Update(Service service)
        {
            try
            {
                _serviceService.Update(service.Id, service);
            }
            catch (NotFoundServiceException ex)
            {
                ModelState.AddModelError("", "Service not found!!!");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
