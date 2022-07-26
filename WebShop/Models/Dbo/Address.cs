namespace WebShop.Models.Dbo;

public class Address : AddressBase, IEntityBase
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
}