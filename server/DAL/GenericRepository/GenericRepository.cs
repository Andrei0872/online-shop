using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using server.Data;

namespace server.DAL.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

    private DbSet<T> entities;
    protected readonly ServerContext _serverContext;
    public GenericRepository(ServerContext serverContext)
    {
      _serverContext = serverContext;
      entities = serverContext.Set<T>();
    }

    public void Delete(T entity)
    {
      entities.Remove(entity);
    }

    public async Task<T> Get(int id)
    {
      return await entities.FindAsync(id);
    }

    public IQueryable<T> GetAll()
    {
      return entities.AsNoTracking();
    }

    public void Insert(T entity)
    {
      entities.Add(entity);
    }

    public void Update(T entity)
    {
      entities.Update(entity);
    }

    public async Task<bool> SaveAsync()
    {
      return await _serverContext.SaveChangesAsync() > 0;
    }
  }
}