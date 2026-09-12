using Application.Contracts.TenantContract;
using Domain.Entity;
using Infrastructure.Persistence;
using Infrastructure.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.TenantService
{
    public class TenantRepository : RepositoryBase<Tenant> , ITenantRepository
    {
        private readonly MasterDbContext _context;

        public TenantRepository(MasterDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
