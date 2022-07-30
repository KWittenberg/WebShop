namespace WebShop.Models;

public class Roles
{
    //public const string SuperAdmin = "SuperAdmin";
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Employee = "Employee";
}


public enum ShoppingCartStatus
{
    Pending,
    Succeeded,
    Suspended
}