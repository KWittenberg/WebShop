namespace WebShop.Models.ViewModel;

public class ShoppingCartViewModel : ShoppingCartBase
{
    public int Id { get; set; }
    public List<ShoppingCartItemViewModel>? ShoppingCartItems { get; set; }
    public ApplicationUserViewModel? ApplicationUser { get; set; }
    public List<AddressViewModel>? Address { get; set; }

    public decimal GetSubTotalPrice()
    {
        decimal subTotalPrice = 0;

        if (ShoppingCartItems == null)
        {
            return default;
        }
        if (ShoppingCartItems.Any())
        {
            foreach (var item in ShoppingCartItems)
            {
                subTotalPrice += item.Price * item.Quantity;
            }

        }
        return subTotalPrice;
    }
    public decimal GetTotalPrice()
    {
        decimal subTotalPrice = GetSubTotalPrice();
        if (subTotalPrice == default)
        {
            return default;
        }
        var totalPrice = subTotalPrice + 55;
        return totalPrice;
    }

    public decimal GetTotalPriceWithVAT()
    {
        decimal totalPrice = GetTotalPrice();
        if (totalPrice == default)
        {
            return default;
        }
        totalPrice = totalPrice * 1.25M;
        return totalPrice;
    }

    public ShoppingCartStatus ShoppingCartStatus { get; set; }
    public DateTime Created { get; set; }
}