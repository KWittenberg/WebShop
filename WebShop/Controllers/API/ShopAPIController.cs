namespace WebShop.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ShopAPIController : ControllerBase
{
    private readonly IApplicationUserService userSevice;
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;
    private readonly UserManager<ApplicationUser> userManager;
    
    public ShopAPIController(ILogger<HomeController> logger, IProductService productService, UserManager<ApplicationUser> userManager, IApplicationUserService userService)
    {
        this.userSevice = userService;
        this.productService = productService;
        this.userManager = userManager;
        _logger = logger;
    }

    /// <summary>
    /// Products
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> Products()
    {
        return Ok(await productService.GetProductsAsync());
    }



    //[ProducesResponseType(typeof(ApplicationUserViewModel), StatusCodes.Status200OK)]
    //[HttpGet]
    //[Route("user-info")]
    //public async Task<IActionResult> GetUser()
    //{
    //    return Ok(await userSevice.GetUser(User));
    //}
}