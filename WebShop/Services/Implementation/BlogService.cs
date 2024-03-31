namespace WebShop.Services.Implementation;

public class BlogService : IBlogService
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;
    
    public BlogService(ApplicationDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }


    public async Task<List<BlogViewModel>> GetBlogsAsync()
    {
        var dbo = await db.Blog.Include(x => x.ApplicationUser)
                                .OrderByDescending(x => x.Created)
                                .ToListAsync();

        return dbo.Select(x => mapper.Map<BlogViewModel>(x)).ToList();
    }

    public async Task<BlogViewModel> GetPostAsync(int id)
    {
        var dbo = await db.Blog.Include(x => x.ApplicationUser)
                                .FirstOrDefaultAsync(x => x.Id == id);
        
        return mapper.Map<BlogViewModel>(dbo);
    }

    public async Task<BlogViewModel> AddBlogAsync(BlogBinding model)
    {
        var user = await db.ApplicationUser.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);
        if (user is null) return null;
        
        model.ImageUrl = "/img/blog/" + model.ImageUrl;
        var dbo = mapper.Map<Blog>(model);
        db.Blog.Add(dbo);
        await db.SaveChangesAsync();
        
        return mapper.Map<BlogViewModel>(dbo);
    }

    public async Task<BlogViewModel> UpdateBlogAsync(BlogBinding model)
    {
        var dbo = await db.Blog.FindAsync(model.Id);
        mapper.Map(model, dbo);
        await db.SaveChangesAsync();
        
        return mapper.Map<BlogViewModel>(dbo);
    }
}