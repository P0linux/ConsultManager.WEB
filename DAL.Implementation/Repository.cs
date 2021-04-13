using DAL.Abstraction;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }
        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
