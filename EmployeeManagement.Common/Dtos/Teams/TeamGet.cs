using EmployeeManagement.Common.Dtos.Employee;

namespace EmployeeManagement.Common.Dtos.Teams
{
    public record TeamGet(int Id, string Name, List<EmployeeList> Employees);
}