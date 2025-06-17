using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Claims;
using BlazorApp.BLL.Utilities;
using BlazorApp.BLL.Interface;
using Microsoft.JSInterop;
using System.Data;

namespace BlazorApp.UI.Components.Pages.Authentication
{
    public partial class SignIn : ComponentBase
    {
        [Inject] public IJSRuntime JS { get; set; } = null!;
        [Inject] public ISigninService SigninService { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get;set;} = null!; 
        [Parameter] public EventCallback<string> ChangeTab { get;set;}
        
        public UserDetailDto SigninForm { get;set;} = new();

        public EditContext _editContext { get;set;} = null!;
        public ValidationMessageStore _messageStore { get; set; } = null!;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(SigninForm);
            _messageStore = new ValidationMessageStore(_editContext);
        }

        public async Task HandleSignin()
        {
            _messageStore.Clear();

            var user = await SigninService.GetUserByNameAsync(SigninForm.UserName);

            if (user == null || !PasswordHelper.VerifyPassword(SigninForm.Password, user.Password))
            {
                _messageStore.Add(() => SigninForm.Password, "Invalid Username or Password");
                _editContext.NotifyValidationStateChanged();
                return;
            }
            
            var UserAssociatedRoles = await SigninService.GetUserAssociatedRolesByIdAsync(user.UserId);
            await JS.InvokeVoidAsync("bootstrapInterop.login", user.UserId, UserAssociatedRoles);

            SigninForm = new();
            NavManager.NavigateTo("/associatedprojects",forceLoad:true);  
        }

        public void ChangeTabtoRegister()
        {
            ChangeTab.InvokeAsync("Register");
        }
    }
}
