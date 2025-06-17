using Microsoft.JSInterop;

namespace BlazorApp.UI.Components.Pages.Common
{
    public partial class ThemeToggler:ComponentBase
    {
        [Inject] private IJSRuntime JS { get; set; } = null!;
        private bool isDarkTheme = false;

        private void ToggleTheme()
        {
            isDarkTheme = !isDarkTheme;
            var theme = isDarkTheme ? "dark" : "light";
            JS.InvokeVoidAsync("bootstrapInterop.setTheme", theme);
        }

        
    }
}
