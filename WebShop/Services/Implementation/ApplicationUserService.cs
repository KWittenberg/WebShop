namespace WebShop.Services.Implementation;

public class ApplicationUserService : IApplicationUserService
{
    private readonly ApplicationDbContext db;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private SignInManager<ApplicationUser> signInManager;
    private readonly AppConfig appSettings;
    private readonly IMapper mapper;

    public ApplicationUserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager, IOptions<AppConfig> appSettings, IMapper mapper)
    {
        this.db = db;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.signInManager = signInManager;
        this.appSettings = appSettings.Value;
        this.mapper = mapper;
    }


    /// <summary>
    /// GetUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    //public async Task<ApplicationUser?> GetUserAsync(ApplicationUserBinding model)
    //{
    //    // First check if user exist
    //    var find = await userManager.FindByEmailAsync(model.Email);
    //    if (find != null) { return null; }
    //    var user = mapper.Map<ApplicationUser>(find);
    //    return user;
    //}



    /// <summary>
    /// GetUserAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApplicationUserViewModel> GetUserAsync(string id)
    {
        var dboUser = await db.ApplicationUser.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        return mapper.Map<ApplicationUserViewModel>(dboUser);
    }


    //public async Task<ApplicationUserViewModel> GetUser(ClaimsPrincipal user)
    //{
    //    var userId = userManager.GetUserId(user);
    //    return await GetUser(userId);

    //}



    /// <summary>
    /// CreateUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApplicationUser?> CreateUserAsync(UserBinding model, string role)
    {
        var find = await userManager.FindByEmailAsync(model.Email);
        if (find != null) { return null; }

        var user = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Email,
        };

        var createdUser = await userManager.CreateAsync(user, model.Password);
        if (createdUser.Succeeded)
        {
            var userAddedToRole = await userManager.AddToRoleAsync(user, role);
            if (!userAddedToRole.Succeeded)
            {
                throw new Exception("User Not Added In Role!!!");
            }
        }
        return user;
    }

    /// <summary>
    /// CreateApiUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task<ApplicationUserViewModel?> CreateApiUserAsync(UserBinding model, string role)
    {
        var result = await CreateUserAsync(model, role);
        if (result == null) { return null; }
        return mapper.Map<ApplicationUserViewModel>(result);
    }
}