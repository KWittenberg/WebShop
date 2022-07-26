namespace WebShop.Services.Interface;

public interface IApplicationUserService
{
    Task<ApplicationUserViewModel> GetUserAsync(string id);
    
    
    Task<ApplicationUser?> CreateUserAsync(UserBinding model, string role);
    

    Task<ApplicationUserViewModel?> CreateApiUserAsync(UserBinding model, string role);
    
}