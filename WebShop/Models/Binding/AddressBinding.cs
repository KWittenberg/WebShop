namespace WebShop.Models.Binding;

public class AddressBinding : AddressBase
{
    public string ApplicationUserId { get; set; }
}

public class AddressUpdateBinding : AddressBinding
{
    public int Id { get; set; }
}