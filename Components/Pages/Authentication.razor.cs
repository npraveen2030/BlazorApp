using Microsoft.AspNetCore.Components;

namespace BlazorApp5.Components
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