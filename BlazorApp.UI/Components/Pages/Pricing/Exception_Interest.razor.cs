using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;
using BlazorApp.Model.Entities.AuthDB.Pricing;

namespace BlazorApp.UI.Components.Pages.Pricing
{
    public partial class Exception_Interest : ComponentBase
    {
        [Parameter] public int ModelId { get; set; }
        [Parameter] public string ModelName { get;set;} = "";
        [Inject] public IJSRuntime JS { get; set; } = null!;
        [Inject] public IExceptionInterestService ExceptionInterestService { get; set; } = null!;
        public ExceptionInterestDto EIFormDetails { get;set;} = new();
        public List<ProductDto> ProductDetails { get; set; } = new();
        public List<ExceptionInterestDto> ExceptionInterestDetails { get; set; } = new();
        public bool IsLoaded { get;set; } = false;


        protected override async Task OnInitializedAsync()
        {
            ExceptionInterestDetails = await ExceptionInterestService.GetExceptionInterestDetails(ModelId);
            ProductDetails = await ExceptionInterestService.GetAllProductDetails();
            IsLoaded = true;
        }

        public int? ProductIdValue
        {
            get => EIFormDetails.ProductId;
            set
            {
                if (EIFormDetails.ProductId != value)
                {
                    EIFormDetails.ProductId = value;
                    if (value != null)
                    {
                        var product = ProductDetails.Where(p => p.Id == value).FirstOrDefault();
                        if (product != null)
                        {
                            EIFormDetails.StandardInterestRate = product.StandardInteresetRate;
                            EIFormDetails.AnnualizedStandardIneterestExpense = product.AnnualizedStandardInterestExpense;
                            EIFormDetails.CurrentInteresetRate = product.CurrentInterestRate;
                            if (decimal.TryParse(product.AnnualizedCurrentInterestExpense, out var parsedValue))
                            {
                                EIFormDetails.AnnualizedCurrentInteresetExpense = parsedValue;
                            }
                            else
                            {
                                EIFormDetails.AnnualizedCurrentInteresetExpense = null;
                            }
                            EIFormDetails.AppliedInterestRate = product?.AppliedInterestRate;
                            EIFormDetails.AnnualizedInterestExpense = product?.AnnualizedInterestExpense;
                        }
                        StateHasChanged();
                    }
                }
            }
        }

        public async Task HandleSaveChanges()
        {
            var newEI = new ExceptionInterest
            {
                Modelid = ModelId,
                ProductId = EIFormDetails.ProductId ?? 0,
                AccountNumber = EIFormDetails.AccountNumber,
                AverageCollectedBalance = EIFormDetails.AverageCollectedBalance,
                CampaignId = EIFormDetails.CampaignId,
                StandardInterestRate = EIFormDetails.StandardInterestRate,
                AnnualizedStandardIneterestExpense = EIFormDetails.AnnualizedStandardIneterestExpense,
                CurrentInteresetRate = EIFormDetails.CurrentInteresetRate,
                AnnualizedCurrentInteresetExpense = EIFormDetails.AnnualizedCurrentInteresetExpense,
                FloatingId = EIFormDetails.FloatingId,
                FloatingRate = EIFormDetails.FloatingRate,
                BonusId = EIFormDetails.BonusId,
                AppliedInterestExpires = EIFormDetails.AppliedInterestExpires,
                AppliedInterestRate = EIFormDetails.AppliedInterestRate,
                AnnualizedInterestExpense = EIFormDetails.AnnualizedInterestExpense,

                CreatedDate = DateOnly.FromDateTime(DateTime.Today),
                IsActive = true
            };

            var success = await ExceptionInterestService.AddEIToDb(newEI);
            if (success)
            {
                EIFormDetails = new();
                ExceptionInterestDetails = await ExceptionInterestService.GetExceptionInterestDetails(ModelId);
            }  
        }

        public async Task HandleEdits()
        {
            var success = await ExceptionInterestService.EditEIInDb(ModelId, ExceptionInterestDetails);
            if (success) {
            await JS.InvokeVoidAsync("bootstrapInterop.showToast", "saveEditstoast");
            }
        }

        public void HandleResetForm()
        {
            EIFormDetails = new();
        }

        public async Task ReloadComponent()
        {
            ExceptionInterestDetails = await ExceptionInterestService.GetExceptionInterestDetails(ModelId);  
            StateHasChanged();
        }
    }
}
