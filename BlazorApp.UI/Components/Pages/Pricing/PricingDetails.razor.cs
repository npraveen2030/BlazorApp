
using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlazorApp.UI.Components.Pages.Pricing
{
    public partial class PricingDetails : ComponentBase
    {
        [Inject] public IModelsService ModelsService { get; set; } = null!;
        [Inject] private IJSRuntime JS { get; set; } = null!;
        [Inject] private NavigationManager NavManager { get; set; } = null!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        private int ActiveTab { get; set; } 
        private List<TabDto> Tabs { get; set; } = null!;
        private int SelectedEIId {  get; set; }
        private string SelectedEIName { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            Tabs = await ModelsService.GetExceptionInterestDetails();
            ActiveTab = Tabs[0].TabId;
        }

        public void SavePreferenceHandler()
        {

        }

        public void ShowExceptionInterest(ModelDto model)
        {
            ActiveTab = 3;
            SelectedEIId = model.Modelid;
            SelectedEIName = model.Name;
        }
    }
}
