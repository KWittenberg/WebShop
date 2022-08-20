namespace WebShop.Models.Binding;

public class ApplicationUserBinding : ApplicationUserBase
{
    public string Password { get; set; }
    
    // Add Role
    public Roles Roles { get; set; }
    public string RoleId { get; set; }
    public string Role { get; set; }
    

    //[NotMapped]
    //public IEnumerable<SelectListItem> RoleList { get; set; }
    
    
    //public AdressBinding UserAdress { get; set; }
}






public class ApplicationUserUpdateBinding : ApplicationUserBinding
{
    public string Id { get; set; }
}