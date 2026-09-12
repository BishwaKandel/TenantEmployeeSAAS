using Application.Interfaces.Base;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.TenantContract
{
    public interface ITenantRepository : IBaseRepository<Tenant>
    {
    }
}
