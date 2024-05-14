using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IServiceService
    {
        void Create(Service service);
        void Delete(int id);
        void Update(int id ,Service service);
        Service GetService(Func<Service,bool>? func=null);
        List<Service> GetAllService(Func<Service,bool>? func=null);
    }
}
