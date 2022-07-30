namespace WebShop.Models.Binding;

public class ApplicationUserBinding : ApplicationUserBase
{
    // Add Role
    public Roles Roles { get; set; }
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    

    //[NotMapped]
    //public IEnumerable<SelectListItem> RoleList { get; set; }
    
    
    //public AdressBinding UserAdress { get; set; }
}






public class ApplicationUserUpdateBinding : ApplicationUserBinding
{
    public string Id { get; set; }
}