namespace WebShop.Services.Implementation;

public class BlogService : IBlogService
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;
    private readonly AppConfig appConfig;

    public BlogService(ApplicationDbContext db, IMapper mapper, IOptions<AppConfig> appConfig)
    {
        this.db = db;
        this.mapper = mapper;
        this.appConfig = appConfig.Value;
    }


    /// <summary>
    /// Get Blogs
    /// </summary>
    /// <returns></returns>
    public async Task<List<BlogViewModel>> GetBlogsAsync()
    {
        var dbo = await db.Blog.Include(x => x.ApplicationUser)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
        return dbo.Select(x => mapper.Map<BlogViewModel>(x)).ToList();
    }

    /// <summary>
    /// Get Blog
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<BlogViewModel> GetPostAsync(int id)
    {
        var dbo = await db.Blog.Include(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.Id == id);
        return mapper.Map<BlogViewModel>(dbo);
    }

    /// <summary>
    /// Add Blog
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<BlogViewModel> AddBlogAsync(BlogBinding model)
    {
        var user = await db.ApplicationUser.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);
        if (user == null) { return null; }
        model.ImageUrl = "/img/blog/" + model.ImageUrl;
        var dbo = mapper.Map<Blog>(model);
        db.Blog.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<BlogViewModel>(dbo);
    }

    /// <summary>
    /// Update Blog
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<BlogViewModel> UpdateBlogAsync(BlogBinding model)
    {
        var dbo = await db.Blog.FindAsync(model.Id);
        mapper.Map(model, dbo);
        await db.SaveChangesAsync();
        return mapper.Map<BlogViewModel>(dbo);
    }
}