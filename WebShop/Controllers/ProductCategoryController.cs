namespace WebShop.Controllers;

[Authorize(Roles = Roles.Admin)]
public class ProductCategoryController : Controller
{
    private readonly ApplicationDbContext db;

    public ProductCategoryController(ApplicationDbContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// Category
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        return this.db.ProductCategory != null ?
                    View(await this.db.ProductCategory.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.ProductCategory'  is null.");
    }

    /// <summary>
    /// Add Or Edit
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult AddOrEdit(int id = 0)
    {
        if (id == 0)
            return View(new ProductCategory());
        else
            return View(this.db.ProductCategory.Find(id));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit([Bind("Id,Created,Title,Description")] ProductCategory productCategory)
    {
        if (ModelState.IsValid)
        {
            if (productCategory.Id == 0)
            {
                this.db.Add(productCategory);
                TempData["success"] = "Category created successfully!";
            }
            else
            {
                this.db.Update(productCategory);
                TempData["success"] = "Category updated successfully!";
            }
            await this.db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(productCategory);
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
        if (this.db.ProductCategory == null) { return Problem("Entity set 'ApplicationDbContext.ProductCategory'  is null."); }
        var category = await this.db.ProductCategory.FindAsync(id);
        if (category != null)
        {
            this.db.ProductCategory.Remove(category);
            TempData["success"] = "Category deleted successfully!";
        }
        await this.db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}