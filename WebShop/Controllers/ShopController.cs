namespace WebShop.Controllers;

public class ShopController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;
    private readonly UserManager<ApplicationUser> userManager;

    public ShopController(ILogger<HomeController> logger, IProductService productService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        this.productService = productService;
        this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(productService.GetProductsAsync().Result);
    }

    /// <summary>
    /// ListView
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult ListView()
    {
        return View(productService.GetProductsAsync().Result);
    }

    /// <summary>
    /// ViewByCategory
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ViewByCategory(int Id)
    {
        var product = await this.productService.GetProductsAsync();
        var filteredResult = product.Where(n => n.ProductCategoryId.Equals(2)).ToList();
        return View("Index", filteredResult);
    }

    /// <summary>
    /// ItemView
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ItemView(int id)
    {
        var product = await productService.GetProductAsync(id);
        return View(product);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ItemView(ShoppingCartBinding model)
    {
        model.UserId = userManager.GetUserId(User);
        var product = await productService.AddShoppingCartAsync(model);
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Get: Search Filter
    /// </summary>
    /// <param name="searchString"></param>
    /// <returns></returns>
    public async Task<IActionResult> Filter(string searchString)
    {
        var product = await this.productService.GetProductsAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            var filteredResult = product.Where(n => n.Title.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();
            return View("Index", filteredResult);
        }
        return View("Index", product);
    }






    /// <summary>
    /// ShoppingCart
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ShoppingCart()
    {
        var shoppingCart = await productService.GetShoppingCartAsync(userManager.GetUserId(User));
        return View(shoppingCart);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ShoppingCart(OrderBinding model)
    {
        var order = await productService.AddOrder(model);
        return RedirectToAction("Index");
    }


    /// <summary>
    /// SuspendShoppingCartItem
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> SuspendShoppingCartItem(int id)
    {
        await productService.SuspendShoppingCartItem(id);
        return RedirectToAction("ShoppingCart");
    }

    /// <summary>
    /// SuspendShoppingCart
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> SuspendShoppingCart(int id)
    {
        var shoppingCart = await productService.SuspendShoppingCart(id);
        return RedirectToAction("ShoppingCart");
    }
}