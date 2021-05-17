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
    public class QueueServiceTests
    {
        private IQueueService _queueService;
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationContext(UnitTestsHelper.GetUnitTestDbOptions());

            _unitOfWork = new UnitOfWork(_context, null, null);
            _queueService = new QueueService(_unitOfWork);
        }

        [Test]
        public async Task GetAll_ReturnsAllEntities()
        {
            // Arrange
            var expectedCount = await _context.Queues.CountAsync();

            // Act
            var resultCount = _queueService.GetAllAsync();

            // Assert

            resultCount.Result.Count().Should().Be(expectedCount);
        }

        [Test]
        public async Task GetById_EntityExists_ReturnsEntity()
        {
            // Arrange
            var expected = await _context.Queues.FirstOrDefaultAsync(s => s.Id == 1);

            // Act
            var result = await _queueService.GetByIdAsync(1);

            // Assert
            result.Should().BeOfType<QueueDTO>();
            result.Should().BeEquivalentTo(expected.AdaptToDTO());
        }

        [Test]
        public async Task GetById_EntityNotExists_ReturnsNull()
        {
            // Act
            var result = await _queueService.GetByIdAsync(10);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task AddAsync_AddEntity()
        {
            // Arrange
            var expectedCount = await _context.Queues.CountAsync();

            // Act
            await _queueService.AddAsync(new QueueDTO { Id = 3, Priority = 1 });
            var resultCount = await _context.Queues.CountAsync();

            // Assert
            resultCount.Should().Be(expectedCount + 1);
        }

        [Test]
        public async Task DeleteAsync_DeletesEntity()
        {
            // Arrange
            var expectedCount = await _context.Queues.CountAsync();

            // Act
            await _queueService.DeleteByIdAsync(1);
            var resultCount = await _context.Queues.CountAsync();

            // Assert
            resultCount.Should().Be(expectedCount - 1);
        }

        [Test]
        public async Task UpdateAsync_UpdatesEntity()
        {
            // Arrange
            var expected = await _context.Queues.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            // Act
            await _queueService.UpdateAsync(new QueueDTO { Id = 1, Priority = 2 });
            var result = await _context.Queues.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            // Assert
            expected.Id.Should().Be(result.Id);
            expected.Priority.Should().NotBe(result.Priority);
        }
    }
}
