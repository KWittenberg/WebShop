namespace WebShop.Models.Base;

public abstract class AddressBase
{
    public string Street { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }

    
    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
}