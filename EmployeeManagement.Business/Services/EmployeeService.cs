using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Dtos.Employee;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using FluentValidation;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IMapper _mapper { get; }
        private IGenericRepository<Employee> _employeeRepository { get; }
        private IGenericRepository<Job> _jobRepository { get; }
        private IGenericRepository<Address> _addressRepository { get; }
        private EmployeeCreateValidator _employeeCreateValidator { get; }
        private EmployeeUpdateValidator _employeeUpdateValidator { get; }

        public EmployeeService(IGenericRepository<Employee> employeeRepository, IGenericRepository<Job> jobRepository, IGenericRepository<Address> addressRepository, IMapper mapper,
            EmployeeCreateValidator employeeCreateValidator, EmployeeUpdateValidator employeeUpdateValidator)
        {
            _employeeRepository = employeeRepository;
            _jobRepository = jobRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
            _employeeCreateValidator = employeeCreateValidator;
            _employeeUpdateValidator = employeeUpdateValidator;
        }

        public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
        {
            await _employeeCreateValidator.ValidateAndThrowAsync(employeeCreate);
            
            var job = await _jobRepository.GetByIdAsync(employeeCreate.JobId);
            if (job == null) 
                throw new JobNotFoundException(employeeCreate.JobId);

            var address = await _addressRepository.GetByIdAsync(employeeCreate.AddressId);
            if (address == null)
                throw new AddressNotFoundException(employeeCreate.AddressId);

            var entity = _mapper.Map<Employee>(employeeCreate);
            entity.Job = job;
            entity.Address = address;
            await _employeeRepository.InsertAsync(entity);
            await _employeeRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
        {
            var entity = await _employeeRepository.GetByIdAsync(employeeDelete.Id);

            if (entity == null)
                throw new EmployeeNotFoundException(employeeDelete.Id);

            _employeeRepository.Delete(entity);
            await _employeeRepository.SaveChangesAsync();

        }

        public async Task<EmployeeDetails> GetEmployeeAsync(int id)
        {
            var entity = await _employeeRepository.GetByIdAsync(id, (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);

            if (entity == null)
                throw new EmployeeNotFoundException(id);

            return _mapper.Map<EmployeeDetails>(entity);
        }

        public async Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter)
        {
            // need to build filters first
            Expression<Func<Employee, bool>> firstNameFilter = (employee) => employeeFilter.FirstName == null ? true :
                employee.FirstName.StartsWith(employeeFilter.FirstName);
            Expression<Func<Employee, bool>> lastNameFilter = (employee) => employeeFilter.LastName == null ? true :
                employee.LastName.StartsWith(employeeFilter.LastName);
            Expression<Func<Employee, bool>> jobFilter = (employee) => employeeFilter.Job == null ? true :
                employee.Job.Name.StartsWith(employeeFilter.Job);
            Expression<Func<Employee, bool>> teamFilter = (employee) => employeeFilter.Team == null ? true :
                employee.Teams.Any(team => team.Name.StartsWith(employeeFilter.Team));

            var entities = await _employeeRepository.GetFilteredAsync(new Expression<Func<Employee, bool>>[]
            {
                firstNameFilter,
                lastNameFilter,
                jobFilter,
                teamFilter

            }, 
            employeeFilter.Skip, employeeFilter.Take, 
            (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);

            return _mapper.Map<List<EmployeeList>>(entities);
        }

        public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
        {
            await _employeeUpdateValidator.ValidateAsync(employeeUpdate);
            
            var job = await _jobRepository.GetByIdAsync(employeeUpdate.JobId);
            if (job == null) 
                throw new JobNotFoundException(employeeUpdate.JobId);   

            var address = await _addressRepository.GetByIdAsync(employeeUpdate.AddressId);
            if (address == null)
                throw new AddressNotFoundException(employeeUpdate.AddressId);

            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeUpdate.Id);
            if (existingEmployee == null)
                throw new EmployeeNotFoundException(employeeUpdate.Id);
            
            var entity = _mapper.Map<Employee>(employeeUpdate);
            entity.Job = job;
            entity.Address = address;
            _employeeRepository.Update(entity);
            await _employeeRepository.SaveChangesAsync();
        }
    }
}
