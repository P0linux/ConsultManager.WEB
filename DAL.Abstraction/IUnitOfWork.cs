using DAL.Entities;
using System.Threading.Tasks;

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
