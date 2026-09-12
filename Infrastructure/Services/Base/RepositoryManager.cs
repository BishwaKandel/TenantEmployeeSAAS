using Application.Contracts.EmployeeContract;
using Application.Contracts.TenantContract;
using Application.Interfaces.Base;
using Infrastructure.Persistence;
using Infrastructure.Services.EmployeeService;
using Infrastructure.Services.TenantService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.Base
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly MasterDbContext _masterContext;

        public RepositoryManager(MasterDbContext masterContext)
        {
            _masterContext = masterContext;
        }
        public ITenantRepository tenantRepository => new TenantRepository(_masterContext);

        public IEmployeeRepository employeeRepository => new EmployeeRepository(_masterContext);

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
