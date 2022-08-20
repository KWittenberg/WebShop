namespace WebShop.Services.Implementation;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;
    private readonly AppConfig appConfig;

    public ProductService(ApplicationDbContext db, IMapper mapper, IOptions<AppConfig> appConfig)
    {
        this.db = db;
        this.mapper = mapper;
        this.appConfig = appConfig.Value;
    }

    /// <summary>
    /// Add Product
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ProductViewModel> AddProductAsync(ProductBinding model)
    {
        var dbo = mapper.Map<Product>(model);
        var productCategory = await db.ProductCategory.FindAsync(model.ProductCategoryId);
        if (productCategory == null) { return null; }
        dbo.ProductCategory = productCategory;
        db.Product.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ProductViewModel>(dbo);
    }

    /// <summary>
    /// Update Product
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ProductViewModel> UpdateProductAsync(ProductUpdateBinding model)
    {
        var category = await db.ProductCategory.FirstOrDefaultAsync(x => x.Id == model.ProductCategoryId);
        var dbo = await db.Product.FindAsync(model.Id);
        mapper.Map(model, dbo);
        dbo.ProductCategory = category;
        await db.SaveChangesAsync();
        return mapper.Map<ProductViewModel>(dbo);
    }

    /// <summary>
    /// Delete Product
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ProductViewModel> DeleteProductAsync(ProductUpdateBinding model)
    {
        //var category = await db.ProductCategory.FirstOrDefaultAsync(x => x.Id == model.ProductCategoryId);
        var dbo = await db.Product.FindAsync(model.Id);
        //mapper.Map(model, dbo);
        //dbo.ProductCategory = category;
        db.Product.Remove(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ProductViewModel>(dbo);
    }



    /// <summary>
    /// Find Product by id-1
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ProductViewModel> GetProductAsync(int id)
    {
        var dbo = await db.Product.Include(x => x.ProductCategory).FirstOrDefaultAsync(x => x.Id == id);
        return mapper.Map<ProductViewModel>(dbo);
    }

    /// <summary>
    /// Find Product by Name
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ProductViewModel> GetProductByNameAsync(string name)
    {
        var dbo = await db.Product.Include(x => x.ProductCategory).FirstOrDefaultAsync(x => x.Title == name);
        return mapper.Map<ProductViewModel>(dbo);
    }

    /// <summary>
    /// Find All Products
    /// </summary>
    /// <returns></returns>
    public async Task<List<ProductViewModel>> GetProductsAsync()
    {
        var dbo = await db.Product.Include(x => x.ProductCategory).ToListAsync();
        return dbo.Select(x => mapper.Map<ProductViewModel>(x)).ToList();
    }

    /// <summary>
    /// Add Category Product
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ProductCategoryViewModel> AddProductCategoryAsync(ProductCategoryBinding model)
    {
        var dbo = mapper.Map<ProductCategory>(model);
        db.ProductCategory.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ProductCategoryViewModel>(dbo);
    }

    /// <summary>
    /// Find Category Product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ProductCategoryViewModel> GetProductCategoryAsync(int id)
    {
        var dbo = await db.ProductCategory.FindAsync(id);
        return mapper.Map<ProductCategoryViewModel>(dbo);
    }

    /// <summary>
    /// Find All Category Product
    /// </summary>
    /// <returns></returns>
    public async Task<List<ProductCategoryViewModel>> GetProductCategorysAsync()
    {
        var dbo = await db.ProductCategory.ToListAsync();
        return dbo.Select(x => mapper.Map<ProductCategoryViewModel>(x)).ToList();
    }

    /// <summary>
    /// UpdateProductCategory
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ProductCategoryViewModel> UpdateProductCategoryAsync(ProductCategoryUpdateBinding model)
    {
        var dbo = await db.ProductCategory.FindAsync(model.Id);
        mapper.Map(model, dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ProductCategoryViewModel>(dbo);
    }



    /// <summary>
    /// AddShoppingCartItem
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ShoppingCartItemViewModel> AddShoppingCartItemAsync(ShoppingCartItemBinding model)
    {
        var dbo = mapper.Map<ShoppingCartItem>(model);
        db.ShoppingCartItem.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ShoppingCartItemViewModel>(dbo);
    }

    /// <summary>
    /// GetShoppingCartItem
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ShoppingCartItemViewModel> GetShoppingCartItemAsync(int id)
    {
        var dbo = await db.ShoppingCartItem.FindAsync(id);
        return mapper.Map<ShoppingCartItemViewModel>(dbo);
    }

    /// <summary>
    /// Get ShoppingCartItems
    /// </summary>
    /// <returns></returns>
    public async Task<List<ShoppingCartItemViewModel>> GetShoppingCartItemsAsync()
    {
        var dbo = await db.ShoppingCartItem.ToListAsync();
        return dbo.Select(x => mapper.Map<ShoppingCartItemViewModel>(x)).ToList();
    }



    /// <summary>
    /// AddShoppingCart
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ShoppingCartViewModel> AddShoppingCartAsync(ShoppingCartBinding model)
    {
        if (model.ShoppingCartId.HasValue)
        {
            return await AddItemToShoppingCartAsync(model);
        }


        var product = await db.Product.FindAsync(model.ProductId);
        product.Quantity -= model.Quantity;

        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
        if (product == null || user == null) { return null; }

        var shoppingCartItem = new ShoppingCartItem
        {
            Price = model.Price,
            Product = product,
            Quantity = model.Quantity
        };

        var dbo = new ShoppingCart
        {
            ShoppingCartItems = new List<ShoppingCartItem> { shoppingCartItem },
            ApplicationUser = user,
            ShoppingCartStatus = Models.ShoppingCartStatus.Pending
        };

        db.ShoppingCart.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ShoppingCartViewModel>(dbo);
    }


    /// <summary>
    /// GetShoppingCartAsync by id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<ShoppingCartViewModel> GetShoppingCartAsync(string userId)
    {
        var shoppingCart = await db.ShoppingCart
            .Include(x=>x.ApplicationUser.Address)
            .Include(x => x.ShoppingCartItems)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.ProductCategory)
            .FirstOrDefaultAsync(x => x.ApplicationUser.Id == userId && x.ShoppingCartStatus == Models.ShoppingCartStatus.Pending);

        if (shoppingCart == null) { return null; }

        return mapper.Map<ShoppingCartViewModel>(shoppingCart);
    }

    /// <summary>
    /// Get ShoppingCarts Status
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<List<ShoppingCartViewModel>> GetShoppingCartsAsync(ShoppingCartStatus status)
    {
        var shoppingCarts = await db.ShoppingCart
            .Include(x => x.ShoppingCartItems)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.ProductCategory)

            .Where(x => x.ShoppingCartStatus == status).ToListAsync();

        if (!shoppingCarts.Any())
        {
            return new List<ShoppingCartViewModel>();
        }

        return shoppingCarts.Select(x => mapper.Map<ShoppingCartViewModel>(x)).ToList();
    }



    /// <summary>
    /// Get Order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OrderViewModel> GetOrderAsync(int id)
    {
        var order = await db.Order
            .Include(x => x.ShoppingCart)
            .ThenInclude(x => x.ApplicationUser)
            .ThenInclude(x => x.Address)
            .Include(x => x.ShoppingCart)
            .ThenInclude(x => x.ShoppingCartItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        return mapper.Map<OrderViewModel>(order);
    }

    /// <summary>
    /// Get Orders
    /// </summary>
    /// <returns></returns>
    public async Task<List<OrderViewModel>> GetOrdersAsync()
    {
        var orders = await db.Order
            .Include(x => x.ShoppingCart)
            .ThenInclude(x => x.ApplicationUser)
            .Include(x => x.ShoppingCart)
            .ThenInclude(x => x.ShoppingCartItems)
            .ThenInclude(x => x.Product)
            .ToListAsync();

        return orders.Select(x => mapper.Map<OrderViewModel>(x)).ToList();
    }

    /// <summary>
    /// GetOrdersByUserAsync
    /// </summary>
    /// <returns></returns>
    public async Task<List<OrderViewModel>> GetOrdersByUserAsync(string id)
    {
        var orders = await db.Order
            .Include(x => x.ShoppingCart)
            .ThenInclude(x => x.ApplicationUser)
            .Include(x => x.ShoppingCart)
            .ThenInclude(x => x.ShoppingCartItems)
            .ThenInclude(x => x.Product)
            .ToListAsync();

        var userOrders = orders.FindAll(x => x.ShoppingCart.ApplicationUser.Id == id);
        return userOrders.Select(x => mapper.Map<OrderViewModel>(x)).ToList();
    }


    /// <summary>
    /// Add Order
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<OrderViewModel> AddOrder(OrderBinding model)
    {
        var shoppingCart = await db.ShoppingCart.FirstOrDefaultAsync(x => x.Id == model.ShoppingCartId);
        shoppingCart.ShoppingCartStatus = Models.ShoppingCartStatus.Succeeded;

        if (shoppingCart == null)
        {
            return null;
        }

        var dbo = new Order
        {
            Paid = true,
            ShoppingCart = shoppingCart

        };

        db.Order.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<OrderViewModel>(dbo);
    }



    /// <summary>
    /// Add Item To ShoppingCart
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private async Task<ShoppingCartViewModel> AddItemToShoppingCartAsync(ShoppingCartBinding model)
    {
        var dbo = await db.ShoppingCart.Include(x => x.ShoppingCartItems).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == model.ShoppingCartId.GetValueOrDefault());

        var product = await db.Product.FindAsync(model.ProductId);

        product.Quantity -= model.Quantity;

        var presentShoppingCartItem = dbo.ShoppingCartItems.FirstOrDefault(x => x.Product.Id == model.ProductId);
        if (presentShoppingCartItem != null)
        {
            presentShoppingCartItem.Quantity += model.Quantity;
            await db.SaveChangesAsync();
            return mapper.Map<ShoppingCartViewModel>(dbo);
        }

        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
        if (product == null || user == null) { return null; }


        var shoppingCartItem = new ShoppingCartItem
        {
            Price = model.Price,
            Product = product,
            Quantity = model.Quantity
        };

        if (dbo == null) { return null; }

        dbo.ShoppingCartItems.Add(shoppingCartItem);
        await db.SaveChangesAsync();
        return mapper.Map<ShoppingCartViewModel>(dbo);
    }








    /// <summary>
    /// SuspendOrder by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OrderViewModel> SuspendOrder(int id)
    {
        var order = await db.Order
            .Include(x => x.ShoppingCart)
            .ThenInclude(x => x.ShoppingCartItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
        SuspendShoppingCart(order.ShoppingCart);
        await db.SaveChangesAsync();
        return mapper.Map<OrderViewModel>(order);
    }






    /// <summary>
    /// UpdateShoppinCartStatus
    /// Ako je shoppingcart u statusu active npr 2h.
    /// Prebaciti status u suspended
    /// Napraviti povrat robe na odg kolicinu
    /// </summary>
    /// <returns></returns>
    public async Task UpdateShoppinCartStatus()
    {
        var shoppingCarts = await db.ShoppingCart.Include(x => x.ShoppingCartItems).ThenInclude(x => x.Product)
            .Where(x => x.ShoppingCartStatus == ShoppingCartStatus.Pending && x.Created < DateTime.Now.AddHours(appConfig.ShoppingCartOffset))
            .ToListAsync();

        if (!shoppingCarts.Any()) { return; }

        SuspendShoppingCarts(shoppingCarts);
        await db.SaveChangesAsync();
    }

    /// <summary>
    /// SuspendShoppingCart
    /// </summary>
    /// <param name="shoppingCart"></param>
    /// <returns></returns>
    private static ShoppingCart SuspendShoppingCart(ShoppingCart shoppingCart)
    {
        shoppingCart.ShoppingCartStatus = ShoppingCartStatus.Suspended;
        foreach (var cartItems in shoppingCart.ShoppingCartItems)
        {
            cartItems.Product.Quantity += cartItems.Quantity;
        }

        return shoppingCart;
    }

    /// <summary>
    /// SuspendShoppingCarts
    /// </summary>
    /// <param name="shoppingCarts"></param>
    /// <returns></returns>
    private List<ShoppingCart> SuspendShoppingCarts(List<ShoppingCart> shoppingCarts)
    {
        foreach (var shoppingCart in shoppingCarts)
        {
            SuspendShoppingCart(shoppingCart);
        }

        return shoppingCarts;
    }



    /// <summary>
    /// SuspendShoppingCartItem
    /// </summary>
    /// <param name="shoppingCartItemId"></param>
    /// <returns></returns>
    public async Task SuspendShoppingCartItem(int shoppingCartItemId)
    {

        var shoppingCartItem = await db.ShoppingCartItem.Include(x => x.ShoppingCart).ThenInclude(x => x.ShoppingCartItems)
            .FirstOrDefaultAsync(x => x.Id == shoppingCartItemId);

        if (shoppingCartItem == null) { return; }

        if (shoppingCartItem.ShoppingCart.ShoppingCartItems.Count == 1)
        {
            await SuspendShoppingCart(shoppingCartItem.ShoppingCart.Id);
            return;
        }

        try
        {
            shoppingCartItem.ShoppingCart.ShoppingCartItems.Remove(shoppingCartItem);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
        return;
    }

    /// <summary>
    /// SuspendShoppingCart
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ShoppingCartViewModel> SuspendShoppingCart(int id)
    {
        var shoppingCart = await db.ShoppingCart.Include(x => x.ShoppingCartItems).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (shoppingCart == null) { return null; }

        SuspendShoppingCart(shoppingCart);
        await db.SaveChangesAsync();
        return mapper.Map<ShoppingCartViewModel>(shoppingCart);
    }






    /// <summary>
    /// Change Available Status
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<ProductViewModel?> ChangeAvailableStatus(int Id, bool status)
    {
        var product = await db.Product.FindAsync(Id);
        if (product == null) { return null; }
        product.Available = status;
        await db.SaveChangesAsync();
        return mapper.Map<ProductViewModel>(product);
    }
    
    /// <summary>
    /// Change Discount Status
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<ProductViewModel?> ChangeDiscountStatus(int Id, bool status)
    {
        var product = await db.Product.FindAsync(Id);
        if (product == null) { return null; }
        product.Discount = status;
        await db.SaveChangesAsync();
        return mapper.Map<ProductViewModel>(product);
    }
}