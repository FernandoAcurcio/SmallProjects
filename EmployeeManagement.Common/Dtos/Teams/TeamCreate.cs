namespace EmployeeManagement.Common.Dtos.Teams
{
    public record TeamCreate(string Name, List<int> Employees);
}