﻿using DAL.Abstraction;
using DAL.Entities;
using DAL.Implementation.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationContext _context;

        public UnitOfWork(ApplicationContext context, 
                          UserManager<User> userManager, 
                          SignInManager<User> signInManager, 
                          RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        IConsultationRepository _consultationRepository;
        public IConsultationRepository ConsultationRepository =>
            _consultationRepository ??= new ConsultationRepository(_context);

        IQueueRepository _queueRepository;
        public IQueueRepository QueueRepository =>
            _queueRepository ??= new QueueRepository(_context);

        IQueueMemberRepository _queueMemberRepository;
        public IQueueMemberRepository QueueMemberRepository =>
            _queueMemberRepository ??= new QueueMemberRepository(_context);

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
