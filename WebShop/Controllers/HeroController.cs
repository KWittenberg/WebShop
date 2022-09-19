﻿namespace WebShop.Controllers;

public class HeroController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly IProductService productService;

    public HeroController(ApplicationDbContext db, IProductService productService)
    {
        this.db = db;
        this.productService = productService;
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
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (this.db.Hero == null) { return Problem("Entity set 'ApplicationDbContext.Hero' is null."); }
        var hero = await this.db.Hero.FindAsync(id);
        if (hero != null)
        {
            this.db.Hero.Remove(hero);
            TempData["success"] = "Hero deleted successfully!";
        }
        await this.db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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
}