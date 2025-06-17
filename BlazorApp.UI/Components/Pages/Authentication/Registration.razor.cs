using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Entities.AuthDB.Core;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.UI.Components.Pages.Authentication
{
    public partial class Registration : ComponentBase
    {
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public IRegisterService RegisterService { get; set; } = null!;
        [Parameter] public EventCallback<string> ChangeTab { get; set; }
        public ConfirmPasswordDto RegFormDetails { get; set; } = new();

        public EditContext _editContext { get; set; } = null!;
        public ValidationMessageStore _messageStore { get; set; } = null!;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(RegFormDetails);
            _messageStore = new ValidationMessageStore(_editContext);
        }
        public async Task HandleRegister()
        {
            try
            {
                var newUser = new UserDetail
                {
                    UserName = RegFormDetails.UserName,
                    Password = PasswordHelper.HashPassword(RegFormDetails.Password),
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    IsActive = true
                };

                await RegisterService.AddUserAsync(newUser);

                RegFormDetails = new();
                NavManager.NavigateTo("/",forceLoad:true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ChangeTabtoSignIn()
        {
            ChangeTab.InvokeAsync("SignIn");
        }
    }


}