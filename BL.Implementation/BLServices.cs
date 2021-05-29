using BL.Abstraction;
using BL.Implementation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BL.Implementation
{
    public static class BLServices
    {
        public static IServiceCollection RegisterBLServices(this IServiceCollection services)
        {
            services.AddTransient<IConsultationService, ConsultationService>();
            services.AddTransient<IQueueMemberService, QueueMemberService>();
            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<MVCUserService, MVCUserService>();

            return services;
        }
    }
}
