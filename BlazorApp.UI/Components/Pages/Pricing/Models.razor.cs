
using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.CustomModels.Pricing;

namespace BlazorApp.UI.Components.Pages.Pricing
{
    public partial class Models : ComponentBase
    {
        [Parameter] public EventCallback<ModelDto> HandleNameClick { get;set; }
        [Inject] public IModelsService ModelsService { get; set; } = null!;

        private List<int> pageSizeOptions = new() { 5, 8, 10 };
        private List<ModelDto> PricingModels { get;set;} = null!;

        public Dictionary<string, GridMeta> GridState { get; set; } = new()
            {
                { "Modelid",      new GridMeta { Width="120px",   OrderIndex=0   ,Visible=true   } },
                { "Name",         new GridMeta { Width = "120px", OrderIndex = 1, Visible = true } },
                { "CreatedDate",   new GridMeta { Width = "120px", OrderIndex = 2, Visible = true } },
                { "CreatedBy",     new GridMeta { Width = "120px", OrderIndex = 3, Visible = true } },
                { "ModifiedDate",  new GridMeta { Width = "120px", OrderIndex = 4, Visible = true } },
                { "ModifiedBy",    new GridMeta { Width = "120px", OrderIndex = 5, Visible = true } },
                { "IsActive",      new GridMeta { Width = "120px", OrderIndex = 6, Visible = true } }
            };

        public GridPreferencesSaver<ModelDto> gridPreferencesSaver { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            gridPreferencesSaver = new GridPreferencesSaver<ModelDto>(GridState);
            PricingModels = await ModelsService.GetAllPricingModels();
        }

        private void SavePreferencesHandler()
        {

        }

        private void SelectModel(ModelDto model)
        {
            HandleNameClick.InvokeAsync(model);
        }
    }
}
