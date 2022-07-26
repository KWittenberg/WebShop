namespace WebShop.Services.Implementation;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IMapper mapper;

    public ApplicationUserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.mapper = mapper;
    }


    /// <summary>
    /// CreateApiUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task<ApplicationUserViewModel?> CreateApiUserAsync(ApplicationUserBinding model, string role)
    {
        var result = await CreateUserAsync(model, role);
        if (result == null) { return null; }
        return mapper.Map<ApplicationUserViewModel>(result);
    }

    /// <summary>
    /// CreateUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApplicationUser?> CreateUserAsync(ApplicationUserBinding model, string role)
    {
        // First check if user exist
        var find = await userManager.FindByEmailAsync(model.Email);
        if (find != null) { return null; }
        var user = mapper.Map<ApplicationUser>(model);
        //var adress = mapper.Map<Adress>(model.UserAdress);
        //user.Adress = new List<Adress>() { adress };
        var createdUser = await userManager.CreateAsync(user, model.Password);
        if (createdUser.Succeeded)
        {
            // Add User In Role
            var userAddedToRole = await userManager.AddToRoleAsync(user, role);

            if (!userAddedToRole.Succeeded)
            {
                throw new Exception("User Not Added In Role!!!");
            }
        }
        return user;
    }




    public async Task<ApplicationUser?> GetUserAsync(ApplicationUserBinding model)
    {
        // First check if user exist
        var find = await userManager.FindByEmailAsync(model.Email);
        if (find != null) { return null; }
        var user = mapper.Map<ApplicationUser>(find);
        return user;
    }
}