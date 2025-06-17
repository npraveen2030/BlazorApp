using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.CustomModels.Personalization;
using Moq;

namespace BlazorApp.Test.Service
{
    public class ProjectLeadPortalServiceTests
    {
        private Mock<IProjectLeadPortalRepository> _mockRepo;
        private ProjectLeadPortalService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IProjectLeadPortalRepository>();
            _service = new ProjectLeadPortalService(_mockRepo.Object);
        }

        [Test]
        public async Task GridPreferenceFetcher_ReturnsGridMetaDictionary()
        {
            var dummyData = new Dictionary<string, GridMeta> { { "UserName", new GridMeta() } };
            _mockRepo.Setup(r => r.GridPreferenceFetcherFromDb(1)).ReturnsAsync((true, dummyData));

            var result = await _service.GridPreferenceFetcher(1);

            Assert.That(result.Item1, Is.True); // Updated to use NUnit's Assert.That
            Assert.That(result.Item2, Is.EqualTo(dummyData));
        }

        [Test]
        public async Task FetchUserDetailsFromIdAndRole_ReturnsDto()
        {
            var dto = new UserProjectRoleAssociationDto { ProjectId = 42 };
            _mockRepo.Setup(r => r.FetchUserDetailsFromIdAndRole(1, 12)).ReturnsAsync((true, dto));

            var result = await _service.FetchUserDetailsFromIdAndRole(1, 12);

            Assert.That(result.Item1, Is.True); // Updated to use NUnit's Assert.That
            Assert.That(result.Item2.ProjectId, Is.EqualTo(42));
        }

        [Test]
        public async Task FetchAssociation_ReturnsList()
        {
            var list = new List<UserProjectRoleAssociationDto> { new UserProjectRoleAssociationDto() };
            _mockRepo.Setup(r => r.FetchAssociationFromDb(42)).ReturnsAsync(list);

            var result = await _service.FetchAssociation(42);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetActiveUsersFromProjectId_ReturnsList()
        {
            var list = new List<UserProjectRoleAssociationDto> { new UserProjectRoleAssociationDto() };
            _mockRepo.Setup(r => r.GetActiveUsersFromProjectId(42)).ReturnsAsync(list);

            var result = await _service.GetActiveUsersFromProjectId(42);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAvailableRoles_ReturnsRolesList()
        {
            var roles = new List<UserProjectRoleAssociationDto> { new UserProjectRoleAssociationDto() };
            _mockRepo.Setup(r => r.GetAvailableRolesFromDb()).ReturnsAsync(roles);

            var result = await _service.GetAvailableRoles();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AddAssociation_ReturnsTrue_OnSuccess()
        {
            var dto = new UserProjectRoleAssociationDto();
            _mockRepo.Setup(r => r.AddAssociationToDb(dto)).ReturnsAsync(true);

            var result = await _service.AddAssociation(dto);

            Assert.That(result, Is.True); // Updated to use NUnit's Assert.That
        }

        [Test]
        public async Task AddAssociation_ReturnsFalse_OnFailure()
        {
            var dto = new UserProjectRoleAssociationDto();
            _mockRepo.Setup(r => r.AddAssociationToDb(dto)).ReturnsAsync(false);

            var result = await _service.AddAssociation(dto);

            Assert.That(result, Is.False); // Updated to use NUnit's Assert.That
        }

        [Test]
        public async Task DeleteAssociation_ReturnsTrue_OnSuccess()
        {
            _mockRepo.Setup(r => r.DeleteAssociationFromDb(100, 1)).ReturnsAsync(true);

            var result = await _service.DeleteAssociation(100, 1);

            Assert.That(result, Is.True); // Updated to use NUnit's Assert.That
        }

        [Test]
        public async Task DeleteAssociation_ReturnsFalse_OnFailure()
        {
            _mockRepo.Setup(r => r.DeleteAssociationFromDb(100, 1)).ReturnsAsync(false);

            var result = await _service.DeleteAssociation(100, 1);

            Assert.That(result, Is.False); // Updated to use NUnit's Assert.That
        }

        [Test]
        public async Task GridPreferenceSaver_ReturnsTrue_OnSuccess()
        {
            var grid = new Dictionary<string, GridMeta> { { "UserName", new GridMeta() } };
            _mockRepo.Setup(r => r.GridPreferenceSaverToDb(grid, 1, 2, 3)).ReturnsAsync(true);

            var result = await _service.GridPreferenceSaver(grid, 1, 2, 3);

            Assert.That(result, Is.True); // Updated to use NUnit's Assert.That
        }

        [Test]
        public async Task GridPreferenceSaver_ReturnsFalse_OnFailure()
        {
            var grid = new Dictionary<string, GridMeta>();
            _mockRepo.Setup(r => r.GridPreferenceSaverToDb(grid, 1, 2, 3)).ReturnsAsync(false);

            var result = await _service.GridPreferenceSaver(grid, 1, 2, 3);

            Assert.That(result, Is.False); // Updated to use NUnit's Assert.That
        }
    }
}
