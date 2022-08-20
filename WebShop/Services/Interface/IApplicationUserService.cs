namespace WebShop.Services.Interface;

public interface IApplicationUserService
{
    Task<List<ApplicationUserViewModel>> GetApplicationUsersAsync();
    Task<ApplicationUserViewModel> GetApplicationUserAsync(string id);
    Task<string> GetApplicationUserRoleAsync(string id);
    Task<List<ApplicationUserRoleViewModel>> GetApplicationUserRolesAsync();


    Task<List<ApplicationUserViewModel>> GetRolesAsync();
    Task<ApplicationUserViewModel> GetRoleAsync(string id);

    
    Task<ApplicationUser?> CreateApplicationUserAsync(ApplicationUserBinding model);
    Task<ApplicationUserViewModel> UpdateApplicationUserAsync(ApplicationUserUpdateBinding model);
    Task<ApplicationUserViewModel> DeleteApplicationUserAsync(ApplicationUserUpdateBinding model);


    Task<ApplicationUser?> RegistrationAsync(UserBinding model, string role);
    Task<ApplicationUser?> CreateUserAsync(UserBinding model, string role);
    Task<ApplicationUserViewModel> UpdateUserAsync(UserUpdateBinding model);


    Task<ApplicationUserViewModel> GetUserAsync(string id);
    Task<ApplicationUserViewModel?> CreateApiUserAsync(UserBinding model, string role);




    Task<string> GetToken(TokenLoginBinding model);
}