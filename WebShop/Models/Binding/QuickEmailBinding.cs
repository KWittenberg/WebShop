namespace WebShop.Models.Binding;

public class QuickEmailBinding
{
    [Required][DataType(DataType.EmailAddress)][EmailAddress] public string To { get; set; }
    [Required][StringLength(50, MinimumLength = 3, ErrorMessage = "Subject min 3 chars!")] public string Subject { get; set; }
    public string Body { get; set; } = String.Empty;
}