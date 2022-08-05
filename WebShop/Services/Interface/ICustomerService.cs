namespace WebShop.Services.Interface;

public interface ICustomerService
{
    // AddressViewModel
    Task<List<ApplicationUserViewModel>> GetAddresses(string Id);
    Task<AddressViewModel> GetAddress(string userId);
    Task<AddressViewModel> GetAddressInt(int id);
    Task<AddressViewModel> AddAddressAsync(AddressBinding model);
    Task<AddressViewModel> UpdateAddressAsync(AddressUpdateBinding model);
    Task<AddressViewModel> DeleteAddressAsync(AddressUpdateBinding model);


    // ApplicationUserViewModel
    Task<ApplicationUserViewModel> GetApplicationUser(string userId);
    Task<List<ApplicationUserViewModel>> GetApplicationUsers();
}