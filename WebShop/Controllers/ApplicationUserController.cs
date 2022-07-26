namespace WebShop.Controllers;

//[Authorize(Roles = Roles.Admin)]
public class ApplicationUserController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly ICustomerService customerService;
    private readonly IApplicationUserService userService;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;


    public ApplicationUserController(ApplicationDbContext db, ICustomerService customerService,
        IApplicationUserService userService, SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.db = db;
        this.customerService = customerService;
        this.userService = userService;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }


    public IActionResult Index()
    {
        return View();
    }


    /// <summary>
    /// Register
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserBinding model)
    {
        var result = await userService.CreateUserAsync(model, Roles.User);
        if (result != null)
        {
            await signInManager.SignInAsync(result, true);
            TempData["success"] = "User Register successfully";
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    
    /// <summary>
    /// Login
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserBinding model)
    {
        if (ModelState.IsValid)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (result.Succeeded)
            {
                TempData["success"] = "User Login successfully";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Invalid Login Attempt !!!";
                return View(model);
            }
        }
        return View();
    }
}