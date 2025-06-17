using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Test.Service
{
    public class ModelsServiceTests
    {
        private ModelsService _service;
        private Mock<IModelsRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IModelsRepository>();
            _service = new ModelsService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetAllPricingModels_ReturnsList()
        {
            var models = new List<ModelDto> { new ModelDto() };
            _repositoryMock.Setup(r => r.GetAllPricingModels()).ReturnsAsync(models);

            var result = await _service.GetAllPricingModels();

            Assert.That(result, Is.EqualTo(models)); // Replace Assert.AreEqual with Assert.That and Is.EqualTo
        }

        [Test]
        public async Task GetExceptionInterestDetails_ReturnsList()
        {
            var tabs = new List<TabDto> { new TabDto() };
            _repositoryMock.Setup(r => r.GetExceptionInterestDetails()).ReturnsAsync(tabs);

            var result = await _service.GetExceptionInterestDetails();

            Assert.That(result, Is.EqualTo(tabs)); // Replace Assert.AreEqual with Assert.That and Is.EqualTo
        }
    }
}
