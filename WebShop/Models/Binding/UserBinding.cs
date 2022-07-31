namespace WebShop.Models.Binding;

public class UserBinding
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserUpdateBinding : ApplicationUserBase 
{
    public string Street { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
}