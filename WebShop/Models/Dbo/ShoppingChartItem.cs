namespace WebShop.Models.Dbo;

public class ShoppingCartItem : ShoppingCartItemBase, IEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public Product Product { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
}