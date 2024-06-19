using Xunit;
using Moq;
using JediApi.Repositories;
using JediApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JediApi.Models;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        //não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_Success()
        {
            var jediId = 1;
            var expectedJedi = new Jedi { Id = jediId, Name = "Luke Skywalker" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(jediId)).ReturnsAsync(expectedJedi);

            var result = await _service.GetByIdAsync(jediId);

            Assert.NotNull(result);
            Assert.Equal(expectedJedi.Id, result.Id);
            Assert.Equal(expectedJedi.Name, result.Name);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            var jediId = 1;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(jediId)).ReturnsAsync((Jedi)null);

            var result = await _service.GetByIdAsync(jediId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll()
        {
            var expectedJedis = new List<Jedi>
            {
                new Jedi { Id = 1, Name = "Luke Skywalker" },
                new Jedi { Id = 2, Name = "Obi-Wan Kenobi" }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedJedis);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(expectedJedis.Count, result.Count());
            Assert.Contains(result, jedi => jedi.Name == "Luke Skywalker");
            Assert.Contains(result, jedi => jedi.Name == "Obi-Wan Kenobi");
        }
    }
}