using Application.Contracts;
using Application.Contracts.EmployeeContract;
using Application.Contracts.TenantContract;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Base
{
    public interface IRepositoryManager
    {
        Task SaveAsync();

        ITenantRepository tenantRepository { get;}

        IEmployeeRepository employeeRepository { get; }

    }
}
