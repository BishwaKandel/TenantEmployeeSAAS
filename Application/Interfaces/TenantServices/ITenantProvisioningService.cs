using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.TenantServices
{
    public interface ITenantProvisioningService
    {
        Task CreateDatabaseAsync(string connectionString);
        Task MigrateTenantDatabaseAsync(string connectionString);
        string BuildTenantConnectionString(string tenantCode);
    }
}
