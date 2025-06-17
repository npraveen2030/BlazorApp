namespace BlazorApp.UI.Components.Pages.Authentication
{
    public partial class AuthenticationPage:ComponentBase
    {
        public string activeTab = "SignIn";

        public void SetActiveTab(string tab)
        {
            activeTab = tab;
        }
    }
}
