namespace WebShop.Models.Binding;

public class ProductBinding : ProductBase
{
    public int ProductCategoryId { get; set; }
}

public class ProductUpdateBinding : ProductBinding
{
    public int Id { get; set; }
}