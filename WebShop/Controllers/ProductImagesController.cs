using WebShop.Models.Dbo;

namespace WebShop.Controllers;

public class ProductImagesController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly IProductService productService;

    public ProductImagesController(ApplicationDbContext db, IProductService productService)
    {
        this.db = db;
        this.productService = productService;
    }

    /// <summary>
    /// Get ProductImages
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        var productImages = this.productService.GetProductImagesAsync();
        return View(await productImages);
    }

    /// <summary>
    /// Create ProductImages
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        return View(new ProductImagesBinding { ProductId = id });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductImagesBinding model)
    {
        await productService.AddProductImagesAsync(model);
        TempData["success"] = "Product Image created successfully";
        return RedirectToAction("ProductAdministration", "Admin");
    }
    
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (this.db.ProductImages == null) { return Problem("Entity set 'ApplicationDbContext.ProductImages' is null."); }
        var productImages = await this.db.ProductImages.FindAsync(id);
        if (productImages != null)
        {
            this.db.ProductImages.Remove(productImages);
            TempData["success"] = "Image deleted successfully!";
        }
        await this.db.SaveChangesAsync();
        //return RedirectToAction(nameof(Index));
        return RedirectToAction("ProductAdministration", "Admin");
    }


    //// GET: ProductImages
    //public async Task<IActionResult> Index()
    //{
    //    return _context.ProductImages != null ?
    //                View(await _context.ProductImages.ToListAsync()) :
    //                Problem("Entity set 'ApplicationDbContext.ProductImages'  is null.");
    //}






    // GET: ProductImages/Details/5
    //public async Task<IActionResult> Details(int? id)
    //{
    //    if (id == null || _context.ProductImages == null)
    //    {
    //        return NotFound();
    //    }

    //    var productImages = await _context.ProductImages
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (productImages == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(productImages);
    //}

    // GET: ProductImages/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: ProductImages/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("Id,Created,Name,ImageUrl")] ProductImages productImages)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(productImages);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(productImages);
    //}

    //// GET: ProductImages/Edit/5
    //public async Task<IActionResult> Edit(int? id)
    //{
    //    if (id == null || _context.ProductImages == null)
    //    {
    //        return NotFound();
    //    }

    //    var productImages = await _context.ProductImages.FindAsync(id);
    //    if (productImages == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(productImages);
    //}

    // POST: ProductImages/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("Id,Created,Name,ImageUrl")] ProductImages productImages)
    //{
    //    if (id != productImages.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(productImages);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ProductImagesExists(productImages.Id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(productImages);
    //}

    //// GET: ProductImages/Delete/5
    //public async Task<IActionResult> Delete(int? id)
    //{
    //    if (id == null || _context.ProductImages == null)
    //    {
    //        return NotFound();
    //    }

    //    var productImages = await _context.ProductImages
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (productImages == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(productImages);
    //}

    //// POST: ProductImages/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(int id)
    //{
    //    if (_context.ProductImages == null)
    //    {
    //        return Problem("Entity set 'ApplicationDbContext.ProductImages'  is null.");
    //    }
    //    var productImages = await _context.ProductImages.FindAsync(id);
    //    if (productImages != null)
    //    {
    //        _context.ProductImages.Remove(productImages);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool ProductImagesExists(int id)
    //{
    //    return (_context.ProductImages?.Any(e => e.Id == id)).GetValueOrDefault();
    //}
}