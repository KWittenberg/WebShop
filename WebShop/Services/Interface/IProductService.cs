namespace WebShop.Services.Interface;

public interface IProductService
{
    // ProductViewModel
    Task<ProductViewModel> GetProductAsync(int id);
    Task<ProductViewModel> GetProductByNameAsync(string name);
    Task<List<ProductViewModel>> GetProductsAsync();
    Task<ProductViewModel> AddProductAsync(ProductBinding model);
    Task<ProductViewModel> UpdateProductAsync(ProductUpdateBinding model);
    Task<ProductViewModel> DeleteProductAsync(ProductUpdateBinding model);

    // ProductCategoryViewModel
    Task<ProductCategoryViewModel> GetProductCategoryAsync(int id);
    Task<List<ProductCategoryViewModel>> GetProductCategorysAsync();
    Task<ProductCategoryViewModel> AddProductCategoryAsync(ProductCategoryBinding model);
    Task<ProductCategoryViewModel> UpdateProductCategoryAsync(ProductCategoryUpdateBinding model);

    // ShoppingCartViewModel
    Task<ShoppingCartViewModel> GetShoppingCartAsync(string userId);
    Task<List<ShoppingCartViewModel>> GetShoppingCartsAsync(ShoppingCartStatus status);
    Task<ShoppingCartViewModel> AddShoppingCartAsync(ShoppingCartBinding model);
    Task SuspendShoppingCartItem(int shoppingCartItemId);
    Task<ShoppingCartViewModel> SuspendShoppingCart(int id);

    
    // OrderViewModel
    Task<OrderViewModel> GetOrderAsync(int id);
    Task<List<OrderViewModel>> GetOrdersAsync();
    Task<OrderViewModel> AddOrder(OrderBinding model);
    Task<OrderViewModel> SuspendOrder(int id);


    Task UpdateShoppinCartStatus();
}