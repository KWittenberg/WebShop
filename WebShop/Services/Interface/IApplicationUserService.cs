namespace WebShop.Services.Interface;

public interface IApplicationUserService
{
    Task<List<ApplicationUserViewModel>> GetRolesAsync();
    Task<ApplicationUserViewModel> GetRoleAsync(string id);

    
    Task<ApplicationUser?> CreateApplicationUserAsync(ApplicationUserBinding model);
    Task<ApplicationUserViewModel> UpdateApplicationUserAsync(ApplicationUserUpdateBinding model);
    Task<ApplicationUserViewModel> DeleteApplicationUserAsync(ApplicationUserUpdateBinding model);



    Task<ApplicationUser?> CreateUserAsync(UserBinding model, string role);
    Task<ApplicationUserViewModel> UpdateUserAsync(UserUpdateBinding model);


    Task<ApplicationUserViewModel> GetUserAsync(string id);
    Task<ApplicationUserViewModel?> CreateApiUserAsync(UserBinding model, string role);
}