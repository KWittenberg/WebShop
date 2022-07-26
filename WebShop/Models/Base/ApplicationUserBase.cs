namespace WebShop.Models.Base;

public abstract class ApplicationUserBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public DateTime DOB { get; set; }
}