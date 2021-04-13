using DAL.Abstraction;
using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    class UnitOfWork : IUnitOfWork
    {
        ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
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

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
