namespace WebShop.Models.Dbo;

public class Product : ProductBase, IEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public int ProductCategoryId { get; set; }
}