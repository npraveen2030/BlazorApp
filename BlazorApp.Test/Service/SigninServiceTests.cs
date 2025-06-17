using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.Entities.AuthDB.Core;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Test.Service
{
    public class SigninServiceTests
    {
        private SigninService _service;
        private Mock<ISigninRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<ISigninRepository>();
            _service = new SigninService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetUserByNameAsync_ReturnsUser()
        {
            var user = new UserDetail();
            _repositoryMock.Setup(r => r.GetByNameAsync("test")).ReturnsAsync(user);

            var result = await _service.GetUserByNameAsync("test");

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public async Task GetUserAssociatedRolesByIdAsync_ReturnsRoles()
        {
            var roles = new List<int> { 1, 2 };
            _repositoryMock.Setup(r => r.GetRolesByIdAsync(1)).ReturnsAsync(roles);

            var result = await _service.GetUserAssociatedRolesByIdAsync(1);

            Assert.That(result, Is.EqualTo(roles));
        }
    }
}
