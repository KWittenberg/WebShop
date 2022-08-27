namespace WebShop.Controllers;

[Authorize(Roles = Roles.Admin)]
public class AdminController : Controller
{
    private readonly IProductService productService;
    private readonly IMapper mapper;
    public AdminController(IProductService productService, IMapper mapper)
    {
        this.productService = productService;
        this.mapper = mapper;
    }


    /// <summary>
    /// Dashboard
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        return View();
    }


    /// <summary>
    /// ProductAdministration
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ProductAdministration()
    {
        var products = await productService.GetProductsAsync();
        return View(products);
    }

    /// <summary>
    /// Details Product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> DetailsProduct(int id)
    {
        var product = await productService.GetProductAsync(id);

        return View(product);
    }



    /// <summary>
    /// Create Product
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductBinding model)
    {
        await productService.AddProductAsync(model);
        TempData["success"] = "Product created successfully";
        return RedirectToAction("ProductAdministration");
    }

    /// <summary>
    /// Edit Product
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await productService.GetProductAsync(id);
        var model = mapper.Map<ProductUpdateBinding>(product);
        return View(product);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductUpdateBinding model)
    {
        await productService.UpdateProductAsync(model);
        TempData["success"] = "Product update successfully";
        return RedirectToAction("ProductAdministration");
    }
    
    /// <summary>
    /// Delete Product
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await productService.GetProductAsync(id);
        var model = mapper.Map<ProductUpdateBinding>(product);
        return View(product);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(ProductUpdateBinding model)
    {
        await productService.DeleteProductAsync(model);
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("ProductAdministration");
    }

    

    /// <summary>
    /// AddProductCategory
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> AddProductCategory()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddProductCategory(ProductCategoryBinding model)
    {
        await productService.AddProductCategoryAsync(model);
        TempData["success"] = "Product created successfully";
        return RedirectToAction("ProductAdministration");
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
    /// Orders
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Orders()
    {
        var orders = await productService.GetOrdersAsync();
        return View(orders);
    }

    /// <summary>
    /// SuspendOrder
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> SuspendOrder(int id)
    {
        var order = await productService.SuspendOrder(id);
        return RedirectToAction("Orders");
    }




    /// <summary>
    /// Change Available Status
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<IActionResult> ChangeAvailableStatus(int id, bool status)
    {
        var discount = await productService.ChangeAvailableStatus(id, status);
        return RedirectToAction("ProductAdministration");
    }
    
    /// <summary>
    /// Change Discount Status
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<IActionResult> ChangeDiscountStatus(int id, bool status)
    {
        var discount = await productService.ChangeDiscountStatus(id, status);
        return RedirectToAction("ProductAdministration");
    }
}