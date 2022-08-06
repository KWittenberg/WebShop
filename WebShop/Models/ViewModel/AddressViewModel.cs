namespace WebShop.Models.ViewModel;

public class AddressViewModel : AddressBase
{
    public int Id { get; set; }
    public DateTime Created { get; set; }

    // Relationships to ApplicationUser
    public ApplicationUserViewModel ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
}