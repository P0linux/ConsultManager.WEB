using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Abstraction
{
    public interface IQueueMemberRepository: IRepository<QueueMember>
    {
        new IQueryable<QueueMember> GetAllAsync(Expression<Func<QueueMember, bool>> filter = null);
    }
}
