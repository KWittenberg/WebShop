namespace WebShop.Models.Base;

public abstract class ShoppingCartItemBase
{
    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(9, 2)")] public decimal Price { get; set; }
}