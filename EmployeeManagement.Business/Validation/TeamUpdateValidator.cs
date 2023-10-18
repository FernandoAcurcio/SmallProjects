using EmployeeManagement.Common.Dtos.Teams;
using FluentValidation;

namespace EmployeeManagement.Business.Validation
{
    public class TeamUpdateValidator : AbstractValidator<TeamUpdate>
    {
        public TeamUpdateValidator()
        {
            RuleFor(jobUpdate => jobUpdate.Name).NotEmpty().MaximumLength(50);
        }
    }
}
