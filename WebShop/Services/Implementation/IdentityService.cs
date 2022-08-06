namespace WebShop.Services.Implementation;

public class IdentityService : IIdentityService
{
    private RoleManager<IdentityRole> roleManager;
    private UserManager<ApplicationUser> userManager;

    public IdentityService(IServiceScopeFactory scopeFactory)
    {
        using (var scope = scopeFactory.CreateScope())
        {
            userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            CreateRoleAsync(Roles.Admin).Wait();
            CreateRoleAsync(Roles.User).Wait();
            CreateRoleAsync(Roles.Employee).Wait();

            CreateUserAsync(new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "098111222",
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                DOB = new DateTime(2000, 10, 10),
                Address = new List<Address>
                    {
                        new Address
                        {
                            Primary = true,
                            Street = "J.J. Strossmayera 1.",
                            City = "Požega",
                            PostCode = "34000",
                            Country = "Croatia"
                        }
                    }
            }, "admin@gmail.com", Roles.Admin).Wait();
            CreateUserAsync(new ApplicationUser
            {
                FirstName = "User",
                LastName = "User",
                PhoneNumber = "098555777",
                Email = "user@gmail.com",
                UserName = "user@gmail.com",
                DOB = new DateTime(2012, 09, 04),
                Address = new List<Address>
                    {
                        new Address
                        {
                            Primary = true,
                            Street = "Miramarska 8.",
                            City = "Zagreb",
                            PostCode = "10000",
                            Country = "Croatia"
                        }
                    }
            }, "user@gmail.com", Roles.User).Wait();
        }
    }
    

    /// <summary>
    /// Create Role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task CreateRoleAsync(string role)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole { Name = role });
        }
    }

    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task CreateUserAsync(ApplicationUser user, string password, string role)
    {
        // First check if user exist
        var find = await userManager.FindByEmailAsync(user.Email);
        if (find != null) { return; }

        // Disable Email Confirmation
        user.EmailConfirmed = true;

        // Add New User
        var createdUser = await userManager.CreateAsync(user, password);

        // Check If the User has been Successfully Added
        if (createdUser.Succeeded)
        {
            // Add User In Role
            var userAddedToRole = await userManager.AddToRoleAsync(user, role);

            if (!userAddedToRole.Succeeded)
            {
                throw new Exception("User Not Added In Role!!!");
            }
        }
    }
}