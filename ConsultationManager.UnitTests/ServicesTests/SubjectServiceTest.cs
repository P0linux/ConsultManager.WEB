using BL.Abstraction;
using DAL.Abstraction;
using DAL.Entities;
using DAL.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultationManager.UnitTests.ServicesTests
{
    [TestFixture]
    class SubjectServiceTest
    {
        private ISubjectService _subjectService;
        private ApplicationContext _applicationContext;
        private IRepository<Subject> _subjectRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ConsultationManagerDb");


        }
    }
}
