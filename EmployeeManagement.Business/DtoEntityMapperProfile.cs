using AutoMapper;
using EmployeeManagement.Common.Dtos.Address;
using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Model;
using System.ComponentModel.Design;

namespace EmployeeManagement.Business
{
    public class DtoEntityMapperProfile : Profile
    {
        public DtoEntityMapperProfile()
        {
            CreateMap<AddressCreate, Address>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AddressUpdate, Address>();
            CreateMap<Address, AddressGet>();
        
            CreateMap<JobCreate, Job>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<JobUpdate, Job>();
            CreateMap<Job, JobGet>();
        }

    }
}