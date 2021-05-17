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
    public class QueueMemberServiceTests
    {
        private IQueueMemberService _queueMemberService;
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationContext(UnitTestsHelper.GetUnitTestDbOptions());

            _unitOfWork = new UnitOfWork(_context, null, null);
            _queueMemberService = new QueueMemberService(_unitOfWork);
        }

        [Test]
        public async Task GetAll_ReturnsAllEntities()
        {
            // Arrange
            var expectedCount = await _context.QueueMembers.CountAsync();

            // Act
            var resultCount = _queueMemberService.GetAllAsync();

            // Assert

            resultCount.Result.Count().Should().Be(expectedCount);
        }

        [Test]
        public async Task GetById_EntityExists_ReturnsEntity()
        {
            // Arrange
            var expected = await _context.QueueMembers.FirstOrDefaultAsync(s => s.Id == 1);

            // Act
            var result = await _queueMemberService.GetByIdAsync(1);

            // Assert
            result.Should().BeOfType<QueueMemberDTO>();
            result.Should().BeEquivalentTo(expected.AdaptToDTO());
        }

        [Test]
        public async Task GetById_EntityNotExists_ReturnsNull()
        {
            // Act
            var result = await _queueMemberService.GetByIdAsync(10);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task AddAsync_AddEntity()
        {
            // Arrange
            var expectedCount = await _context.QueueMembers.CountAsync();

            // Act
            await _queueMemberService.AddAsync(new QueueMemberDTO { Id = 3, Priority = 1 });
            var resultCount = await _context.QueueMembers.CountAsync();

            // Assert
            resultCount.Should().Be(expectedCount + 1);
        }

        [Test]
        public async Task DeleteAsync_DeletesEntity()
        {
            // Arrange
            var expectedCount = await _context.QueueMembers.CountAsync();

            // Act
            await _queueMemberService.DeleteByIdAsync(1);
            var resultCount = await _context.QueueMembers.CountAsync();

            // Assert
            resultCount.Should().Be(expectedCount - 1);
        }

        [Test]
        public async Task UpdateAsync_UpdatesEntity()
        {
            // Arrange
            var expected = await _context.QueueMembers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            // Act
            await _queueMemberService.UpdateAsync(new QueueMemberDTO { Id = 1, Priority = 2 });
            var result = await _context.QueueMembers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);

            // Assert
            expected.Id.Should().Be(result.Id);
            expected.Priority.Should().NotBe(result.Priority);
        }
    }
}
