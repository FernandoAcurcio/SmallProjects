namespace EmployeeManagement.Common.Dtos
{
    public record AddressGet(string Id, string Street, string Zip, string city, string Email, string? Phone);
}