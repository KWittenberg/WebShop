namespace WebShop.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IApplicationUserService userService;
    private readonly IProductService productService;
    private readonly ICustomerService customerService;
    private readonly IEmailService emailService;
    private readonly IMapper mapper;

    public UserController(ApplicationDbContext db, ICustomerService customerService, IMapper mapper, IApplicationUserService userService, IProductService productService, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
    {
        this.db = db;
        this.productService = productService;
        this.customerService = customerService;
        this.mapper = mapper;
        this.userService = userService;
        this.signInManager = signInManager;
        this.emailService = emailService;
    }



    /// <summary>
    /// Registration
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Registration()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registration(UserBinding model)
    {
        if (ModelState.IsValid)
        {
            var result = await userService.RegistrationAsync(model, Roles.User);
            if (result != null)
            {
                await signInManager.SignInAsync(result, true);
                TempData["success"] = "User Register successfully";
                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            TempData["error"] = "Invalid Registration !!!";
            return View(model);
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
    public async Task<IActionResult> Login(LoginUserBinding model)
    {
        if (ModelState.IsValid)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (result.Succeeded)
            {
                var dbo = await db.ApplicationUser.FirstOrDefaultAsync(x=>x.Email == model.Email);
                var to = "kejo@net.hr";
                var subject = "Bolta WebShop - PRIJAVA";
                var body = dbo.FirstName + " " + dbo.LastName + " upravo ste se prijavili na Bolta WebShop !";
                this.emailService.SendEmail(to, subject, body);
                
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
    /// Index
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index(string id)
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
    /// Edit User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> EditUser(string Id)
    {
        var user = await userService.GetApplicationUserAsync(Id);
        return View(user);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(UserUpdateBinding model)
    {
        await userService.UpdateUserAsync(model);
        TempData["success"] = "User update successfully";
        return RedirectToAction("Index", "User", new { id = model.Id });
    }

    
    /// <summary>
    /// OLD GET: Address
    /// </summary>
    /// <returns></returns>
    //public async Task<IActionResult> Index(string Id)
    //{
    //    return this.db.Address != null ?
    //                View(await this.db.Address.Include(am => am.ApplicationUser)
    //                    .Where(x=>x.ApplicationUser.Id == Id)
    //                    .ToListAsync()) :
    //                Problem("Entity set 'ApplicationDbContext.Address'  is null.");
    //}

    /// <summary>
    /// GET: Address/Details/1
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || this.db.Address == null) { return NotFound(); }

        var address = await this.db.Address.FirstOrDefaultAsync(m => m.Id == id);

        if (address == null) { return NotFound(); }

        return View(address);
    }


    /// <summary>
    /// Address/Create
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddressBinding addressBinding)
    {
        await customerService.AddAddressAsync(addressBinding);
        TempData["success"] = "Address created successfully";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Address/Edit
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var address = await customerService.GetAddressInt(id);
        var model = mapper.Map<AddressUpdateBinding>(address);
        return View(address);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AddressUpdateBinding model)
    {
        await customerService.UpdateAddressAsync(model);
        TempData["success"] = "Address update successfully";
        return RedirectToAction(nameof(Index));
    }
    
    /// <summary>
    /// Address/Delete
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var address = await customerService.GetAddressInt(id);
        var model = mapper.Map<AddressUpdateBinding>(address);
        return View(address);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(AddressUpdateBinding model)
    {
        await customerService.DeleteAddressAsync(model);
        TempData["success"] = "Address deleted successfully";
        return RedirectToAction(nameof(Index));
    }


    /// <summary>
    /// Orders
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Orders(string id)
    {
         
        var orders = await productService.GetOrdersByUserAsync(id);
        return View(orders);
    }

    /// <summary>
    /// Order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Order(int id)
    {
        var order = await productService.GetOrderAsync(id);
        return View(order);
    }

    /// <summary>
    /// Change Primary Status
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<IActionResult> ChangePrimaryStatus(int id, bool status)
    {
        var address = await db.Address.FindAsync(id);
        if (address == null) { return null; }
        address.Primary = status;
        await db.SaveChangesAsync();
        //return RedirectToAction("Index");
        return RedirectToAction(nameof(Index));
        //return RedirectToAction("Index", User new { id = ApplicationUser.Id });
    }
}