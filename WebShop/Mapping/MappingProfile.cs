namespace WebShop.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ApplicationUser
        CreateMap<ApplicationUserBinding, ApplicationUser>();
        CreateMap<ApplicationUser, ApplicationUserViewModel>();

        // Address
        CreateMap<AddressBinding, Address>();
        CreateMap<AddressUpdateBinding, Address>();
        CreateMap<Address, AddressViewModel>();
        CreateMap<AddressViewModel, AddressUpdateBinding>();

        // Product
        CreateMap<ProductBinding, Product>();
        CreateMap<ProductUpdateBinding, Product>();
        CreateMap<Product, ProductViewModel>();
        CreateMap<ProductViewModel, ProductUpdateBinding>();
        
        // ProductCategory
        CreateMap<ProductCategoryBinding, ProductCategory>();
        CreateMap<ProductCategoryUpdateBinding, ProductCategory>();
        CreateMap<ProductCategory, ProductCategoryViewModel>();
        
        // ShoppingCartItem
        CreateMap<ShoppingCartItemBinding, ShoppingCartItem>();
        CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>();

        // ShoppingCart
        CreateMap<ShoppingCart, ShoppingCartViewModel>();

        // Order
        CreateMap<Order, OrderViewModel>();
    }
}