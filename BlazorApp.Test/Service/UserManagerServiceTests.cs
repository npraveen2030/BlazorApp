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
    public class UserManagerServiceTests
    {
        private UserManagerService _service;
        private Mock<IUserManagerRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IUserManagerRepository>();
            _service = new UserManagerService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetAllUsers_ReturnsList()
        {
            var users = new List<UserDetailDto>();
            _repositoryMock.Setup(r => r.GetAllUsers()).ReturnsAsync(users);

            var result = await _service.GetAllUsers();

            Assert.That(result, Is.EqualTo(users));
        }

        [Test]
        public async Task AddUserToDb_CallsRepo()
        {
            var user = new UserDetail();
            _repositoryMock.Setup(r => r.AddUserToDb(user)).ReturnsAsync(true);

            var result = await _service.AddUserToDb(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task EditUserInDb_ReturnsTrue()
        {
            _repositoryMock.Setup(r => r.EditUserInDb(1, 1, It.IsAny<string>(), true)).ReturnsAsync(true);

            var result = await _service.EditUserInDb(1, 1, "pass", true);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task SetUserStatus_ReturnsTrue()
        {
            _repositoryMock.Setup(r => r.SetUserStatus(1, true)).ReturnsAsync(true);

            var result = await _service.SetUserStatus(1, true);

            Assert.That(result, Is.True);
        }
    }
}
