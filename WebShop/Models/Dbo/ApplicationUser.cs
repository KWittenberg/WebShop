namespace WebShop.Models.Dbo;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public DateTime DOB { get; set; }

    // Add Address
    public ICollection<Address> Address { get; set; }

    // Add ShoppingChart
    public ICollection<ShoppingCart> ShoppingCart { get; set; }


    // Add Role
    [NotMapped]
    public string RoleId { get; set; }
    [NotMapped]
    public string Role { get; set; }
    [NotMapped]
    public IEnumerable<SelectListItem> RoleList { get; set; }
}