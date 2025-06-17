using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Service;
using BlazorApp.DLL.Interface;
using BlazorApp.DLL.Repository;

namespace BlazorApp.UI
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ISigninRepository, SigninRepository>();
            services.AddScoped<IRegisterRepository, RegisterRepository>();
            services.AddScoped<IForgotPasswordRepository, ForgotPasswordRepository>();
            services.AddScoped<IUserAssociatedProjectsRepository, UserAssociatedProjectsRepository>();
            services.AddScoped<IProjectLeadPortalRepository, ProjectLeadPortalRepository>();
            services.AddScoped<IProjectManagerPortalRepository, ProjectManagerPortalRepository>();
            services.AddScoped<IAdminPortalRepository, AdminPortalRepository>();
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<IExceptionInterestRepository, ExceptionInterestRepository>();
            services.AddScoped<IModelsRepository, ModelsRepository>();
            services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
            services.AddScoped<IUserProjectRoleAssociationRepository, UserProjectRoleAssociationRepository>();

            // Services
            services.AddScoped<ISigninService, SigninService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
            services.AddScoped<IUserAssociatedProjectsService, UserAssociatedProjectsService>();
            services.AddScoped<IProjectLeadPortalService, ProjectLeadPortalService>();
            services.AddScoped<IProjectManagerPortalService, ProjectMangerPortalService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IAdminPortalService, AdminPortalService>();
            services.AddScoped<IExceptionInterestService, ExceptionInterestService>();
            services.AddScoped<IModelsService, ModelsService>();
            services.AddScoped<IProjectManagerService, ProjectManagerService>();
            services.AddScoped<IUserProjectRoleAssociationService, UserProjectRoleAssociationService>();
        }
    }
}
