using BL.DTO.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsultationManager.IntegrationTests.ControllersTests
{
    [TestFixture]
    public class SubjectControllerTests
    {
        private HttpClient _httpClient;
        private CustomWebApplicationFactory _factory;
        private const string requestURI = "api/subject/";

        public SubjectControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
        }

        [SetUp]
        public void SetUp()
        {
            _httpClient = _factory.CreateClient();
        }

        [Test]
        public async Task GetAll_ReturnsAllEntities()
        {
            // Act
            var responce = await _httpClient.GetAsync(requestURI);
            var responceString = await responce.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(responceString);

            //Assert
            responce.EnsureSuccessStatusCode();
            entities.Should().HaveCount(2);
        }

        [Test]
        public async Task GetById_ReturnsEntity()
        {
            // Act
            var responce = await _httpClient.GetAsync(requestURI + 1);
            var responceString = await responce.Content.ReadAsStringAsync();
            var entity = JsonConvert.DeserializeObject<SubjectDTO>(responceString);

            //Assert
            responce.EnsureSuccessStatusCode();
            entity.Id.Should().Be(1);
            entity.Name.Should().Be("Subject1");
        }

        [Test]
        public async Task Add_WhenModelIsValid_ReturnsSuccess()
        {
            //Arrange
            var formModel = new SubjectDTO
            {
                Id = 3,
                Name = "Subject3",
            };

            var json = JsonConvert.SerializeObject(formModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var responce = await _httpClient.PostAsync("api/subject", data); 
            var responceString = responce.Content.ReadAsStringAsync().Result;

            //Assert
            responce.EnsureSuccessStatusCode();
            responceString.Should().Contain("Subject3");
        }
    }
}
