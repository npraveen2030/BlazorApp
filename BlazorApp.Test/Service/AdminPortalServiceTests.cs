using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BlazorApp.Test.Service;
[TestFixture]
public class AdminPortalServiceTests
{
    private Mock<IAdminPortalRepository> _repositoryMock;
    private IAdminPortalService _service;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IAdminPortalRepository>();
        _service = new AdminPortalService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetUserPreferenceTabById_ReturnsTabId()
    {
        _repositoryMock.Setup(r => r.GetUserPreferenceTabById(1)).ReturnsAsync(5);

        var result = await _service.GetUserPreferenceTabById(1);

        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public async Task SaveUserPreferences_ReturnsTrue()
    {
        _repositoryMock.Setup(r => r.SaveUserPreferences(1, 2)).ReturnsAsync(true);

        var result = await _service.SaveUserPreferences(1, 2);

        Assert.That(result, Is.True);
    }
}