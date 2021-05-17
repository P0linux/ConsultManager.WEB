using BL.Abstraction;
using BL.DTO;
using BL.DTO.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace ConsultationManager.UnitTests.ControllersTests
{
    [TestFixture]
    public class QueueControllerTests
    {
        private Mock<IQueueService> _queueService;
        private QueueController _queueController;
        private QueueDTO queue;

        [SetUp]
        public void SetUp()
        {
            _queueService = new Mock<IQueueService>();
            _queueController = new QueueController(_queueService.Object);

            queue = new QueueDTO { Id = 1, ConsultationId = 1, IssueCategory = IssueCategory.Pass, Priority = 1 };
        }

        [Test]
        public void GetAll_ReturnsAllQueues()
        {
            //Arrange
            _queueService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<QueueDTO>());

            //Act
            var actionResult = _queueController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<QueueDTO>>>();
        }

        [Test]
        public void GetById_ReturnsQueue()
        {
            //Arrange
            _queueService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new QueueDTO { Id = 1 });

            //Act
            var actionResult = _queueController.GetById(1);

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<QueueDTO>>();
        }

        [Test]
        public void Add_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _queueService.Setup(s => s.AddAsync(queue));

            //Act
            var actionResult = _queueController.Add(queue).Result as CreatedAtActionResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(201);
        }

        [Test]
        public void Add_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _queueService.Setup(s => s.AddAsync(queue)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _queueController.Add(queue).Result as BadRequestObjectResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _queueService.Setup(s => s.UpdateAsync(queue));

            //Act
            var actionResult = _queueController.Update(queue).Result as OkResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _queueService.Setup(s => s.UpdateAsync(queue));

            //Act
            var actionResult = _queueController.Update(null).Result as BadRequestResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _queueService.Setup(s => s.DeleteByIdAsync(queue.Id));

            //Act
            var actionResult = _queueController.Delete(queue.Id).Result as OkResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Equals(200);
        }
    }
}
