namespace WebShop.Models.Base;

public abstract class HeroBase
{
    public string ImageUrl { get; set; }
    public string Align { get; set; }
    public string? Subtitle { get; set; }
    public string Title { get; set; }
    public string ColorTitle { get; set; }
    public string? Description { get; set; }
    public string ColorDescription { get; set; }
    public string ProductUrl { get; set; }
    public bool Publish { get; set; }

    //Badge
    public string? EventName { get; set; }
    public string? EventText { get; set; }
}