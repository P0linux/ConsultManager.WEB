using BL.Abstraction;
using BL.DTO.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Controllers;

namespace ConsultationManager.UnitTests.ControllersTests
{
    [TestFixture]
    class SubjectControllerTests
    {
        private Mock<ISubjectService> _subjectService;
        private SubjectController _subjectController;
        private SubjectDTO subject;

        [SetUp]
        public void SetUp()
        {
            _subjectService = new Mock<ISubjectService>();
            _subjectController = new SubjectController(_subjectService.Object);

            subject = new SubjectDTO { Id = 1, Name = "SubjectName" };
        }

        [Test]
        public void GetAll_ReturnsAllSubjects()
        {
            //Arrange
            _subjectService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<SubjectDTO>());

            //Act
            var actionResult = _subjectController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<SubjectDTO>>>();
        }

        [Test]
        public void GetById_ReturnsSubject()
        {
            //Arrange
            _subjectService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new SubjectDTO { Id = 1 });

            //Act
            var actionResult = _subjectController.GetById(1);

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<SubjectDTO>>();
        }

        [Test]
        public void Add_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _subjectService.Setup(s => s.AddAsync(subject));

            //Act
            var actionResult = _subjectController.Add(subject).Result as CreatedAtActionResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Equals(201);
        }

        [Test]
        public void Add_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _subjectService.Setup(s => s.AddAsync(subject)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _subjectController.Add(subject).Result as BadRequestObjectResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Equals(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _subjectService.Setup(s => s.UpdateAsync(subject));

            //Act
            var actionResult = _subjectController.Update(subject).Result as OkResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Equals(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _subjectService.Setup(s => s.UpdateAsync(subject));

            //Act
            var actionResult = _subjectController.Update(null).Result as BadRequestResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Equals(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _subjectService.Setup(s => s.DeleteByIdAsync(subject.Id));

            //Act
            var actionResult = _subjectController.Delete(subject.Id).Result as OkResult;

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Equals(200);
        }
    }
}
