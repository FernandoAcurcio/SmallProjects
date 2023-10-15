using AutoMapper;
using EmployeeManagement.Common.Dtos;
using EmployeeManagement.Common.Model;

namespace EmployeeManagement.Business
{
    public class DtoEntityMapperProfile : Profile
    {
        public DtoEntityMapperProfile()
        {
            CreateMap<AddressCreate, Address>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AddressUpdate, Address>();
            CreateMap<Address, AddressGet>();
        }

    }
}