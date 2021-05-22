using BL.DTO;
using DAL.Entities;
using DAL.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultationManager.UnitTests
{
    static class UnitTestsHelper
    {
        public static DbContextOptions<ApplicationContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationContext(options);
            FillWithData(context);
            return options;
        }

        private static void FillWithData(ApplicationContext context)
        {
            context.Subjects.Add(new Subject { Id = 1, Name = "Subject1" });
            context.Subjects.Add(new Subject { Id = 2, Name = "Subject2" });

            context.Consultations.Add(new Consultation { Id = 1, Date = new DateTime(2021, 06, 12), LecturerId = 1, SubjectId = 1 });

            context.Queues.Add(new Queue { Id = 1, ConsultationId = 1, IssueCategory = "Code", Priority = 1 });

            context.QueueMembers.Add(new QueueMember { Id = 1, QueueId = 1, StudentId = 1, Priority = 1, IsAbsent = false, TimeInterval = TimeSpan.FromHours(1)});

            context.SaveChanges();
        }
    }
}
