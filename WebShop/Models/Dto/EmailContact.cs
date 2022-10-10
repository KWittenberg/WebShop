namespace WebShop.Models.Dto;

public class EmailContact : EmailDto
{
    [Required][StringLength(50, MinimumLength = 3, ErrorMessage = "Name min 3 chars!")] public string MessageName { get; set; }
    [Required][DataType(DataType.EmailAddress)][EmailAddress] public string MessageEmail { get; set; }
}