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
    public class ProjectManagerPortalServiceTests
    {
        private ProjectMangerPortalService _service;
        private Mock<IProjectManagerPortalRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IProjectManagerPortalRepository>();
            _service = new ProjectMangerPortalService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetUserByIdAsync_ReturnsUserName()
        {
            _repositoryMock.Setup(r => r.GetUserByIdFromDbAsync(1)).ReturnsAsync("TestUser");

            var result = await _service.GetUserByIdAsync(1);

            Assert.That(result, Is.EqualTo("TestUser"));
        }

        [Test]
        public async Task FetchAssociations_ReturnsList()
        {
            var list = new List<UserProjectRoleAssociationDto> { new UserProjectRoleAssociationDto() };
            _repositoryMock.Setup(r => r.FetchAssociationsFromDb(1)).ReturnsAsync(list);

            var result = await _service.FetchAssociations(1);

            Assert.That(result, Is.EqualTo(list));
        }

        [Test]
        public async Task AddAssociation_ReturnsTrue()
        {
            var assoc = new UserProjectRoleAssociation();
            _repositoryMock.Setup(r => r.AddAssociationToDb(assoc)).ReturnsAsync(true);

            var result = await _service.AddAssociation(assoc);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteAssociation_ReturnsTrue()
        {
            _repositoryMock.Setup(r => r.DeleteAssociationFromDb(1)).ReturnsAsync(true);

            var result = await _service.DeleteAssociation(1);

            Assert.That(result, Is.True);
        }
    }
}
