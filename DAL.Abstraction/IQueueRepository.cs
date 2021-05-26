using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstraction
{
    public interface IQueueRepository: IRepository<Queue>
    {
        new IQueryable<Queue> GetAllAsync(Expression<Func<Queue, bool>> filter = null);
    }
}
