namespace WebShop.Models.Base;

public abstract class ProductBase
{
    // 01
    [Required] [StringLength(200, MinimumLength = 2)] public string Title { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? Author { get; set; }
    // Image
    public string? Image { get; set; }

    
    // 02
    public bool Available { get; set; }
    public int? Quantity { get; set; }
    [Column(TypeName = "decimal(9, 2)")] public decimal? Price { get; set; }
    public bool Discount { get; set; }
    [Column(TypeName = "decimal(9, 2)")] public decimal? DiscountPrice { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }


    // 03
    public int? YearOfPublication { get; set; }
    public string? Publisher { get; set; }
    public string? Isbn { get; set; }
    public BookCategory? BookCategory { get; set; }
    public BookBinding? BookBinding { get; set; }
    public int? NumberOfPages { get; set; }


    // 04
    public int? Width { get; set; }
    public int? Height { get; set; }
    public int? Thickness { get; set; }
    public int? Weight { get; set; }
}