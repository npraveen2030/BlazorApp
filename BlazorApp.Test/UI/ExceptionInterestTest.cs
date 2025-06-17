using Bunit;
using Bunit.TestDoubles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.UI.Components.Pages.Pricing;
using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;
using BlazorApp.Model.Entities.AuthDB.Pricing;
using FluentAssertions;

namespace BlazorApp.Test.UI
{
    [TestFixture]
    public class ExceptionInterestTests : Bunit.TestContext
    {
        private readonly Mock<IExceptionInterestService> _exceptionServiceMock;
        private readonly Mock<IJSRuntime> _jsMock;

        public ExceptionInterestTests()
        {
            _exceptionServiceMock = new Mock<IExceptionInterestService>();
            _jsMock = new Mock<IJSRuntime>();

            Services.AddSingleton(_exceptionServiceMock.Object);
            Services.AddSingleton(_jsMock.Object);

            _exceptionServiceMock.Setup(s => s.GetExceptionInterestDetails(It.IsAny<int>()))
                .ReturnsAsync(new List<ExceptionInterestDto>
                {
                    new ExceptionInterestDto { ProductId = 1, AccountNumber = "123" }
                });

            _exceptionServiceMock.Setup(s => s.GetAllProductDetails())
                .ReturnsAsync(new List<ProductDto>
                {
                    new ProductDto
                    {
                        Id = 1,
                        StandardInteresetRate = 1.1m,
                        AnnualizedStandardInterestExpense = 100,
                        CurrentInterestRate = 1.2m,
                        AnnualizedCurrentInterestExpense = "200",
                        AppliedInterestRate = 1.3m,
                        AnnualizedInterestExpense = 300
                    }
                });

            _exceptionServiceMock.Setup(s => s.AddEIToDb(It.IsAny<ExceptionInterest>()))
                .ReturnsAsync(true);

            _exceptionServiceMock.Setup(s => s.EditEIInDb(It.IsAny<int>(), It.IsAny<List<ExceptionInterestDto>>()))
                .ReturnsAsync(true);
        }

        [Test]
        public async Task OnInitializedAsync_LoadsInitialData()
        {
            var cut = RenderComponent<Exception_Interest>(parameters => parameters
                .Add(p => p.ModelId, 5)
                .Add(p => p.ModelName, "TestModel"));

            var instance = cut.Instance;

            Assert.That(instance.IsLoaded, Is.True); 
            Assert.That(instance.ExceptionInterestDetails.Count, Is.EqualTo(1));
            Assert.That(instance.ProductDetails.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task ProductIdValue_Setter_PopulatesFormFields()
        {
            var cut = RenderComponent<Exception_Interest>(parameters => parameters
                .Add(p => p.ModelId, 1));

            var instance = cut.Instance;

            await cut.InvokeAsync(() =>
            {
                instance.ProductIdValue = 1;
            });

            Assert.That(instance.EIFormDetails.ProductId, Is.EqualTo(1));
            Assert.That(instance.EIFormDetails.StandardInterestRate, Is.EqualTo(1.1m));
            Assert.That(instance.EIFormDetails.CurrentInteresetRate, Is.EqualTo(1.2m));
            Assert.That(instance.EIFormDetails.AnnualizedCurrentInteresetExpense, Is.EqualTo(200));
            Assert.That(instance.EIFormDetails.AppliedInterestRate, Is.EqualTo(1.3m));
            Assert.That(instance.EIFormDetails.AnnualizedInterestExpense, Is.EqualTo(300));
        }

        [Test]
        public async Task HandleSaveChanges_ClearsFormAndRefreshesList()
        {
            var cut = RenderComponent<Exception_Interest>(parameters => parameters
                .Add(p => p.ModelId, 10));

            var instance = cut.Instance;

            instance.EIFormDetails.ProductId = 1;
            instance.EIFormDetails.AccountNumber = "456";

            await cut.InvokeAsync(() => instance.HandleSaveChanges());

            _exceptionServiceMock.Verify(s => s.AddEIToDb(It.IsAny<ExceptionInterest>()), Times.Once);
            _exceptionServiceMock.Verify(s => s.GetExceptionInterestDetails(10), Times.Exactly(2)); // once in init, once after save

            Assert.That(instance.EIFormDetails.ProductId, Is.Null);
            Assert.That(instance.ExceptionInterestDetails.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task HandleEdits_CallsServiceAndShowsToast()
        {
            var cut = RenderComponent<Exception_Interest>(parameters => parameters
                .Add(p => p.ModelId, 2));

            var instance = cut.Instance;

            await cut.InvokeAsync(() => instance.HandleEdits());

            _exceptionServiceMock.Verify(s => s.EditEIInDb(2, It.IsAny<List<ExceptionInterestDto>>()), Times.Once);

            var toastInvocation = _jsMock.Invocations.FirstOrDefault(i =>
                i.Method.Name == "InvokeVoidAsync"
                && i.Arguments[0] as string == "bootstrapInterop.showToast"
                && i.Arguments[1] is object[] args
                && args.Length == 1
                && args[0] as string == "saveEditstoast");

            toastInvocation.Should().NotBeNull("because 'bootstrapInterop.showToast' should be called with 'saveEditstoast'");
        }


        [Test]
        public void HandleResetForm_ClearsForm()
        {
            var cut = RenderComponent<Exception_Interest>();
            var instance = cut.Instance;

            instance.EIFormDetails.ProductId = 99;

            instance.HandleResetForm();

            Assert.That(instance.EIFormDetails.ProductId, Is.Null);
        }

    }
}
