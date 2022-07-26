namespace WebShop.Models.Dto;

public class AppConfig
{
    public int ShoppingCartOffset { get; set; }
    public Identity Identity { get; set; }
    public string AppUrl { get; set; }
}

public class Identity
{
    public string Key { get; set; }
}