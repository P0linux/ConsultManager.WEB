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
    public class ConsultationControllerTests
    {
        private Mock<IConsultationService> _consultationService;
        private ConsultationController _consultationController;
        private ConsultationDTO consultation;

        [SetUp]
        public void SetUp()
        {
            _consultationService = new Mock<IConsultationService>();
            _consultationController = new ConsultationController(_consultationService.Object);

            consultation = new ConsultationDTO { Id = 1, Date = new DateTime(2021, 6, 12), LecturerId = 1, SubjectId = 1 };
        }

        [Test]
        public void GetAll_ReturnsAllConsultations()
        {
            //Arrange
            _consultationService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<ConsultationDTO>());

            //Act
            var actionResult = _consultationController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<ConsultationDTO>>>(); 
        }

        [Test]
        public void GetById_ReturnsConsultation()
        {
            //Arrange
            _consultationService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new ConsultationDTO { Id = 1 });

            //Act
            var actionResult = _consultationController.GetById(1);

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<ConsultationDTO>>();
        }

        [Test]
        public void Add_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _consultationService.Setup(s => s.AddAsync(consultation));

            //Act
            var actionResult = _consultationController.Add(consultation).Result as CreatedAtActionResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(201);
        }

        [Test]
        public void Add_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _consultationService.Setup(s => s.AddAsync(consultation)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _consultationController.Add(consultation).Result as BadRequestObjectResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _consultationService.Setup(s => s.UpdateAsync(consultation));

            //Act
            var actionResult = _consultationController.Update(consultation).Result as OkResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _consultationService.Setup(s => s.UpdateAsync(consultation));

            //Act
            var actionResult = _consultationController.Update(null).Result as BadRequestResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _consultationService.Setup(s => s.DeleteByIdAsync(consultation.Id));

            //Act
            var actionResult = _consultationController.Delete(consultation.Id).Result as OkResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Equals(200);
        }
    }
}
