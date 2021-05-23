using BL.DTO.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsultationManager.IntegrationTests.ControllersTests
{
    [TestFixture]
    public class QueueControllerTests
    {
        private HttpClient _httpClient;
        private CustomWebApplicationFactory _factory;
        private const string requestURI = "api/queue/";

        public QueueControllerTests()
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
            var entities = JsonConvert.DeserializeObject<IEnumerable<QueueDTO>>(responceString);

            //Assert
            responce.EnsureSuccessStatusCode();
            entities.Should().HaveCount(1);
        }

        [Test]
        public async Task GetById_ReturnsEntity()
        {
            // Act
            var responce = await _httpClient.GetAsync(requestURI + 1);
            var responceString = await responce.Content.ReadAsStringAsync();
            var entity = JsonConvert.DeserializeObject<QueueDTO>(responceString);

            //Assert
            responce.EnsureSuccessStatusCode();
            entity.Id.Should().Be(1);
            entity.ConsultationId.Should().Be(1);
        }
    }
}
