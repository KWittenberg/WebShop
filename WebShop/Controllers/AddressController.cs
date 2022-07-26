namespace WebShop.Controllers;

[Authorize(Roles = Roles.Admin)]
public class AddressController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly ICustomerService customerService;
    private readonly IMapper mapper;

    public AddressController(ApplicationDbContext db, ICustomerService customerService, IMapper mapper)
    {
        this.db = db;
        this.customerService = customerService;
        this.mapper = mapper;
    }

    /// <summary>
    /// GET: Address
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        return this.db.Address != null ?
                    View(await this.db.Address.Include(am => am.ApplicationUser).ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.Address'  is null.");
    }

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
}