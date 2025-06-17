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
    public class UserProjectRoleAssociationServiceTests
    {
        private UserProjectRoleAssociationService _service;
        private Mock<IUserProjectRoleAssociationRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IUserProjectRoleAssociationRepository>();
            _service = new UserProjectRoleAssociationService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetAllUpras_ReturnsList()
        {
            var upras = new List<UserProjectRoleAssociationDto>();
            _repositoryMock.Setup(r => r.GetAllUpras()).ReturnsAsync(upras);

            var result = await _service.GetAllUpras();

            Assert.That(result, Is.EqualTo(upras));
        }

        [Test]
        public async Task AddUpraToDb_ReturnsTrue()
        {
            var upra = new UserProjectRoleAssociation();
            _repositoryMock.Setup(r => r.AddUpraToDb(upra)).ReturnsAsync(true);

            var result = await _service.AddUpraToDb(upra);

            Assert.That(result, Is.True);
        }
    }
}
