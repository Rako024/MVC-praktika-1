using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts
{
    public interface IGenericRepository<T> where T : class,new()
    {
        void Add(T entity);
        void Delete(T entity);
        T Get(Func<T,bool>? func = null);
        List<T> GetAll(Func<T,bool>? func = null);
        void Commit();
    }
}
