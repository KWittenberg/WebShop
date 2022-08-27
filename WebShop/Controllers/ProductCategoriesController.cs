using WebShop.Extensions;

namespace WebShop.Controllers;

[Authorize(Roles = Roles.Admin)]
public class ProductCategoriesController : BaseController
{
    private readonly ApplicationDbContext _context;

    public ProductCategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ProductCategories
    public async Task<IActionResult> Index()
    {
        //BasicNotification("Delete", NotificationType.Info, "Correcto!");
        //CustomNotification("Created", NotificationType.Error, NotificationPosition.Center,"Correcto!",showConfirmButton:true,timer:5000,toast:true);
        DeleteNotification();
        
        return _context.ProductCategory != null ?
                    View(await _context.ProductCategory.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.ProductCategory'  is null.");
    }

    // GET: ProductCategories/Details/1
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.ProductCategory == null)
        {
            return NotFound();
        }

        var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(m => m.Id == id);
        if (productCategory == null)
        {
            return NotFound();
        }

        return View(productCategory);
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
            return View(_context.ProductCategory.Find(id));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit([Bind("Id,Created,Title,Description")] ProductCategory productCategory)
    {
        if (ModelState.IsValid)
        {
            if (productCategory.Id == 0)
            {
                _context.Add(productCategory);
                TempData["success"] = "Category created successfully!";
            }
            else
            {
                _context.Update(productCategory);
                TempData["success"] = "Category updated successfully!";
            }
            await _context.SaveChangesAsync();
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
    public async Task<IActionResult> Delete(int id)
    {
        if (_context.ProductCategory == null)
        {
            return Problem("Entity set 'ApplicationDbContext.ProductCategory'  is null.");
        }
        var category = await _context.ProductCategory.FindAsync(id);
        if (category != null)
        {
            _context.ProductCategory.Remove(category);
            TempData["success"] = "Category deleted successfully!";
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }













    // GET: ProductCategories/Create
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Created,Title,Description")] ProductCategory productCategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(productCategory);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(productCategory);
    }


    // GET: ProductCategories/Edit/1
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.ProductCategory == null)
        {
            return NotFound();
        }

        var productCategory = await _context.ProductCategory.FindAsync(id);
        if (productCategory == null)
        {
            return NotFound();
        }
        return View(productCategory);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Created,Title,Description")] ProductCategory productCategory)
    {
        if (id != productCategory.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(productCategory);
                await _context.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(productCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(productCategory);
    }


    // GET: ProductCategories/Delete/1
    //public async Task<IActionResult> Delete(int? id)
    //{
    //    if (id == null || _context.ProductCategory == null)
    //    {
    //        return NotFound();
    //    }

    //    var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(m => m.Id == id);
    //    if (productCategory == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(productCategory);
    //}
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(int id)
    //{
    //    if (_context.ProductCategory == null)
    //    {
    //        return Problem("Entity set 'ApplicationDbContext.ProductCategory'  is null.");
    //    }
    //    var productCategory = await _context.ProductCategory.FindAsync(id);
    //    if (productCategory != null)
    //    {
    //        _context.ProductCategory.Remove(productCategory);
    //    }

    //    await _context.SaveChangesAsync();
    //    TempData["success"] = "Category deleted successfully";
    //    return RedirectToAction(nameof(Index));
    //}

    private bool ProductCategoryExists(int id)
    {
        return (_context.ProductCategory?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}