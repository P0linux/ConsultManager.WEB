using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation
{
    public class ApplicationContext : IdentityDbContext<User>
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                   .HasData(new IdentityRole { Name = "student", NormalizedName = "student".ToUpper() });
            builder.Entity<IdentityRole>()
                   .HasData(new IdentityRole { Name = "lecturer", NormalizedName = "lecturer".ToUpper() });
        }
    }
}
