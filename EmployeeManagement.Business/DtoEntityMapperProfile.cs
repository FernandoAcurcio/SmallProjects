﻿using AutoMapper;
using EmployeeManagement.Common.Dtos.Address;
using EmployeeManagement.Common.Dtos.Employee;
using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Dtos.Teams;
using EmployeeManagement.Common.Model;
using System.ComponentModel.Design;

namespace EmployeeManagement.Business
{
    public class DtoEntityMapperProfile : Profile
    {
        public DtoEntityMapperProfile()
        {
            CreateMap<AddressUpdate, Address>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AddressUpdate, Address>();
            CreateMap<Address, AddressGet>();

            CreateMap<JobCreate, Job>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<JobUpdate, Job>();
            CreateMap<Job, JobGet>();

            CreateMap<EmployeeCreate, Employee>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Teams, opt => opt.Ignore())
                    .ForMember(dest => dest.Job, opt => opt.Ignore());
            
            CreateMap<EmployeeUpdate, Employee>()
                    .ForMember(dest => dest.Teams, opt => opt.Ignore())

                    .ForMember(dest => dest.Job, opt => opt.Ignore());
            CreateMap<Employee, EmployeeDetails>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                   /*.ForMember(dest => dest.Teams, opt => opt.Ignore())*/ // TODO: add teams
                   .ForMember(dest => dest.Job, opt => opt.Ignore())
                   .ForMember(dest => dest.Address, opt => opt.Ignore());
            CreateMap<Employee, EmployeeList>();

            CreateMap<TeamCreate, Team>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore());
            CreateMap<TeamUpdate, Team>()
                .ForMember(dest => dest.Employees, opt => opt.Ignore());
            CreateMap<Team, TeamGet>();
        }

    }
}