using DAL.Abstraction;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation.Repositories
{
    public class QueueMemberRepository: Repository<QueueMember>, IQueueMemberRepository
    {
        ApplicationContext _context;
        DbSet<QueueMember> _dbSet;
        public QueueMemberRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
            _dbSet = context.Set<QueueMember>();
        }

        public new IQueryable<QueueMember> GetAllAsync(Expression<Func<QueueMember, bool>> filter)
        {
            IQueryable<QueueMember> entities = _dbSet;

            if (filter != null) entities = entities.Where(filter);

            return entities.Include(q => q.Queue).Include(q => q.Student);
        }
    }
}
