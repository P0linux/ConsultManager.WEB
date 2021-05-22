using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using BL.Implementation.Services;
using DAL.Abstraction;
using DAL.Implementation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultationManager.UnitTests.ServicesTests
{
    [TestFixture]
    public class ConsultationServiceTests
    {
        private IConsultationService _consultationService;
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationContext(UnitTestsHelper.GetUnitTestDbOptions());

            _unitOfWork = new UnitOfWork(_context, null, null);
            _consultationService = new ConsultationService(_unitOfWork);
        }

        [Test]
        public async Task GetAll_ReturnsAllEntities()
        {
            // Arrange
            var expectedCount = await _context.Consultations.CountAsync();

            // Act
            var resultCount = _consultationService.GetAllAsync();

            // Assert

            resultCount.Result.Count().Should().Be(expectedCount);
        }

        [Test]
        public async Task GetById_EntityExists_ReturnsEntity()
        {
            // Arrange
            var expected = await _context.Consultations.FirstOrDefaultAsync(s => s.Id == 1);

            // Act
            var result = await _consultationService.GetByIdAsync(1);

            // Assert
            result.Should().BeOfType<ConsultationDTO>();
            result.Should().BeEquivalentTo(expected.AdaptToDTO());
        }

        [Test]
        public async Task GetById_EntityNotExists_ReturnsNull()
        {
            // Act
            var result = await _consultationService.GetByIdAsync(10);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task AddAsync_AddEntity()
        {
            // Arrange
            var expectedCount = await _context.Consultations.CountAsync();

            // Act
            await _consultationService.AddAsync(new ConsultationDTO { Id = 3, SubjectId = 1 });
            var resultCount = await _context.Consultations.CountAsync();

            // Assert
            resultCount.Should().Be(expectedCount + 1);
        }

        [Test]
        public async Task DeleteAsync_DeletesEntity()
        {
            // Arrange
            var expectedCount = await _context.Consultations.CountAsync();

            // Act
            await _consultationService.DeleteByIdAsync(1);
            var resultCount = await _context.Consultations.CountAsync();

            // Assert
            resultCount.Should().Be(expectedCount - 1);
        }

        [Test]
        public async Task UpdateAsync_UpdatesEntity()
        {
            // Arrange
            var expected = await _context.Consultations.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            // Act
            await _consultationService.UpdateAsync(new ConsultationDTO { Id = 1, SubjectId = 2 });
            var result = await _context.Consultations.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            // Assert
            expected.Id.Should().Be(result.Id);
            expected.SubjectId.Should().NotBe(result.SubjectId);
        }
    }
}
