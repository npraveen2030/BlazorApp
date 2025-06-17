using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Test.Service
{
    public class UserAssociatedProjectsServiceTests
    {
        private UserAssociatedProjectsService _service;
        private Mock<IUserAssociatedProjectsRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IUserAssociatedProjectsRepository>();
            _service = new UserAssociatedProjectsService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetUserNameFromIdAsync_ReturnsName()
        {
            _repositoryMock.Setup(r => r.GetNameFromIdAsync(1)).ReturnsAsync("test");

            var result = await _service.GetUserNameFromIdAsync(1);

            Assert.That(result, Is.EqualTo("test")); // Fixed: Replaced Assert.AreEqual with Assert.That and Is.EqualTo  
        }

        [Test]
        public async Task GetAssociatedProjects_ReturnsList()
        {
            var projects = new List<UserProjectRoleAssociationDto>();
            _repositoryMock.Setup(r => r.GetAssociatedProjectsFromDb(1)).ReturnsAsync(projects);

            var result = await _service.GetAssociatedProjects(1);

            Assert.That(result, Is.EqualTo(projects)); // Fixed: Replaced Assert.AreEqual with Assert.That and Is.EqualTo  
        }
    }
}
