namespace WebShop.Models.ViewModel;

public class ProductImagesViewModel : ProductImagesBase
{
    public int Id { get; set; }
    public DateTime Created { get; set; }

    public ProductViewModel Product { get; set; }
    public int ProductId { get; set; }
}