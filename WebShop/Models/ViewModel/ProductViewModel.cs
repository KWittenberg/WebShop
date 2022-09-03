namespace WebShop.Models.ViewModel;

public class ProductViewModel : ProductBase
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public ProductCategoryViewModel ProductCategory { get; set; }
    public int ProductCategoryId { get; set; }


    // Add ProductImages
    public ICollection<ProductImages> ProductImages { get; set; }
    

    public decimal PriceEuro()
    {
        var priceEuro = Price / 7.53450m;
        return priceEuro;
    }

    public decimal DiscountPriceEuro()
    {
        var discountPriceEuro = DiscountPrice / 7.53450m;
        return discountPriceEuro;
    }
}