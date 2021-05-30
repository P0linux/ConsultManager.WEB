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
    public class ConsultationRepository : Repository<Consultation>, IConsultationRepository
    {
        ApplicationContext _context;
        DbSet<Consultation> _dbSet;
        public ConsultationRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
            _dbSet = context.Set<Consultation>();
        }
        public new IQueryable<Consultation> GetAllAsync(Expression<Func<Consultation, bool>> filter)
        {
            IQueryable<Consultation> entities = _dbSet;

            if (filter != null) entities = entities.Where(filter);

            return entities.Include(c => c.Subject).Include(c => c.Lecturer);
        }
    }
}
