﻿using EmployeeManagement.Common.Dtos.Employee;
using FluentValidation;

namespace EmployeeManagement.Business.Validation
{
    public class EmployeeCreateValidator : AbstractValidator<EmployeeCreate>
    {
        public EmployeeCreateValidator()
        {
            RuleFor(employeeCreate => employeeCreate.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(employeeCreate => employeeCreate.LastName).NotEmpty().MaximumLength(100);
        }
    }
}
