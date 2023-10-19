using EmployeeManagement.Common.Dtos.Teams;
using FluentValidation;

namespace EmployeeManagement.Business.Validation
{
    public class TeamCreateValidator : AbstractValidator<TeamCreate>
    {
        public TeamCreateValidator()
        {
            RuleFor(teamCreate => teamCreate.Name).NotEmpty().MaximumLength(50);
        }
    }
}
