namespace WebShop.Models.Binding;

public class TokenLoginBinding
{
    [Required]
    [DisplayName("User Name")]
    public string UserName { get; set; }
    [Required]
    [DisplayName("Password")]
    public string Password { get; set; }
}