namespace WebShop.Models.ViewModel;

public class ProductViewModel : ProductBase
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public ProductCategoryViewModel ProductCategory { get; set; }
    public int ProductCategoryId { get; set; }
}