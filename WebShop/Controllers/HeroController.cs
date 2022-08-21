using WebShop.Services.Implementation;

namespace WebShop.Controllers;

public class HeroController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly IProductService productService;
    private readonly IMapper mapper;

    public HeroController(ApplicationDbContext db, IProductService productService, IMapper mapper)
    {
        this.db = db;
        this.productService = productService;
        this.mapper = mapper;
    }


    /// <summary>
    /// Hero
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var hero = await productService.GetHerosAsync();
        return View(hero);
    }

    /// <summary>
    /// Details Hero
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var hero = await productService.GetHeroAsync(id);
        return View(hero);
    }

    /// <summary>
    /// Change Publish Status
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<IActionResult> ChangePublishStatus(int id, bool status)
    {
        var hero = await db.Hero.FindAsync(id);
        if (hero == null) { return null; }
        hero.Publish = status;
        await db.SaveChangesAsync();
        TempData["success"] = "Hero successfully publish!";
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Create Hero
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HeroBinding model)
    {
        await productService.AddHeroAsync(model);
        TempData["success"] = "Hero created successfully!";
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Edit Hero
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var hero = await productService.GetHeroAsync(id);
        return View(hero);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(HeroBinding model)
    {
        await productService.UpdateHeroAsync(model);
        TempData["success"] = "Hero update successfully!";
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Delete Hero
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var hero = await productService.GetHeroAsync(id);
        return View(hero);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(HeroBinding model)
    {
        await productService.DeleteHeroAsync(model);
        TempData["success"] = "Hero deleted successfully!";
        return RedirectToAction("Index");
    }
}