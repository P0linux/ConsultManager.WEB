using DAL.Entities;
using DAL.Implementation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPI;

namespace ConsultationManager.IntegrationTests
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                RemoveDbContextServiceRegistration(services);

                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ApplicationContext>();

                    context.Database.EnsureCreated();

                    FillWithData(context);
                }
            });
        }

        private void RemoveDbContextServiceRegistration(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationContext>));
            
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        private void FillWithData(ApplicationContext context)
        {
            context.Subjects.Add(new Subject { Id = 1, Name = "Subject1" });
            context.Subjects.Add(new Subject { Id = 2, Name = "Subject2" });

            context.Consultations.Add(new Consultation { Id = 1, Date = new DateTime(2021, 06, 12), LecturerId = 1, SubjectId = 1 });

            context.Queues.Add(new Queue { Id = 1, ConsultationId = 1, IssueCategory = "Code", Priority = 1 });

            context.QueueMembers.Add(new QueueMember { Id = 1, QueueId = 1, StudentId = 1, Priority = 1, IsAbsent = false, TimeInterval = TimeSpan.FromHours(1) });

            context.SaveChanges();
        }
    }
}
