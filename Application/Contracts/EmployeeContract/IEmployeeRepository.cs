using Application.Interfaces.Base;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.EmployeeContract
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
    }
}
