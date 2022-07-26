namespace WebShop.Models.Dbo;

public class ProductCategory : ProductCategoryBase, IEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
}