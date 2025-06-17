//// ExceptionInterestRepositoryTests.cs
//using BlazorApp.DLL.DBContext;
//using BlazorApp.DLL.Repository;
//using BlazorApp.Model.Entities.AuthDB.Pricing;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using FluentAssertions;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace BlazorApp.Test.Repository
//{
//    public class ExceptionInterestRepositoryTests
//    {
//        private AuthDbContext _context;
//        private ExceptionInterestRepository _repository;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<AuthDbContext>()
//                .UseSqlServer("Server=LAPTOP-7FF2JANM;Database=AuthDB;Trusted_Connection=True;TrustServerCertificate=True")
//                .Options;

//            _context = new AuthDbContext(options);
//            _context.Database.EnsureDeleted();
//            _context.Database.EnsureCreated();

//            _repository = new ExceptionInterestRepository(_context);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            _context?.Dispose();
//        }

//        [Test]
//        public async Task GetAllProductDetails_ReturnsActiveProducts()
//        {
//            _context.Products.Add(new Product { Name = "Product1", IsActive = true });
//            await _context.SaveChangesAsync();

//            var result = await _repository.GetAllProductDetails();

//            result.Should().NotBeEmpty();
//        }

//        [Test]
//        public async Task GetExceptionInterestDetails_ReturnsMatchingModelId()
//        {
//            var product = new Product { Name = "TestProduct", IsActive = true };
//            _context.Products.Add(product);
//            await _context.SaveChangesAsync();

//            _ = _context.ExceptionInterests.Add(new ExceptionInterest
//            {
//                Modelid = 1,
//                ProductId = product.ID,
//                Product = product,
//                IsActive = true,
//                CreatedDate = DateOnly.FromDateTime(DateTime.Today)
//            });
//            await _context.SaveChangesAsync();

//            var result = await _repository.GetExceptionInterestDetails(1);

//            result.Should().NotBeEmpty();
//        }

//        [Test]
//        public async Task AddEIToDb_AddsSuccessfully()
//        {
//            var ei = new ExceptionInterest { Modelid = 1, ProductId = 1, CreatedDate = DateOnly.FromDateTime(DateTime.Today), IsActive = true };
//            var result = await _repository.AddEIToDb(ei);
//            result.Should().BeTrue();
//        }

//        [Test]
//        public async Task EditEIInDb_UpdatesSuccessfully()
//        {
//            var ei = new ExceptionInterest { Modelid = 2, AverageCollectedBalance = 100, IsActive = true };
//            _context.ExceptionInterests.Add(ei);
//            await _context.SaveChangesAsync();

//            var dto = new List<Model.CustomModels.Pricing.ExceptionInterestDto>
//            {
//                new Model.CustomModels.Pricing.ExceptionInterestDto { Id = ei.Id, AverageCollectedBalance = 200 }
//            };

//            var result = await _repository.EditEIInDb(2, dto);

//            result.Should().BeTrue();
//            (await _context.ExceptionInterests.FindAsync(ei.Id)).AverageCollectedBalance.Should().Be(200);
//        }

//        [Test]
//        public async Task SetEIStatus_DeactivatesCorrectly()
//        {
//            var ei = new ExceptionInterest { IsActive = true };
//            _context.ExceptionInterests.Add(ei);
//            await _context.SaveChangesAsync();

//            var result = await _repository.SetEIStatus(ei.Id);

//            result.Should().BeTrue();
//            (await _context.ExceptionInterests.FindAsync(ei.Id)).IsActive.Should().BeFalse();
//        }
//    }
//}
