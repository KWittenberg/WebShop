namespace WebShop.Controllers;

//[Authorize (Roles = Roles.Admin)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;

    public HomeController(ILogger<HomeController> logger, IProductService productService)
    {
        _logger = logger;
        this.productService = productService;
    }


    /// <summary>
    /// For Best Offer Carusel GetAvailableProductsAsync
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        var product = await this.productService.GetAvailableProductsAsync();
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