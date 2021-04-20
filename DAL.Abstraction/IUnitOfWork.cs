using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DAL.Abstraction
{
    public interface IUnitOfWork
    {
        SignInManager<User> SignInManager { get; }
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IRepository<Consultation> ConsultationRepository { get; }
        IRepository<Queue> QueueRepository { get; }
        IRepository<QueueMember> QueueMemberRepository { get; }
        IRepository<Subject> SubjectRepository { get; }

        Task CommitAsync();
    }
}
