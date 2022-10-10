namespace WebShop.Controllers;

//[Authorize (Roles = Roles.Admin)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;
    private readonly IEmailService emailService;
    private readonly IBlogService blogService;

    public HomeController(ILogger<HomeController> logger, IProductService productService, IEmailService emailService, IBlogService blogService)
    {
        _logger = logger;
        this.productService = productService;
        this.emailService = emailService;
        this.blogService = blogService;
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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactBinding model)
    {
        if (ModelState.IsValid)
        {
            this.emailService.SendEmailMessage(model);
            TempData["success"] = "Message send successfully!";
        }
        else
        {
            TempData["error"] = "Message not send!";
        }
        return RedirectToAction("Contact");
    }
    
    /// <summary>
    /// About
    /// </summary>
    /// <returns></returns>
    public IActionResult About()
    {
        return View();
    }


    /// <summary>
    /// Blog
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Blog()
    {
        var blogs = await blogService.GetBlogsAsync();
        return View(blogs);
    }

    /// <summary>
    /// Get Post
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Post(int Id)
    {
        var post = await blogService.GetPostAsync(Id);
        return View(post);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}