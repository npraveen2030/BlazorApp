using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Components.Pages.Authentication
{
    public partial class SignIn : ComponentBase
    {
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        [Inject] private AuthDbContext Context { get; set; } = null!;
        [Inject] private IHttpContextAccessor HttpContextAccessor { get;set; } = null!;
        [SupplyParameterFromForm] public UserDetailDto SigninForm { get;set;} = new();

        private EditContext _editContext { get;set;} = null!;
        private ValidationMessageStore _messageStore { get; set; } = null!;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(SigninForm);
            _messageStore = new ValidationMessageStore(_editContext);
        }

        internal async Task HandleSignin()
        {
            _messageStore.Clear();

            var user = await Context.UserDetails.FirstOrDefaultAsync(u => u.UserName == SigninForm.UserName);

            if (user == null || !PasswordHelper.VerifyPassword(SigninForm.Password, user.Password))
            {
                _messageStore.Add(() => SigninForm.Password, "Invalid Username or Password");
                _editContext.NotifyValidationStateChanged();
                return;
            }
            else
            {
                var UserAssociatedRoles = await Context.UserProjectRoleAssociations
                                          .Where(assoc => assoc.UserId == user.UserId && assoc.IsActive)
                                          .Include(assoc => assoc.Role)
                                          .OrderBy(assoc => assoc.Role.RolePriority)
                                          .Select(assoc => assoc.RoleId)
                                          .Distinct()
                                          .ToListAsync();

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserId.ToString())
                    };

                foreach (var role in UserAssociatedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                if (HttpContextAccessor.HttpContext != null)
                {
                    await HttpContextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        new AuthenticationProperties { IsPersistent = false }
                    );
                }

                SigninForm = new();
                Navigation.NavigateTo("/associatedprojects", forceLoad: true);
            }
        }
    }
}
