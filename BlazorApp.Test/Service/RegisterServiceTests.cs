using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.Entities.AuthDB.Core;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BlazorApp.Test.Service
{
    public class RegisterServiceTests
    {
        private RegisterService _service;
        private Mock<IRegisterRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRegisterRepository>();
            _service = new RegisterService(_repositoryMock.Object);
        }

        [Test]
        public async Task AddUserAsync_CallsRepository()
        {
            var user = new UserDetail();
            _repositoryMock.Setup(r => r.CreateUserAsync(user)).Returns(Task.CompletedTask);

            await _service.AddUserAsync(user);

            _repositoryMock.Verify(r => r.CreateUserAsync(user), Times.Once);
        }
    }
}
