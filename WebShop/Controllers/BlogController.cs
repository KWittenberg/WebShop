namespace WebShop.Controllers;

public class BlogController : Controller
{
    private readonly ApplicationDbContext db;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IBlogService blogService;

    public BlogController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IBlogService blogService)
    {
        this.db = db;
        this.userManager = userManager;
        this.blogService = blogService;
    }


    /// <summary>
    /// Blogs
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var blogs = await blogService.GetBlogsAsync();
        return View(blogs);
    }

    ///// <summary>
    ///// Details Hero
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpGet]
    //public async Task<IActionResult> Details(int id)
    //{
    //    var hero = await productService.GetHeroAsync(id);
    //    return View(hero);
    //}

    /// <summary>
    /// Create Blog
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogBinding model)
    {
        var userId = this.userManager.GetUserId(User);
        model.ApplicationUserId = userId;
        await blogService.AddBlogAsync(model);
        TempData["success"] = "Blog created successfully!";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Edit Blog
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var blog = await blogService.GetPostAsync(id);
        return View(blog);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BlogBinding model)
    {
        await blogService.UpdateBlogAsync(model);
        TempData["success"] = "Blog update successfully!";
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Delete Blog
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (this.db.Blog == null) { return Problem("Entity set 'db.Blog' is null."); }
        var blog = await this.db.Blog.FindAsync(id);
        if (blog != null)
        {
            this.db.Blog.Remove(blog);
            TempData["success"] = "Blog deleted successfully!";
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
        var blog = await db.Blog.FindAsync(id);
        if (blog == null) { return null; }
        blog.Publish = status;
        await db.SaveChangesAsync();
        TempData["success"] = "Blog successfully publish!";
        return RedirectToAction(nameof(Index));
    }
}