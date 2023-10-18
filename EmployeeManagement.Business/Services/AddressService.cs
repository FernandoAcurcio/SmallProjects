using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Dtos.Address;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using FluentValidation;

namespace EmployeeManagement.Business.Services
{
    public class AddressService : IAddressService
    {
        private IMapper _mapper { get; }
        private IGenericRepository<Address> _addressRepository { get; }
        private AddressCreateValidator _addressCreateValidator { get; }
        private AddressUpdateValidator _addressUpdateValidator { get; }

        public AddressService(IMapper mapper, IGenericRepository<Address> addressRepository,
               AddressCreateValidator addressCreateValidator, AddressUpdateValidator addressUpdateValidator)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _addressCreateValidator = addressCreateValidator;
            _addressUpdateValidator = addressUpdateValidator;
        }

        public async Task<int> CreateAddressAsync(AddressUpdate addressCreate)
        {
            await _addressCreateValidator.ValidateAndThrowAsync(addressCreate);
            var entity = _mapper.Map<Address>(addressCreate);
            await _addressRepository.InsertAsync(entity);
            await _addressRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAddressAsync(AddressDelete addressDelete)
        {
            var entity = await _addressRepository.GetByIdAsync(addressDelete.Id, address => address.Employees);

            if (entity == null) { throw new AddressNotFoundException(addressDelete.Id); }

            if (entity.Employees.Count > 0) { throw new DependentEmployeesExistException(entity.Employees); }

            _addressRepository.Delete(entity);
            await _addressRepository.SaveChangesAsync();
        }

        public async Task<AddressGet> GetAddressAsync(int id)
        {
            var entity = await _addressRepository.GetByIdAsync(id);

            if (entity == null) { throw new AddressNotFoundException(id); }

            return _mapper.Map<AddressGet>(entity);
        }

        public async Task<List<AddressGet>> GetAddressesAsync()
        {
            var entity = await _addressRepository.GetAsync(null, null);
            return _mapper.Map<List<AddressGet>>(entity);
        }

        public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
        {
            await _addressUpdateValidator.ValidateAndThrowAsync(addressUpdate);

            var existingAddress = await _addressRepository.GetByIdAsync(addressUpdate.Id);

            if (existingAddress == null) { throw new AddressNotFoundException(addressUpdate.Id); }

            var entity = _mapper.Map<Address>(addressUpdate);
            _addressRepository.Update(entity);
            await _addressRepository.SaveChangesAsync();
        }
    }
}
