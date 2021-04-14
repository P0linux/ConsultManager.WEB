using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Abstraction
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
