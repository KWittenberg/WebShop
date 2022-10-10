namespace WebShop.Services.Interface;

public interface IBlogService
{
    // BlogViewModel
    Task<List<BlogViewModel>> GetBlogsAsync();
    Task<BlogViewModel> GetPostAsync(int id);
    Task<BlogViewModel> AddBlogAsync(BlogBinding model);
    Task<BlogViewModel> UpdateBlogAsync(BlogBinding model);
}