using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.TenantServices
{
    public interface ICurrentTenantService
    {
        string? TenantId { get; set; }        // e.g. "ab12" from JWT claim
        string? ConnectionString { get; set; } // resolved by looking up Tenants table
    }
}
