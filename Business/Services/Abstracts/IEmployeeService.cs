using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IEmployeeService
    {
        void Create(Employee employee);
        void Delete(int id);
        Employee GetEmployee(Func<Employee,bool>? func = null);
        List<Employee> GetAllEmployees(Func<Employee,bool>? func = null);
        void Update(int id, Employee emp);
    }
}
