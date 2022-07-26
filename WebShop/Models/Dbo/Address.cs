namespace WebShop.Models.Dbo;

public class Address : AddressBase, IEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
}