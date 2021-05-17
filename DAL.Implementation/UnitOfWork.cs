using DAL.Abstraction;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationContext _context;

        public UnitOfWork(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        IRepository<Consultation> _consultationRepository;
        public IRepository<Consultation> ConsultationRepository =>
            _consultationRepository ??= new Repository<Consultation>(_context);

        IRepository<Queue> _queueRepository;
        public IRepository<Queue> QueueRepository =>
            _queueRepository ??= new Repository<Queue>(_context);

        IRepository<QueueMember> _queueMemberRepository;
        public IRepository<QueueMember> QueueMemberRepository =>
            _queueMemberRepository ??= new Repository<QueueMember>(_context);

        IRepository<Subject> _subjectRepository;
        public IRepository<Subject> SubjectRepository =>
            _subjectRepository ??= new Repository<Subject>(_context);

        public SignInManager<User> SignInManager { get; }

        public UserManager<User> UserManager { get; }

        public RoleManager<IdentityRole> RoleManager { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
