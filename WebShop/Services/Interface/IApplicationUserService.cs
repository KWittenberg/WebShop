namespace WebShop.Services.Interface;

public interface IApplicationUserService
{
    Task<ApplicationUser?> GetUserAsync(ApplicationUserBinding model);
    Task<ApplicationUser?> CreateUserAsync(ApplicationUserBinding model, string role);
    Task<ApplicationUserViewModel?> CreateApiUserAsync(ApplicationUserBinding model, string role);
}