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
    /// GetRolesAsync
    /// </summary>
    /// <returns></returns>
    public async Task<List<ApplicationUserViewModel>> GetRolesAsync()
    {
        var dbo = await db.Roles.ToListAsync();
        return dbo.Select(x => mapper.Map<ApplicationUserViewModel>(x)).ToList();
    }
    
    /// <summary>
    /// GetRoleAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApplicationUserViewModel> GetRoleAsync(string id)
    {
        var role = await db.Roles.FindAsync(id);
        return mapper.Map<ApplicationUserViewModel>(role);
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
    /// CreateApplicationUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApplicationUser?> CreateApplicationUserAsync(ApplicationUserBinding model)
    {
        var find = await userManager.FindByEmailAsync(model.Email);
        if (find != null) { return null; }
        
        var user = new ApplicationUser
        {
            RoleId = model.RoleId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            UserName = model.Email,
            DOB = model.DOB
            };

        var createdUser = await userManager.CreateAsync(user, model.Password);
        if (createdUser.Succeeded)
        {
            var role = await this.roleManager.FindByIdAsync(model.RoleId);
            var userAddedToRole = await userManager.AddToRoleAsync(user, role.Name);
            if (!userAddedToRole.Succeeded)
            {
                throw new Exception("User Not Added In Role!!!");
            }
        }
        return user;
    }


    /// <summary>
    /// UpdateApplicationUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ApplicationUserViewModel> UpdateApplicationUserAsync(ApplicationUserUpdateBinding model)
    {
        var objFromDb = await this.userManager.FindByIdAsync(model.Id);
        if (objFromDb == null) { return null; }

        var userRole = await this.db.UserRoles.FirstOrDefaultAsync(u => u.UserId == objFromDb.Id);
        if (userRole != null)
        {
            var previousRoleName = this.db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
            // Removing the old role
            await this.userManager.RemoveFromRoleAsync(objFromDb, previousRoleName);
        }

        //Add new role
        await this.userManager.AddToRoleAsync(objFromDb, this.db.Roles.FirstOrDefault(u => u.Id == model.RoleId).Name);
        objFromDb.Email = model.Email;
        
        // Change password
        var user = await userManager.FindByIdAsync(model.Id);
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        await userManager.ResetPasswordAsync(user, token, model.Password);
        
        var dbo = await db.ApplicationUser.FindAsync(model.Id);
        mapper.Map(model, dbo);
        await this.db.SaveChangesAsync();
        return mapper.Map<ApplicationUserViewModel>(dbo);
    }




    /// <summary>
    /// UpdateUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ApplicationUserViewModel> UpdateUserAsync(UserUpdateBinding model)
    {
        var objFromDb = await this.userManager.FindByIdAsync(model.Id);
        if (objFromDb == null) { return null; }
        
        var dbo = await db.ApplicationUser.FindAsync(model.Id);
        mapper.Map(model, dbo);
        await this.db.SaveChangesAsync();
        return mapper.Map<ApplicationUserViewModel>(dbo);
    }






    /// <summary>
    /// DeleteApplicationUserAsync
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ApplicationUserViewModel> DeleteApplicationUserAsync(ApplicationUserUpdateBinding model)
    {
        var roles = await db.Roles.FirstOrDefaultAsync(x => x.Id == model.RoleId);
        var dbo = await db.ApplicationUser.FindAsync(model.Id);
        mapper.Map(model, dbo);
        //dbo.Roles = roles;
        this.db.ApplicationUser.Remove(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ApplicationUserViewModel>(dbo);
    }











    /// <summary>
    /// Create User Async only Email and Password
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