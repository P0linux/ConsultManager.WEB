using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation
{
    class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            base.Database.EnsureCreated();
        }

        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<QueueMember> QueueMembers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
