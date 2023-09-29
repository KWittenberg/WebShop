namespace WebShop.Controllers.API;

[Route("api/[controller]")]
[ApiController]
[EnableCors(CorsPolicy.AllowAll)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
public class AdminApiController : ControllerBase
{
    private readonly IProductService productService;
    private readonly IMapper mapper;

    public AdminApiController(IProductService productService, IMapper mapper)
    {
        this.mapper = mapper;
        this.productService = productService;
    }

    /// <summary>
    /// ProductCategorys
    /// </summary>
    /// <returns></returns>
    [Route("product-categorys")]
    [ProducesResponseType(typeof(List<ProductCategoryViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductCategorysAsync()
    {
        return Ok(await productService.GetProductCategorysAsync());
    }
    
    /// <summary>
    /// Product
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("product/{id}")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductAsync(int id)
    {
        return Ok(await productService.GetProductAsync(id));
    }
    
    [HttpPost]
    [Route("product")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddProductAsync(ProductBinding model)
    {
        return Ok(await productService.AddProductAsync(model));
    }
    
    [HttpPut]
    [Route("product")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProductAsync(ProductUpdateBinding model)
    {
        return Ok(await productService.UpdateProductAsync(model));
    }
    
    [HttpDelete]
    [Route("product")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProductAsync(ProductUpdateBinding model)
    {
        return Ok(await productService.DeleteProductAsync(model));
    }
}