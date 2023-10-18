using EmployeeManagement.Common.Dtos.Address;

namespace EmployeeManagement.Common.Interfaces
{
    public interface IAddressService 
    {
        Task<int> CreateAddressAsync(AddressUpdate addressCreate);
        Task UpdateAddressAsync(AddressUpdate addressUpdate);
        Task DeleteAddressAsync(AddressDelete addressDelete);
        Task<AddressGet> GetAddressAsync(int id);
        Task<List<AddressGet>> GetAddressesAsync();

    }
}
