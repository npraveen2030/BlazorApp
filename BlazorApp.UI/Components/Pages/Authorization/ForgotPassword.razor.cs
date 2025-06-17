using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Common;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.UI.Components.Pages.Authorization
{
    public partial class ForgotPassword:ComponentBase
    {
        [Inject] private IForgotPasswordService ForgotPasswordService { get; set; } = null!;
        [Inject] private IJSRuntime JS { get; set; } = null!;

        private ForgotPasswordEmail emailModel = new();

        private async Task HandleForgotPassword()
        {
            var EmailSent = await ForgotPasswordService.AddTokenByName(emailModel.UserName, emailModel.Email);
            if (EmailSent)
            {
                await JS.InvokeVoidAsync("bootstrapInterop.showToast", "sentemailtoast");
            }
        }
    }

    
}
