
namespace BlazorApp.Components.Pages.Admin
{
    public partial class Admin_Portal : ComponentBase
    {
        [Inject] private AuthDbContext Context { get;set; } = null!;

        private int ActiveTab { get;set;} 

        protected override async Task OnInitializedAsync()
        {
            var user = await Context.UserTabAssocs.Where(uta => uta.UserId == 1).FirstOrDefaultAsync();
            if (user != null)
            {
                ActiveTab = user.TabId;
            }
        }

        private async Task SavePreferenceHandler()
        {
            var user = await Context.UserTabAssocs.Where(uta => uta.UserId == 1).FirstOrDefaultAsync();
            if (user != null)
            {
                user.TabId = ActiveTab;
                await Context.SaveChangesAsync();
            }
        }

    }
}
