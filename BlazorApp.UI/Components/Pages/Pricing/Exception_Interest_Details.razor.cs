using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;

namespace BlazorApp.UI.Components.Pages.Pricing
{
    public partial class Exception_Interest_Details : ComponentBase
    {
        [Parameter] public ExceptionInterestDto Details { get;set;} = null!;
        [Parameter] public EventCallback ReloadComponent { get;set;}
        [Inject] public IExceptionInterestService ExceptionInterestService { get; set; } = null!;

        private async Task HandleDeleteEI()
        {
            var success = await ExceptionInterestService.SetEIStatus(Details.Id ?? 0);
            if (success) {
                await ReloadComponent.InvokeAsync();
            }
        }
    }
}
