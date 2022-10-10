namespace WebShop.Models.Binding;

public class ContactBinding
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string To { get; set; } = "boltawebshop@gmail.com";

    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Subject min 3 chars!")]
    public string Subject { get; set; } = "Bolta WebShop Message from: ";

    [Required]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Text Message min 3 chars!")]
    public string Body { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name min 3 chars!")]
    public string MessageName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string MessageEmail { get; set; }
}