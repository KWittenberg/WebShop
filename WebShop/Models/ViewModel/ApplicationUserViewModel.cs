namespace WebShop.Models.ViewModel;

public class ApplicationUserViewModel : ApplicationUserBase
{
    public string Id { get; set; }
    public DateTime Created { get; set; }

    // Add Address
    //public ICollection<Address> Address { get; set; }



    // Add Role
    [NotMapped]
    public string RoleId { get; set; }
    [NotMapped]
    public string Role { get; set; }
    [NotMapped]
    public IEnumerable<SelectListItem> RoleList { get; set; }


    //public string ApplicationUserId { get; set; }
}