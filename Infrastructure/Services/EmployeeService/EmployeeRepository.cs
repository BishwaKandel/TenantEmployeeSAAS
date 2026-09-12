using Application.Contracts.EmployeeContract;
using Application.Contracts.TenantContract;
using Domain.Entity;
using Infrastructure.Persistence;
using Infrastructure.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.EmployeeService
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        private readonly MasterDbContext _context;

        public EmployeeRepository(MasterDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
