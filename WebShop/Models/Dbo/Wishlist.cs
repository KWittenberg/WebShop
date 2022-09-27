namespace WebShop.Models.Dbo;

public class Wishlist
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    public int ProductId { get; set; }
    public decimal? Price { get; set; }
    public bool? isActive { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
    public virtual Product Product { get; set; }
}