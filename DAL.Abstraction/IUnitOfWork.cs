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
        IConsultationRepository ConsultationRepository { get; }
        IQueueRepository QueueRepository { get; }
        IQueueMemberRepository QueueMemberRepository { get; }
        IRepository<Subject> SubjectRepository { get; }

        Task CommitAsync();
    }
}
