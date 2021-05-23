using BL.DTO.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
           var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/subject/Add/");

            var formModel = new Dictionary<string, string>
            {
                {"Name", "Subject3" },
                {"Id", "3"},
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            //Act
            var responce = await _httpClient.SendAsync(postRequest);
            var responceString = await responce.Content.ReadAsStringAsync();

            //Assert
            responce.EnsureSuccessStatusCode();
            responceString.Should().Contain("Subject3");

        }
    }
}
