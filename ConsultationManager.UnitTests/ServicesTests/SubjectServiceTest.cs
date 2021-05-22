using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using BL.Implementation.Services;
using DAL.Abstraction;
using DAL.Entities;
using DAL.Implementation;
using FluentAssertions;
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
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationContext(UnitTestsHelper.GetUnitTestDbOptions());

            _unitOfWork = new UnitOfWork(_context, null, null);
            _subjectService = new SubjectService(_unitOfWork);
        }

        [Test]
        public async Task GetAll_ReturnsAllEntities() 
        {
            // Arrange
            var expectedCount = await _context.Subjects.CountAsync();

            // Act
            var resultCount = _subjectService.GetAllAsync();

            // Assert

            resultCount.Result.Count().Should().Be(expectedCount);
        }

        [Test]
        public async Task GetById_EntityExists_ReturnsEntity()
        {
            // Arrange
            var expected = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == 1);

            // Act
            var result = await _subjectService.GetByIdAsync(1);

            // Assert
            result.Should().BeOfType<SubjectDTO>();
            result.Should().BeEquivalentTo(expected.AdaptToDTO());
        }

        [Test]
        public async Task GetById_EntityNotExists_ReturnsNull()
        {
            // Act
            var result = await _subjectService.GetByIdAsync(10);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task AddAsync_AddEntity()
        {
            // Arrange
            var expectedCount = await _context.Subjects.CountAsync();

            // Act
            await _subjectService.AddAsync(new SubjectDTO { Id = 3, Name = "Subject3" });
            var resultCount = await _context.Subjects.CountAsync();

            // Assert
            resultCount.Should().Be(expectedCount + 1);
        }

        [Test]
        public async Task DeleteAsync_DeletesEntity()
        {
            // Arrange
            var expectedCount = await _context.Subjects.CountAsync();

            // Act
            await _subjectService.DeleteByIdAsync(1);
            var resultCount = await _context.Subjects.CountAsync();

            // Assert
            resultCount.Should().Be(expectedCount - 1);
        }

        [Test]
        public async Task UpdateAsync_UpdatesEntity()
        {
            // Arrange
            var expected = await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            // Act
            await _subjectService.UpdateAsync(new SubjectDTO { Id = 1, Name = "AnotherName" });
            var result = await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            // Assert
            expected.Id.Should().Be(result.Id);
            expected.Name.Should().NotBe(result.Name);
        }
    }
}
