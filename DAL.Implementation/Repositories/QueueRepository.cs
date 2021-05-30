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
    public class QueueRepository : Repository<Queue>, IQueueRepository
    {
        ApplicationContext _context;
        DbSet<Queue> _dbSet;
        public QueueRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
            _dbSet = context.Set<Queue>();
        }

        public new IQueryable<Queue> GetAllAsync(Expression<Func<Queue, bool>> filter)
        {
            IQueryable<Queue> entities = _dbSet;

            if (filter != null) entities = entities.Where(filter);

            return entities.Include(q => q.Consultation)
                           .ThenInclude(c => c.Lecturer)
                           .Include(c => c.Consultation)
                           .ThenInclude(c => c.Subject);
        }
    }
}
