using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;
using BlazorApp.Model.Entities.AuthDB.Pricing;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Test.Service;
[TestFixture]
public class ExceptionInterestServiceTests
{
    private Mock<IExceptionInterestRepository> _repositoryMock;
    private ExceptionInterestService _service;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IExceptionInterestRepository>();
        _service = new ExceptionInterestService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetAllProductDetails_ReturnsList()
    {
        var products = new List<ProductDto> { new ProductDto { Id = 1 } };
        _repositoryMock.Setup(r => r.GetAllProductDetails()).ReturnsAsync(products);

        var result = await _service.GetAllProductDetails();

        Assert.That(result, Is.EqualTo(products));
    }

    [Test]
    public async Task AddEIToDb_ReturnsTrue()
    {
        var ei = new ExceptionInterest();
        _repositoryMock.Setup(r => r.AddEIToDb(ei)).ReturnsAsync(true);

        var result = await _service.AddEIToDb(ei);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task EditEIInDb_ReturnsTrue()
    {
        var list = new List<ExceptionInterestDto>();
        _repositoryMock.Setup(r => r.EditEIInDb(1, list)).ReturnsAsync(true);

        var result = await _service.EditEIInDb(1, list);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task SetEIStatus_ReturnsTrue()
    {
        _repositoryMock.Setup(r => r.SetEIStatus(1)).ReturnsAsync(true);

        var result = await _service.SetEIStatus(1);

        Assert.That(result, Is.True);
    }
}