using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstraction
{
    public interface IConsultationRepository: IRepository<Consultation>
    {
        new IQueryable<Consultation> GetAllAsync(Expression<Func<Consultation, bool>> filter = null);
    }
}
