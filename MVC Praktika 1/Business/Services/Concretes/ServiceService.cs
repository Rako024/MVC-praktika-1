using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes;

public class ServiceService : IServiceService
{
    IWebHostEnvironment _webHostEnvironment;
    IServiceRepository _serviceRepository;

    public ServiceService(IWebHostEnvironment webHostEnvironment, IServiceRepository serviceRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _serviceRepository = serviceRepository;
    }

    public void Create(Service service)
    {
        if (service.ImageFile == null)
        {
            throw new NotFoundImageFileException("File Yuklemek Mutleqdir!!");
        }
        if (!service.ImageFile.ContentType.Contains("image/"))
        {
            throw new ContentTypeException(service.ImageFile.FileName, "Duzgun formatda deyil!");
        }
       
        string path = _webHostEnvironment.WebRootPath+@"\upload\service\"+service.ImageFile.FileName;
        using(FileStream file = new FileStream(path, FileMode.Create))
        {
            service.ImageFile.CopyTo(file);
        }
        service.ImgUrl = service.ImageFile.FileName;
        _serviceRepository.Add(service);
        _serviceRepository.Commit();
    }

    public void Delete(int id)
    {
        Service service = _serviceRepository.Get(x => x.Id == id);
        if(service == null)
        {
            throw new NotFoundServiceException("Bele bir Service Yoxdur!!!");
        }
        _serviceRepository.Delete(service);
        _serviceRepository.Commit();
    }

    public List<Service> GetAllService(Func<Service, bool>? func = null)
    {
        return _serviceRepository.GetAll(func); 
    }

    public Service GetService(Func<Service, bool>? func = null)
    {
        return _serviceRepository.Get(func);
    }

    public void Update(int id, Service newService)
    {
        Service oldService = _serviceRepository.Get(x => x.Id == id);
        if (newService == null)
        {
            throw new NotFoundServiceException("Bele bir Service Yoxdur!!!");
        }
        if(newService.ImageFile != null)
        {
            FileInfo fileInfo = new FileInfo(_webHostEnvironment.WebRootPath + @"\upload\service\"+oldService.ImgUrl);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            string path = _webHostEnvironment.WebRootPath + @"\upload\service\" + newService.ImageFile.FileName;
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                newService.ImageFile.CopyTo(file);
            }
            oldService.ImgUrl = newService.ImageFile.FileName;
        }
        oldService.Name = newService.Name;
        oldService.Description = newService.Description;
        _serviceRepository.Commit();
    }
}
