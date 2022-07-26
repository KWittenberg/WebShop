namespace WebShop.Models.Dbo;

public class Order : OrderBase, IEntityBase
{
    public ShoppingCart ShoppingCart { get; set; }
    public int Id { get; set; }
    public DateTime Created { get; set; }
}