namespace WebShop.Models.Binding;

public class UserBinding
{
    [Required][StringLength(50, MinimumLength = 3, ErrorMessage = "First Name min 3 chars!")] public string FirstName { get; set; }
    [Required][StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name min 3 chars!")] public string LastName { get; set; }
    [Required][StringLength(50, MinimumLength = 9, ErrorMessage = "Phone Number min 9 chars!")] public string PhoneNumber { get; set; }
    [Required][DataType(DataType.EmailAddress)][EmailAddress] public string Email { get; set; }
    [Required][StringLength(50, MinimumLength = 8, ErrorMessage = "Password min 8 chars!")] public string Password { get; set; }
    [Required] public DateTime DOB { get; set; }

    // Add for Address
    public bool Primary { get; set; }
    [Required][StringLength(50, MinimumLength = 3, ErrorMessage = "Street min 3 chars!")] public string Street { get; set; }
    [Required][StringLength(50, MinimumLength = 3, ErrorMessage = "City min 3 chars!")] public string City { get; set; }
    [Required][StringLength(50, MinimumLength = 5, ErrorMessage = "Post Code min 5 chars!")] public string PostCode { get; set; }
    [Required][StringLength(50, MinimumLength = 3, ErrorMessage = "Country min 3 chars!")] public string Country { get; set; }
}

public class UserUpdateBinding 
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DOB { get; set; }
}

public class LoginUserBinding
{
    public string Email { get; set; }
    public string Password { get; set; }
}