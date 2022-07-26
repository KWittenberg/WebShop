﻿namespace WebShop.Models.ViewModel;

public class ApplicationUserViewModel : ApplicationUserBase
{
    public string Id { get; set; }
    public string Password { get; set; }
    public DateTime Created { get; set; }

    // Add Address
    //public ICollection<Address> Address { get; set; }
    public List<AddressViewModel>? Address { get; set; }


    // Add Role
    //public Roles Roles { get; set; }
    public string RoleId { get; set; }
    public string Role { get; set; }

    // Add ToDoList
    public List<ToDoListViewModel> ToDoList { get; set; }
}