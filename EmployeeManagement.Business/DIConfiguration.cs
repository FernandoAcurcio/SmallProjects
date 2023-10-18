using EmployeeManagement.Business.Services;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Business
{
    public class DIConfiguration
    {
        // RegisterServices method is responsible for configuring services and dependencies.
        public static void RegisterServices(IServiceCollection services)
        {
            // Add AutoMapper to the service collection.
            // This makes AutoMapper available for dependency injection.
            // typeof(DtoEntityMapperProfile) specifies the AutoMapper profile class.
            services.AddAutoMapper(typeof(DtoEntityMapperProfile));
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITeamService, TeamService>();

            // Validators
            services.AddScoped<AddressCreateValidator>();
            services.AddScoped<AddressUpdateValidator>();
            services.AddScoped<EmployeeCreateValidator>();
            services.AddScoped<EmployeeUpdateValidator>();            
            services.AddScoped<JobCreateValidator>();
            services.AddScoped<JobUpdateValidator>();            
            services.AddScoped<TeamCreateValidator>();
            services.AddScoped<TeamUpdateValidator>();
        }
    }
}
