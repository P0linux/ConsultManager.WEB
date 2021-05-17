using BL.Abstraction;
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
    public class QueueMemberControllerTests
    {
        private Mock<IQueueMemberService> _queueMemberService;
        private QueueMemberController _queueMemberController;
        private QueueMemberDTO queueMember;

        [SetUp]
        public void SetUp()
        {
            _queueMemberService = new Mock<IQueueMemberService>();
            _queueMemberController = new QueueMemberController(_queueMemberService.Object);

            queueMember = new QueueMemberDTO { Id = 1, QueueId = 1, StudentId = 1, Priority = 1, IsAbsent = false };
        }

        [Test]
        public void GetAll_ReturnsAllQueueMembers()
        {
            //Arrange
            _queueMemberService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<QueueMemberDTO>());

            //Act
            var actionResult = _queueMemberController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<QueueMemberDTO>>>();
        }

        [Test]
        public void GetById_ReturnsQueueMember()
        {
            //Arrange
            _queueMemberService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new QueueMemberDTO { Id = 1 });

            //Act
            var actionResult = _queueMemberController.GetById(1);

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<QueueMemberDTO>>();
        }

        [Test]
        public void Add_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _queueMemberService.Setup(s => s.AddAsync(queueMember));

            //Act
            var actionResult = _queueMemberController.Add(queueMember).Result as CreatedAtActionResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(201);
        }

        [Test]
        public void Add_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _queueMemberService.Setup(s => s.AddAsync(queueMember)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _queueMemberController.Add(queueMember).Result as BadRequestObjectResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _queueMemberService.Setup(s => s.UpdateAsync(queueMember));

            //Act
            var actionResult = _queueMemberController.Update(queueMember).Result as OkResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _queueMemberService.Setup(s => s.UpdateAsync(queueMember));

            //Act
            var actionResult = _queueMemberController.Update(null).Result as BadRequestResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _queueMemberService.Setup(s => s.DeleteByIdAsync(queueMember.Id));

            //Act
            var actionResult = _queueMemberController.Delete(queueMember.Id).Result as OkResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Equals(200);
        }
    }
}
