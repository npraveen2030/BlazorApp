using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Test.Service
{
    public class ProjectManagerServiceTests
    {
        private ProjectManagerService _service;
        private Mock<IProjectManagerRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IProjectManagerRepository>();
            _service = new ProjectManagerService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetAllProjects_ReturnsList()
        {
            var projects = new List<ProjectDto> { new ProjectDto() };
            _repositoryMock.Setup(r => r.GetAllProjects()).ReturnsAsync(projects);

            var result = await _service.GetAllProjects();

            Assert.That(result, Is.EqualTo(projects));
        }

        [Test]
        public async Task AddProjectToDb_CallsRepository()
        {
            var project = new Project();
            _repositoryMock.Setup(r => r.AddProjectToDb(project)).ReturnsAsync(true);

            var result = await _service.AddProjectToDb(project);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task EditUserInDb_ReturnsTrue()
        {
            _repositoryMock.Setup(r => r.EditUserInDb(1, 1, "Project", true)).ReturnsAsync(true);

            var result = await _service.EditUserInDb(1, 1, "Project", true);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task SetProjectStatus_ReturnsTrue()
        {
            _repositoryMock.Setup(r => r.SetProjectStatus(1, true)).ReturnsAsync(true);

            var result = await _service.SetProjectStatus(1, true);

            Assert.That(result, Is.True);
        }
    }
}
