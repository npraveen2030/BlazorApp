using Bunit;
using Bunit.TestDoubles;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.CustomModels.Personalization;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Common;
using BlazorApp.UI.Components.Pages.Admin;
using FluentAssertions;

namespace BlazorApp.Tests.ComponentTests.Admin
{
    [TestFixture]
    public class ProjectManagerTests : Bunit.TestContext
    {
        private readonly Mock<IProjectManagerService> _pmServiceMock;
        private readonly Mock<IUserAssociatedProjectsService> _userProjectServiceMock;
        private readonly Mock<IProjectLeadPortalService> _leadServiceMock;
        private readonly Mock<IJSRuntime> _jsMock;
        private readonly TestAuthorizationContext _authContext;
        private readonly UserSession _testSession;
        private readonly List<ProjectDto> _sampleProjects;

        public ProjectManagerTests()
        {
            _pmServiceMock = new Mock<IProjectManagerService>();
            _userProjectServiceMock = new Mock<IUserAssociatedProjectsService>();
            _leadServiceMock = new Mock<IProjectLeadPortalService>();
            _jsMock = new Mock<IJSRuntime>();

            Services.AddSingleton(_pmServiceMock.Object);
            Services.AddSingleton(_userProjectServiceMock.Object);
            Services.AddSingleton(_leadServiceMock.Object);
            Services.AddSingleton(_jsMock.Object);

            _authContext = this.AddTestAuthorization();
            _authContext.SetAuthorized("123");
            _authContext.SetClaims(new Claim(ClaimTypes.Role, "Admin"));

            _userProjectServiceMock.Setup(s => s.GetUserNameFromIdAsync(It.IsAny<int>())).ReturnsAsync("admin");

            _leadServiceMock.Setup(s => s.GridPreferenceFetcher(It.IsAny<int>()))
                .ReturnsAsync((true, new Dictionary<string, GridMeta>
                {
                    ["ProjectId"] = new GridMeta(),
                    ["ProjectName"] = new GridMeta(),
                    ["CreatedBy"] = new GridMeta(),
                    ["CreatedDate"] = new GridMeta(),
                    ["ModifiedBy"] = new GridMeta(),
                    ["ModifiedDate"] = new GridMeta(),
                    ["IsActive"] = new GridMeta()
                }));

            _testSession = new UserSession
            {
                UserId = 123,
                UserName = "admin",
                UserRoles = new List<string> { "Admin" }
            };

            _sampleProjects = new List<ProjectDto>
            {
                new ProjectDto { ProjectId = 1, ProjectName = "Test Project", IsActive = true }
            };

            _pmServiceMock.Setup(s => s.GetAllProjects()).ReturnsAsync(_sampleProjects);
        }

        [Test]
        public async Task AddProjectSubmitHandler_AddsAndRefreshes()
        {
            var cut = RenderComponent<Project_Manager>(p => p.Add(c => c.SessionDetails, _testSession));
            var instance = cut.Instance;

            instance.AddProjectForm.ProjectName = "New Project";

            _pmServiceMock.Setup(p => p.AddProjectToDb(It.IsAny<Project>())).ReturnsAsync(true);
            _pmServiceMock.Setup(p => p.GetAllProjects()).ReturnsAsync(new List<ProjectDto>
            {
                new ProjectDto { ProjectId = 2, ProjectName = "New Project", IsActive = true }
            });

            await cut.InvokeAsync(() => instance.AddProjectSubmitHandler());

            _pmServiceMock.Setup(p => p.AddProjectToDb(It.IsAny<Project>()))
                        .Callback<Project>(proj =>
                        {
                            Console.WriteLine($"ProjectName: {proj.ProjectName}, CreatedBy: {proj.CreatedBy}, isActive: {proj.isActive}");
                        });

            _pmServiceMock.Verify(p => p.GetAllProjects(), Times.Exactly(2));
            _jsMock.Verify(js =>
                        js.InvokeAsync<object>(
                            "bootstrapInterop.hideModal",
                            It.Is<object[]>(o => o.Length == 1 && o[0]!.ToString() == "#AddProjectModal")
                        ),
                        Times.Once);
        }

