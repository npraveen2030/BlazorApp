using BlazorApp.BLL.Interface;
using BlazorApp.Model.Common;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorApp.UI.Components.Pages.Authorization
{
    public partial class ResetPassword:ComponentBase
    {
        [Parameter] public string? Token { get; set; }
        [Inject] private NavigationManager NavManager { get; set; } = null!;
        [Inject] private IForgotPasswordService ForgotPasswordService { get; set; } = null!;
        private ResetModel resetModel = new();
        private string? token;

        protected override void OnInitialized()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var t))
            {
                token = t;
            }
        }

        private async Task HandleResetPassword()
        {
            var success = await ForgotPasswordService.ChangePasswordByToken(token, resetModel.NewPassword);
            if (success)
            {
                NavManager.NavigateTo("/", forceLoad: true);
            }
        }
    }
}
