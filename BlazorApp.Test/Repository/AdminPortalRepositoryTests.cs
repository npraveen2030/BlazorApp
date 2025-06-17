//using BlazorApp.DLL.DBContext;
//using BlazorApp.DLL.Repository;
//using BlazorApp.Model.Entities.AuthDB.Core;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using BlazorApp.Model.Entities.AuthDB.Personalization;
//using FluentAssertions;

//namespace BlazorApp.Test.Repository
//{
//    public class AdminPortalRepositoryTests
//    {
//        private AuthDbContext _context;
//        private AdminPortalRepository _repository;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<AuthDbContext>()
//        .UseSqlServer("Server=LAPTOP-7FF2JANM;Database=AuthDB;Trusted_Connection=True;TrustServerCertificate=True")
//        .Options;

//            _context = new AuthDbContext(options);
//            _context.Database.EnsureDeleted();
//            _context.Database.EnsureCreated();

//            _repository = new AdminPortalRepository(_context);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            _context?.Dispose();
//        }

//        [Test]
//        public async Task GetUserPreferenceTabById_ReturnsCorrectTabId()
//        {
//            _context.UserTabAssocs.Add(new UserTabAssoc { UserId = 1, TabId = 3 });
//            await _context.SaveChangesAsync();

//            var result = await _repository.GetUserPreferenceTabById(1);

//            result.Should().Be(3);
//        }

//        [Test]
//        public async Task GetUserPreferenceTabById_ReturnsZero_WhenUserNotFound()
//        {
//            var result = await _repository.GetUserPreferenceTabById(99);

//            result.Should().Be(0);
//        }

//        [Test]
//        public async Task SaveUserPreferences_UpdatesTabIdAndReturnsTrue()
//        {
//            _context.UserTabAssocs.Add(new UserTabAssoc { UserId = 2, TabId = 1 });
//            await _context.SaveChangesAsync();

//            var result = await _repository.SaveUserPreferences(2, 5);
//            var updated = await _context.UserTabAssocs.FirstOrDefaultAsync(x => x.UserId == 2);

//            result.Should().BeTrue();
//            updated.TabId.Should().Be(5);
//        }

//        [Test]
//        public async Task SaveUserPreferences_ReturnsFalse_WhenUserNotFound()
//        {
//            var result = await _repository.SaveUserPreferences(99, 10);

//            result.Should().BeFalse();
//        }
//    }
//}
