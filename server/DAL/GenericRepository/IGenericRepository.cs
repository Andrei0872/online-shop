using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.DAL.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveAsync();
    }
}