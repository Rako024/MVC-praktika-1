using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Praktika_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(employee.PhotoFile == null)
            {
                ModelState.AddModelError("PhotoFile", "PhotoFile is Required!");
                return View();
            }

            try
            {
                _employeeService.Create(employee);
            }catch(NotFoundEmployeeException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.Delete(id);
            }catch(NotFoundEmployeeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return RedirectToAction(nameof(Index));
            }catch(Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Employee employee = _employeeService.GetEmployee(x=>x.Id == id);
            if(employee == null)
            {
                ModelState.AddModelError("", "Employe is not found");
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            try
            {
                _employeeService.Update(employee.Id, employee);
            }catch (NotFoundEmployeeException ex)
            {
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View();
            }catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
