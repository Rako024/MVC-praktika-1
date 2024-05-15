using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        IWebHostEnvironment _webHostEnvironment;
        public EmployeeService(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Create(Employee emp)
        {
            if(emp == null)
            {
                throw new NotFoundEmployeeException("", "Employe referance is null");
            }

            if(emp.PhotoFile == null)
            {
                throw new NotFoundPhotoFileException("PhotoFile", "Photo File is null!");
            }
            if (!emp.PhotoFile.ContentType.Contains("image/"))
            {
                throw new ContentTypeException("PhotoFile", "Photo File Content is Bad!");
            }

            string path = _webHostEnvironment.WebRootPath + @"\upload\employee\" + emp.PhotoFile.FileName;
            using(FileStream file = new FileStream(path, FileMode.Create))
            {
                emp.PhotoFile.CopyTo(file);
            }
            emp.ImgUrl = emp.PhotoFile.FileName;
            _employeeRepository.Add(emp);
            _employeeRepository.Commit();
        }

        public void Delete(int id)
        {
            Employee emp = _employeeRepository.Get(x => x.Id == id);
            if(emp == null)
            {
                throw new NotFoundEmployeeException("", "Employe referance is null");
            }
            string path = _webHostEnvironment.WebRootPath + @"\upload\employee\" + emp.ImgUrl;
            FileInfo fileInfo = new FileInfo(path);
            fileInfo.Delete();
            _employeeRepository.Delete(emp);
            _employeeRepository.Commit();
        }

        public List<Employee> GetAllEmployees(Func<Employee, bool>? func = null)
        {
            return _employeeRepository.GetAll(func);
        }

        public Employee GetEmployee(Func<Employee, bool>? func = null)
        {
            return _employeeRepository.Get(func);
        }

        public void Update(int id, Employee newEmp)
        {
            Employee oldEmp = _employeeRepository.Get(x => x.Id == id);
            if (oldEmp == null)
            {
                throw new NotFoundEmployeeException("", "Employe referance is null");
            }
            if(newEmp.PhotoFile != null)
            {
                if (!newEmp.PhotoFile.ContentType.Contains("image/"))
                {
                    throw new ContentTypeException("PhotoFile", "Photo File Content is Bad!");
                }

                string path = _webHostEnvironment.WebRootPath + @"\upload\employee\" + newEmp.PhotoFile.FileName;
                using (FileStream file = new FileStream(path, FileMode.Create))
                {
                    newEmp.PhotoFile.CopyTo(file);
                }
                string path1 = _webHostEnvironment.WebRootPath + @"\upload\employee\" + oldEmp.ImgUrl;
                FileInfo fileInfo = new FileInfo(path1);
                fileInfo.Delete();
                oldEmp.ImgUrl = newEmp.PhotoFile.FileName;
            }
            oldEmp.FullName = newEmp.FullName;
            oldEmp.Position = newEmp.Position;
            _employeeRepository.Commit();
        }
    }
}
