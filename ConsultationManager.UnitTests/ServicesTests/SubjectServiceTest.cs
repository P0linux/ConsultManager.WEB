using BL.Abstraction;
using BL.Implementation.Services;
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
        private ApplicationContext _context;
        private IRepository<Subject> _subjectRepository;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationContext(UnitTestsHelper.GetUnitTestDbOptions());
            _subjectRepository = new Repository<Subject>(_context);
        }

        [Test]
        public async Task GetAll_
    }
}
