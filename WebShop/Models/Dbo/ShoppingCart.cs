﻿namespace WebShop.Models.Dbo;

public class ShoppingCart : ShoppingCartBase, IEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public ApplicationUser ApplicationUser { get; set; }


    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    public ShoppingCartStatus ShoppingCartStatus { get; set; }
}