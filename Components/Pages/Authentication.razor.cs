using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Pages
{
    public partial class Authentication : ComponentBase
    {
        public string activeTab = "SignIn";

        public void SetActiveTab(string tab)
        {
            activeTab = tab;
        }
    }

}