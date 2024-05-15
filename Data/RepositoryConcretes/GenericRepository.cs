using Core.RepositoryAbstracts;
using Data.DAL;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }

        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public T Get(Func<T, bool>? func = null)
        {
            return func == null ? _appDbContext.Set<T>().FirstOrDefault() :
                                  _appDbContext.Set<T>().Where(func).FirstOrDefault();
        }

        public List<T> GetAll(Func<T, bool>? func = null)
        {
            return func == null ? _appDbContext.Set<T>().ToList() :
                                  _appDbContext.Set<T>().Where(func).ToList();
        }
    }
}
