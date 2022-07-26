namespace WebShop.Services.Implementation;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;

    public CustomerService(ApplicationDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }


    /// <summary>
    /// Get Address by Int
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<AddressViewModel> GetAddressInt(int id)
    {
        var address = await db.Address.FindAsync(id);
        return mapper.Map<AddressViewModel>(address);
    }

    /// <summary>
    /// Get Address
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<AddressViewModel> GetAddress(string userId)
    {
        var address = await db.Address.FirstOrDefaultAsync(x => x.ApplicationUser.Id == userId);
        return mapper.Map<AddressViewModel>(address);
    }

    /// <summary>
    /// Add Address
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<AddressViewModel> AddAddressAsync(AddressBinding model)
    {
        var user = await db.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);
        if (user == null) { return null; }
        var dbo = mapper.Map<Address>(model);
        user.Address.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<AddressViewModel>(dbo);
    }
    
    /// <summary>
    /// Update Address
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<AddressViewModel> UpdateAddressAsync(AddressUpdateBinding model)
    {
        var user = await db.ApplicationUser.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);
        var dbo = await db.Address.FindAsync(model.Id);
        mapper.Map(model, dbo);
        dbo.ApplicationUser = user;
        await db.SaveChangesAsync();
        return mapper.Map<AddressViewModel>(dbo);
    }

    /// <summary>
    /// Delete Address
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<AddressViewModel> DeleteAddressAsync(AddressUpdateBinding model)
    {
        var user = await db.ApplicationUser.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);
        var dbo = await db.Address.FindAsync(model.Id);
        mapper.Map(model, dbo);
        dbo.ApplicationUser = user;
        db.Address.Remove(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<AddressViewModel>(dbo);
    }

    

    /// <summary>
    /// Get ApplicationUser
    /// </summary>
    /// <returns></returns>
    public async Task<ApplicationUserViewModel> GetApplicationUser(string userId)
    {
        var users = await db.ApplicationUser.FindAsync(userId);

        return mapper.Map<ApplicationUserViewModel>(users);
    }

    /// <summary>
    /// Get All ApplicationUsers
    /// </summary>
    /// <returns></returns>
    public async Task<List<ApplicationUserViewModel>> GetApplicationUsers()
    {
        var dbo = await db.ApplicationUser.ToListAsync();
        return dbo.Select(x => mapper.Map<ApplicationUserViewModel>(x)).ToList();
    }




    public async Task UpdateUserPhone(string userId, string phone)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        user.PhoneNumber = phone;
        await db.SaveChangesAsync();
    }
}