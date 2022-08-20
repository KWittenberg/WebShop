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
    
    public async Task<IActionResult> Index()
    {
        var product = await this.productService.GetProductsAsync();
        var filteredResult = product.Where(n => n.ProductCategoryId.Equals(1)).ToList();
        return View("Index", filteredResult);
        //return View(productService.GetProductsAsync().Result);
    }

    /// <summary>
    /// Contact
    /// </summary>
    /// <returns></returns>
    public IActionResult Contact()
    {
        return View();
    }

    /// <summary>
    /// About
    /// </summary>
    /// <returns></returns>
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