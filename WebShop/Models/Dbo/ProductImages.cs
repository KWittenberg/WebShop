namespace WebShop.Models.Dbo;

public class ProductImages : ProductImagesBase, IEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }

    // Add Product
    //public Product Product { get; set; }
    
    //public int ProductId { get; set; }
}