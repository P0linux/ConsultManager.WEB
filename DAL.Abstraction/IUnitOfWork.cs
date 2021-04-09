using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Abstraction
{
    public interface IUnitOfWork
    {
        IRepository<Consultation> ConsultationRepository { get; }
        IRepository<Queue> QueueRepository { get; }
        IRepository<QueueMember> QueueMemberRepository { get; }
        IRepository<Subject> SubjectRepository { get; }

        Task CommitAsync();
    }
}
