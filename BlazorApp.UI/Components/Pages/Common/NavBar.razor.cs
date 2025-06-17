namespace BlazorApp.UI.Components.Pages.Common
{
    public partial class NavBar:ComponentBase
    {
        [Parameter] public string Title { get; set; } = "";
        [Parameter] public string UserName { get; set; } = "";
        [Parameter] public List<string> UserRoles { get; set; } = [];
        [Parameter] public string UserProject { get; set; } = "";
    }
}
