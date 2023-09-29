namespace WebShop.Models.Base;

public abstract class BlogBase
{
    public string Title { get; set; }
    
    public string ImageUrl { get; set; }
    
    public string Content { get; set; }
    
    public bool Publish { get; set; }


    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }
    
    public string ApplicationUserId { get; set; }
}