using WebShop.Utils;

namespace WebShop.Controllers;

[Authorize(Roles = Roles.Admin)] //[Area("Admin")]
public class ApplicationUserController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly ICustomerService customerService;
    private readonly IApplicationUserService userService;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IMapper mapper;

    public ApplicationUserController(ApplicationDbContext db, ICustomerService customerService,
        IApplicationUserService userService, SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        this.db = db;
        this.customerService = customerService;
        this.userService = userService;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.mapper = mapper;
    }


    /// <summary>
    /// Index
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //var userList = this.db.ApplicationUser.ToList();
        //var userRole = this.db.UserRoles.ToList();
        //var roles = this.db.Roles.ToList();
        //var users = this.customerService.GetApplicationUsers();

        //foreach (var user in userList)
        //{
        //    var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
        //    if (role == null) { user.Role = "None"; }
        //    else { user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name; }
        //}
        //return View(users.Result);

        var applicationUsers = await userService.GetApplicationUsersAsync();
        return View(applicationUsers);
    }

    /// <summary>
    /// Details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var userList = this.db.ApplicationUser.ToList();
        var userRole = this.db.UserRoles.ToList();
        var roles = this.db.Roles.ToList();

        foreach (var user in userList)
        {
            var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
            if (role == null) { user.Role = "None"; }
            else { user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name; }
        }
        var dboUsers = await userService.GetUserAsync(id);
        return View(dboUsers);
    }


    /// <summary>
    /// Create
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ApplicationUserBinding model)
    {
        var result = await userService.CreateApplicationUserAsync(model);
        if (result != null)
        {
            TempData["success"] = "ApplicationUser Register successfully";
            return RedirectToAction("Index", "ApplicationUser");
        }
        return View();
    }

    /// <summary>
    /// Edit
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await userService.GetApplicationUserAsync(id);
        return View(user);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ApplicationUserUpdateBinding model)
    {
        await userService.UpdateApplicationUserAsync(model);
        TempData["success"] = "ApplicationUser update successfully";
        return RedirectToAction("Index", "ApplicationUser");
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await userService.GetApplicationUserAsync(id);
        return View(user);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(ApplicationUserUpdateBinding model)
    {
        await userService.DeleteApplicationUserAsync(model);
        TempData["success"] = "ApplicationUser Deleted successfully";
        return RedirectToAction("Index", "ApplicationUser");
    }



    /// <summary>
    /// LockUnlock
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult LockUnlock(string userId)
    {
        var objFromDb = this.db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
        if (objFromDb == null) { return NotFound(); }
        if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
        {
            //user is locked and will remain locked untill lockoutend time
            //clicking on this action will unlock them
            objFromDb.LockoutEnd = DateTime.Now;
            TempData["success"] = "User unlocked successfully.";
        }
        else
        {
            //user is not locked, and we want to lock the user
            objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            TempData["success"] = "User locked successfully.";
        }
        this.db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    /// <summary>
    /// Change Primary Status
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<IActionResult> ChangePrimaryStatus(int id, bool status, string userid)
    {
        var address = await db.Address.FindAsync(id);
        if (address == null) { return null; }
        address.Primary = status;
        await db.SaveChangesAsync();
        //return RedirectToAction("Index");
        return RedirectToAction("Index", "ApplicationUser");
    }






    /// <summary>
    /// Register
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [AllowAnonymous]
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
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [AllowAnonymous]
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



    /// <summary>
    /// Edit User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> EditUser(string Id)
    {
        var user = await userService.GetUserAsync(Id);
        var model = mapper.Map<ApplicationUserUpdateBinding>(user);
        return View(user);
    }
    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(UserUpdateBinding model)
    {
        await userService.UpdateUserAsync(model);
        TempData["success"] = "User update successfully";
        return RedirectToAction("Index", "ApplicationUser");
    }
}