namespace BlazorApp.Components.Layout
{
    public partial class AuthenticationLayout: LayoutComponentBase
    {
        [Inject] private NavigationManager Navigation { get; set; } = null!;

        private string activeTab = "";

        protected override void OnInitialized()
        {
            activeTab = Navigation.ToBaseRelativePath(Navigation.Uri);
        }

    }
}