        [Test]
        public async Task HandleEdit_SetsIsEditTrue_WhenNotEditing()
        {
            var cut = RenderComponent<Project_Manager>(p => p.Add(c => c.SessionDetails, _testSession));
            var instance = cut.Instance;
            var project = new ProjectDto { ProjectId = 1, ProjectName = "Edit Me", IsActive = true, IsEdit = false };

            await cut.InvokeAsync(() => instance.HandleEdit(project));

            Assert.That(project.IsEdit, Is.True);
            Assert.That(instance.Editing, Is.True);
        }

        [Test]
        public async Task HandleSubmit_UpdatesProjectAndRefreshes()
        {
            var cut = RenderComponent<Project_Manager>(p => p.Add(c => c.SessionDetails, _testSession));
            var instance = cut.Instance;
            var project = new ProjectDto { ProjectId = 1, ProjectName = "Updated Name", IsActive = true, IsEdit = true };

            _pmServiceMock.Setup(p => p.EditUserInDb(project.ProjectId, _testSession.UserId, "Updated Name", true)).ReturnsAsync(true);

            await cut.InvokeAsync(() => instance.HandleSubmit(project));

            _pmServiceMock.Verify(p => p.EditUserInDb(project.ProjectId, _testSession.UserId, "Updated Name", true), Times.Once);
            Assert.That(project.IsEdit, Is.False);
            Assert.That(instance.Editing, Is.False);
        }

        [Test]
        public void HandleCancel_SetsEditToFalse()
        {
            var cut = RenderComponent<Project_Manager>(p => p.Add(c => c.SessionDetails, _testSession));
            var instance = cut.Instance;
            var project = new ProjectDto { ProjectId = 1, ProjectName = "To Cancel", IsActive = true, IsEdit = true };

            cut.InvokeAsync(() => instance.HandleCancel(project));

            Assert.That(project.IsEdit, Is.False);
            Assert.That(instance.Editing, Is.False);
        }

        //[Test]
        //public async Task HandleInActive_UpdatesStatusAndRefreshes()
        //{
        //    var jsInterop = new BunitJSInterop();
        //    Services.AddSingleton(jsInterop);

        //    var cut = RenderComponent<Project_Manager>(p => p.Add(c => c.SessionDetails, _testSession));
        //    var instance = cut.Instance;
        //    var project = new ProjectDto { ProjectId = 1, ProjectName = "Inactive Me", IsActive = true };

        //    _pmServiceMock.Setup(p => p.SetProjectStatus(project.ProjectId, false)).ReturnsAsync(true);
        //    _pmServiceMock.Setup(p => p.GetAllProjects()).ReturnsAsync(new List<ProjectDto>());

        //    await cut.InvokeAsync(() => instance.HandleInActive(project));

        //    _pmServiceMock.Verify(p => p.SetProjectStatus(project.ProjectId, false), Times.Once);

        //    var invocation = jsInterop.VerifyInvoke("bootstrapInterop.hideModal");
        //    invocation.Arguments[0].ToString().Should().Be("#DeleteProjectConfirmationModal");
        //}


        [Test]
        public async Task HandleActive_UpdatesStatusAndRefreshes()
        {
            var cut = RenderComponent<Project_Manager>(p => p.Add(c => c.SessionDetails, _testSession));
            var instance = cut.Instance;
            var project = new ProjectDto { ProjectId = 1, ProjectName = "Activate Me", IsActive = false };

            _pmServiceMock.Setup(p => p.SetProjectStatus(project.ProjectId, true)).ReturnsAsync(true);

            await cut.InvokeAsync(() => instance.HandleActive(project));

            _pmServiceMock.Verify(p => p.SetProjectStatus(project.ProjectId, true), Times.Once);
        }
    }
}
