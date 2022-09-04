using WebShop.Services.Implementation;

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

    // GET: ProductImages
    public async Task<IActionResult> Index()
    {
        //var productImages = this.db.ProductImages.Include(x=>x.Product);
        var productImages = this.db.ProductImages;
        return View(await productImages.ToListAsync());
    }


    /// <summary>
    /// Create Product Images
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        //return View(new ProductImagesBinding { ProductId = id });
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductImagesBinding model)
    {
        await productService.AddProductImagesAsync(model);
        TempData["success"] = "Product Image created successfully";
        return RedirectToAction(nameof(Index));
    }










    //// GET: ProductImages/Details/5
    //public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null || this.db.ProductImages == null)
    //        {
    //            return NotFound();
    //        }

    //        var productImages = await this.db.ProductImages
    //            .Include(p => p.Product)
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (productImages == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(productImages);
    //    }

    ////GET: ProductImages/Create
    //public IActionResult Create()
    //{
    //    ViewData["ProductId"] = new SelectList(this.db.Product, "Id", "Title");
    //    return View();
    //}

    //// POST: ProductImages/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("Id,Created,ProductId,Name,ImageUrl")] ProductImages productImages)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        this.db.Add(productImages);
    //        await this.db.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    ViewData["ProductId"] = new SelectList(this.db.Product, "Id", "Title", productImages.ProductId);
    //    return View(productImages);
    //}
















    //// GET: ProductImages/Edit/5
    //public async Task<IActionResult> Edit(int? id)
    //{
    //    if (id == null || this.db.ProductImages == null)
    //    {
    //        return NotFound();
    //    }

    //    var productImages = await this.db.ProductImages.FindAsync(id);
    //    if (productImages == null)
    //    {
    //        return NotFound();
    //    }
    //    ViewData["ProductId"] = new SelectList(this.db.Product, "Id", "Title", productImages.ProductId);
    //    return View(productImages);
    //}

    //// POST: ProductImages/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("Id,Created,ProductId,Name,ImageUrl")] ProductImages productImages)
    //{
    //    if (id != productImages.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            this.db.Update(productImages);
    //            await this.db.SaveChangesAsync();
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
    //    ViewData["ProductId"] = new SelectList(this.db.Product, "Id", "Title", productImages.ProductId);
    //    return View(productImages);
    //}

    //// GET: ProductImages/Delete/5
    //public async Task<IActionResult> Delete(int? id)
    //{
    //    if (id == null || this.db.ProductImages == null)
    //    {
    //        return NotFound();
    //    }

    //    var productImages = await this.db.ProductImages
    //        .Include(p => p.Product)
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
    //    if (this.db.ProductImages == null)
    //    {
    //        return Problem("Entity set 'ApplicationDbContext.ProductImages'  is null.");
    //    }
    //    var productImages = await this.db.ProductImages.FindAsync(id);
    //    if (productImages != null)
    //    {
    //        this.db.ProductImages.Remove(productImages);
    //    }

    //    await this.db.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool ProductImagesExists(int id)
    //{
    //    return (this.db.ProductImages?.Any(e => e.Id == id)).GetValueOrDefault();
    //}
}
