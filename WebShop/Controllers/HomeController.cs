namespace WebShop.Controllers;

//[Authorize (Roles = Roles.Admin)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;
    private readonly UserManager<ApplicationUser> userManager;

    public HomeController(ILogger<HomeController> logger, IProductService productService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        this.productService = productService;
        this.userManager = userManager;
    }
    
    public IActionResult Index()
    {
        return View(productService.GetProductsAsync().Result);
    }


    //[Authorize]
    //public async Task<IActionResult> ItemView(int id)
    //{
    //    var product = await productService.GetProductAsync(id);

    //    return View(product);
    //}

    //[Authorize]
    //[HttpPost]
    //public async Task<IActionResult> ItemView(ShoppingCartBinding model)
    //{
    //    model.UserId = userManager.GetUserId(User);
    //    var product = await productService.AddShoppingCartAsync(model);

    //    return RedirectToAction("Index");
    //}

    //[Authorize]
    //public async Task<IActionResult> ShoppingCart()
    //{
    //    var shoppingCart = await productService.GetShoppingCartAsync(userManager.GetUserId(User));
    //    return View(shoppingCart);
    //}


    public IActionResult Contact()
    {
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}