﻿using EmployeeManagement.Common.Model;
using System.Runtime.Serialization;

namespace EmployeeManagement.Business.Exceptions
{
    [Serializable]
    public class DependentEmployeesExistException : Exception
    {
        public List<Employee> Employees { get; }

        public DependentEmployeesExistException()
        {
        }

        public DependentEmployeesExistException(List<Employee> employees)
        {
            this.Employees = employees;
        }

        public DependentEmployeesExistException(string? message) : base(message)
        {
        }

        public DependentEmployeesExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DependentEmployeesExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}