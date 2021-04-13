using DAL.Abstraction;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Implementation
{
    public static class DALServices
    {
        public static IServiceCollection RegisterDALServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequiredLength = 4;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
