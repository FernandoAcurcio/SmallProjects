using EmployeeManagement.Common.Dtos.Employee;

namespace EmployeeManagement.Common.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate);
        Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);
        Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter);
        Task<EmployeeDetails> GetEmployeeAsync(int id);
        Task DeleteEmployeeAsync(EmployeeDelete employeeDelete);
    }
}
